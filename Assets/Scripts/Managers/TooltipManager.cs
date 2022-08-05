using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipManager : MonoBehaviour
{
    private bool isListButton;

    private void Update()
    {
        Building buildingButtonHoverOver = ShowHideTooltip();
        if (buildingButtonHoverOver != null)
            Tooltip.StaticShowTooltip(buildingButtonHoverOver, isListButton);
        else
            Tooltip.StaticHideTooltip();
    }

    private Building ShowHideTooltip()
    {
        List<RaycastResult> eventSystemRaysastResults = GetEventSystemRaycastResults();
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaycastResult = eventSystemRaysastResults[index];
            if (curRaycastResult.gameObject.layer == 14) //List Buttons
            {
                isListButton = true;
                return curRaycastResult.gameObject.GetComponentInParent<BuildingButtonCharacteristics>().GetAssociatedBuilding();
            }
            if (curRaycastResult.gameObject.layer == 15) //Buildings Buttons
            {
                isListButton = false;
                return curRaycastResult.gameObject.GetComponent<BuildingButton>().GetBuilding();
            }
        }
        return null;
    }


    //Gets all event system raycast results of current mouse or touch position.
    public static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
