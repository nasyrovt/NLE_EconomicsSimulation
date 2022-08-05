using UnityEngine;

//Camera Movement script for mobile development
public class PanGameCamera : MonoBehaviour
{
#if UNITY_ANDROID || UNITY_IOS
    [SerializeField] private float speed = 0.1f;

    private bool inTransition;
    private BuildingButtonTouch buttonsControls;
    private DestroyBuildingsManager destroyBuildingsManager;

    void Start()
    {
        buttonsControls = FindObjectOfType<BuildingButtonTouch>();
        destroyBuildingsManager = FindObjectOfType<DestroyBuildingsManager>();
    }


    void Update()
    {
        foreach (var targetHit in TooltipManager.GetEventSystemRaycastResults())
        {
            if (targetHit.gameObject.layer == 12)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    DragCameraView();
                    return;
                }
            }
        }


    }

    private void DragCameraView()
    {
        destroyBuildingsManager.enabled = false;
        buttonsControls.enabled = false;

        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        transform.Translate(-touchDeltaPosition.x * speed, 0, -touchDeltaPosition.y * speed);

        float xPos = Mathf.Clamp(transform.position.x, -370f, 450f);
        float zPos = Mathf.Clamp(transform.position.z, -850f, 0);

        transform.position = new Vector3(xPos, 400, zPos);

        destroyBuildingsManager.enabled = true;
        buttonsControls.enabled = true;
    }
#endif
}
