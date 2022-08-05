using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildingNameTextField;
    [SerializeField] private TextMeshProUGUI buildingCostTextField;
    [SerializeField] private TextMeshProUGUI buildingResourcesTextField;
    [SerializeField] private TextMeshProUGUI buildingAERPerMinuteTextField;
    [SerializeField] private Image buildingIcon;
    [SerializeField] private RectTransform canvasTransform;

    public static Tooltip instance;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;
    }

    private void ShowTooltip(Building building, bool isListButton)
    {
        gameObject.SetActive(true);
        if (isListButton)
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
        else
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        buildingIcon.sprite = building.GetTooltipIcon();
        buildingNameTextField.text = building.GetBuildingName();
        buildingCostTextField.text = "Cost: " + building.GetPrice().ToString();
        buildingResourcesTextField.text = "Resources: " + building.GetStringBuildingResourceTypes();
        buildingAERPerMinuteTextField.text = "AER/Min: " + building.GetResourceRateByCycle().ToString();
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }



    #region StaticMethodes
    public static void StaticHideTooltip()
    {
        instance.HideTooltip();
    }

    public static void StaticShowTooltip(Building building, bool isListButton)
    {
        instance.ShowTooltip(building, isListButton);
    }

    #endregion
}
