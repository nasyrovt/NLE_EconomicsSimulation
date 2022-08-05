using System.Collections;
using UnityEngine;


public class BuildingButtonTouch : MonoBehaviour
{
    [SerializeField] private LayerMask floorMask = new LayerMask();
    [SerializeField] private GameObject notEnoughAER;
    private Camera mainCamera;
    private Building currentBuilding;
    private ResourceManager resourceManager;
    private GameObject buildingPreviewInstance;
    private BoxCollider buildingCollider;
    private Renderer buildingRendererInstance;
    private Player player;
    private PanGameCamera cameraMovement;
    private Vector3 positionForBuilding;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraMovement = FindObjectOfType<PanGameCamera>();
        resourceManager = FindObjectOfType<ResourceManager>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.touchCount <= 0)
        {
            if (buildingPreviewInstance == null) return;

            OnEndDrag(positionForBuilding);
            return;
        }
        if (buildingPreviewInstance == null)
        {
            foreach (var raycastResult in TooltipManager.GetEventSystemRaycastResults())
            {
                if (raycastResult.gameObject.layer == 15)
                {
                    currentBuilding = raycastResult
                                    .gameObject
                                    .GetComponent<BuildingButton>()
                                    .GetBuilding();
                    buildingCollider = currentBuilding.GetComponent<BoxCollider>();
                    cameraMovement.enabled = false;
                    OnStartDrag(currentBuilding);
                }
            }
        }

        UpdateBuildingPreview();
    }

    //Instatiates a building preview if not yet instatiated
    private void OnStartDrag(Building building)
    {
        if (buildingPreviewInstance != null) return;

        //Check if we've got enough of money
        if (resourceManager.GetResources() < building.GetPrice())
        {
            StartCoroutine(nameof(NotEnoughOfAER));
            cameraMovement.enabled = true;
            return;
        }

        AudioPlayer.PlayHoverOverClip();
        buildingPreviewInstance = Instantiate(building.GetBuildingPreview());

        //Disable movement scripts if there are ones
        if (buildingPreviewInstance.gameObject.GetComponentInChildren<Oscillator>() != null)
            buildingPreviewInstance.gameObject.GetComponentInChildren<Oscillator>().enabled = false;
        if (buildingPreviewInstance.gameObject.GetComponentInChildren<Rotator>() != null)
            buildingPreviewInstance.gameObject.GetComponentInChildren<Rotator>().enabled = false;

        buildingRendererInstance = buildingPreviewInstance.GetComponentInChildren<Renderer>();

        buildingPreviewInstance.SetActive(false);
    }

    IEnumerator NotEnoughOfAER()
    {
        FindObjectOfType<CameraShake>().Play();
        AudioPlayer.PlayNotEnoughAERClip();
        notEnoughAER.SetActive(true);
        yield return new WaitForSeconds(3f);
        notEnoughAER.SetActive(false);

    }

    private void OnEndDrag(Vector3 positionToPlace)
    {
        if (buildingPreviewInstance == null) return;

        Ray ray = mainCamera.ScreenPointToRay(positionToPlace);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            //Tells the game so it tries to place a building
            player.TryPlaceBuilding(currentBuilding.GetId(), positionToPlace);
        }

        Destroy(buildingPreviewInstance);
        buildingPreviewInstance = null;
        cameraMovement.enabled = true;
    }

    private void UpdateBuildingPreview()
    {
        if (buildingPreviewInstance == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            buildingPreviewInstance.SetActive(false);
            return;
        }

        buildingPreviewInstance.transform.position = hit.point;

        positionForBuilding = hit.point;

        if (!buildingPreviewInstance.activeSelf) buildingPreviewInstance.SetActive(true);

        Color canPlaceColor = player.CanPlaceBuilding(buildingCollider, hit.point) ? Color.green : Color.red;

        buildingRendererInstance.material.color = canPlaceColor;
    }

}
