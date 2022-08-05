using UnityEngine;

public class DestroyBuildingsManager : MonoBehaviour
{
    private bool buildingDestroyed;

    void Update()
    {
        if (Input.touchCount <= 0) buildingDestroyed = false;
        if (Input.touchCount > 0 && !buildingDestroyed)
        {
            DestroyBuilding();
        }
    }

    private void DestroyBuilding()
    {
        foreach (var raycastResult in TooltipManager.GetEventSystemRaycastResults())
        {
            if (raycastResult.gameObject.layer == 16)
            {
                raycastResult.gameObject.GetComponentInParent<BuildingButtonCharacteristics>().DestroyAssociatedBuilding();
                buildingDestroyed = true;
                return;
            }
        }
    }
}
