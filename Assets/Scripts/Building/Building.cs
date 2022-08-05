using System;
using UnityEngine;


public enum BuildingResourceType
{
    //int references of enum determines Aer/Min rate of this building
    Trade = 10, //Commerce
    Leisure = 20, //Loisir
    Hygiene = 25, //Hygiene 
    Accommodation = 30 //Logement
}


public class Building : MonoBehaviour
{
    [SerializeField] private GameObject buildingPreview = null;
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite tooltipIcon;
    [SerializeField] private int id = -1;
    [SerializeField] private int price = 99999;
    [SerializeField] private BuildingResourceType[] resourceTypes;

    public static event Action<Building> OnBuildingSpawned;
    public static event Action<Building> OnBuildingDespawned;


    #region Getters&Setters

    public GameObject GetBuildingPreview()
    {
        return buildingPreview;
    }

    public string GetBuildingName()
    {
        return buildingName;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Sprite GetTooltipIcon()
    {
        return tooltipIcon;
    }

    public int GetId()
    {
        return id;
    }

    public int GetPrice()
    {
        return price;
    }

    public BuildingResourceType[] GetBuildingResourceTypes()
    {
        return resourceTypes;
    }

    public string GetStringBuildingResourceTypes()
    {
        string finalText = "";
        foreach (BuildingResourceType type in resourceTypes)
        {
            finalText += type.ToString() + ", ";
        }
        return finalText.Substring(0, finalText.Length - 2);
    }

    public int GetResourceRateByCycle()
    {
        int totalRate = 0;
        foreach (BuildingResourceType type in resourceTypes)
        {
            totalRate += (int)type;
        }
        return totalRate;
    }

    #endregion

    void Start()
    {
        OnBuildingSpawned?.Invoke(this);
    }

    void OnDestroy()
    {
        OnBuildingDespawned?.Invoke(this);
    }

}
