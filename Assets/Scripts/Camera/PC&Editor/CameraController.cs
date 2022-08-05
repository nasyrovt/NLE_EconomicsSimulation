using UnityEngine;
using UnityEngine.InputSystem;


//Camera movement script for PC and Editor development (uses keyboard and mouse)
public class CameraController : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private Transform playerCameraTransform = null;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float screenBorderThickness = 10f;
    [SerializeField] private Vector2 screenXLimits = Vector2.zero;
    [SerializeField] private Vector2 screenZLimits = Vector2.zero;

    private Vector2 previousInput;

    private Controls controls;

    private void Start()
    {
        this.gameObject.SetActive(true);

        controls = new Controls();

        controls.Player.MoveCamera.performed += SetPreviousInput;
        controls.Player.MoveCamera.canceled += SetPreviousInput;

        controls.Enable();
    }

    private void Update()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 pos = playerCameraTransform.position;

        //if no keyboard input(mouse moves a screen)
        if (previousInput == Vector2.zero)
        {
            Vector3 cursorMovement = Vector3.zero;

            Vector2 cursorPosition = Mouse.current.position.ReadValue();

            if (cursorPosition.y >= Screen.height - screenBorderThickness)
                cursorMovement.z += 1;
            else if (cursorPosition.y <= screenBorderThickness)
                cursorMovement.z -= 1;

            if (cursorPosition.x >= Screen.width - screenBorderThickness)
                cursorMovement.x += 1;
            else if (cursorPosition.x <= screenBorderThickness)
                cursorMovement.x -= 1;

            pos += cursorMovement.normalized * speed * Time.deltaTime;
        }
        else
        {
            pos += new Vector3(previousInput.x, 0f, previousInput.y) * speed * (Time.deltaTime);
        }

        //Verify if we're out of screen bounds
        pos.x = Mathf.Clamp(pos.x, screenXLimits.x, screenXLimits.y);
        pos.z = Mathf.Clamp(pos.z, screenZLimits.x, screenZLimits.y);

        playerCameraTransform.position = pos;
    }

    private void SetPreviousInput(InputAction.CallbackContext ctx)
    {
        previousInput = ctx.ReadValue<Vector2>();
    }
#endif
}
