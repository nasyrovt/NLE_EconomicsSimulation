using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform = null;
    [SerializeField] private LayerMask buildingBlockLayer = new LayerMask();
    [SerializeField] private Building[] buildings = new Building[0];
    [SerializeField] private int resources = 600;

    [SerializeField] private GameObject newBuildingButtonPrefab;
    [SerializeField] private GameObject listParentObject;

    private List<Building> myBuildings = new List<Building>();

    public Transform GetCameraTransform()
    {
        return cameraTransform;
    }

    public List<Building> GetMyBuildings()
    {
        return myBuildings;
    }

    public int GetResources()
    {
        return resources;
    }


    public bool CanPlaceBuilding(BoxCollider buildingCollider, Vector3 spawnLocation)
    {

        if (Physics.CheckBox(
            spawnLocation + buildingCollider.center,
            buildingCollider.size / 2,
            Quaternion.identity,
            buildingBlockLayer
        )) return false;

        return true;

        // ***** Additional feature *****
        // ***** If we want to make sure we're in range of our buildings *****

        // foreach (Building building in myBuildings)
        // {
        //     if ((spawnLocation - building.transform.position).sqrMagnitude
        //         <= buildingRangeLimit * buildingRangeLimit)
        //     {
        //         return true;
        //     }
        // }

        // return false;
    }


    private void Start()
    {
        Building.OnBuildingSpawned += HandleBuildingSpawned;
        Building.OnBuildingDespawned += HandleBuildingDespawned;
    }

    private void HandleBuildingSpawned(Building building)
    {
        myBuildings.Add(building);
        GameObject newBuildingButton = Instantiate(newBuildingButtonPrefab);
        BuildingButtonCharacteristics characteristics = newBuildingButton.GetComponent<BuildingButtonCharacteristics>();
        characteristics.SetBuildingName(building.name);
        characteristics.SetResourcesType(building.GetBuildingResourceTypes());
        characteristics.SetAssociatedBuilding(building);
        characteristics.GetIcon().sprite = building.GetTooltipIcon();
        newBuildingButton.transform.SetParent(listParentObject.transform);
    }

    private void HandleBuildingDespawned(Building building)
    {
        myBuildings.Remove(building);
    }

    public void SetResources(int newResources)
    {
        resources = newResources;
    }
    public void AddResources(int resourcesToAdd)
    {
        if (resourcesToAdd != 0) AudioPlayer.PlayAerAddedClip();
        resources += resourcesToAdd;
    }

    public void TryPlaceBuilding(int buildingId, Vector3 spawnLocation)
    {
        Building buildingToPlace = null;

        //Make sure the building exists in allowed buildings on Player Objects
        foreach (Building building in buildings)
        {
            if (building.GetId() == buildingId)
            {
                buildingToPlace = building;
                break;
            }
        }
        if (buildingToPlace == null) return;

        //Make sure we're not overlapping anything
        BoxCollider buildingCollider = buildingToPlace.GetComponent<BoxCollider>();
        if (!CanPlaceBuilding(buildingCollider, spawnLocation)) return;

        GameObject buildingInstance = Instantiate(buildingToPlace.gameObject, spawnLocation, buildingToPlace.transform.rotation);

        SetResources(resources - buildingToPlace.GetPrice());
        AudioPlayer.PlayBuildingPlacedClip();
    }

}
