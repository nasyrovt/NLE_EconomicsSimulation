using UnityEngine;
using UnityEngine.UI;

public class ScrollbarManager : MonoBehaviour
{
    [SerializeField] private float scrollingSpeenReduction;
    [SerializeField] private Scrollbar scrollbar;

    void Update()
    {
        MoveScrollAlongYInput();
    }

    private void MoveScrollAlongYInput()
    {
        foreach (var touch in TooltipManager.GetEventSystemRaycastResults())
        {
            if (touch.gameObject.layer == 17)
            {
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    Debug.Log(touchDeltaPosition.y);
                    scrollbar.value += (touchDeltaPosition.y / scrollingSpeenReduction);
                }
            }
        }
    }
}
