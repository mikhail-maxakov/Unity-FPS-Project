using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Настройки чувствительности")]
    public float mouseSensitivity = 100f;

    [Header("Ограничение вертикального взгляда")]
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    [Header("Ссылки на объекты")]
    public Transform playerBody;

    [Header("Инвертировать оси")]
    public bool invertVertical = false;
    public bool invertHorizontal = false;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (invertVertical) mouseY = -mouseY;
        if (invertHorizontal) mouseX = -mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (playerBody != null)
        {
            playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetRotation(float verticalAngle, float horizontalAngle)
    {
        xRotation = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
        yRotation = horizontalAngle;

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        if (playerBody != null)
        {
            playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }

    public Vector2 GetCurrentRotation()
    {
        return new Vector2(xRotation, yRotation);
    }
}