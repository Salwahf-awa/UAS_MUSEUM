using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float gravity = -9.81f;

    [Header("Jump")]
    public float jumpHeight = 1.2f;

    [Header("Look")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float lookUpLimit = 80f;
    public float lookDownLimit = -80f;

    [Header("Stamina (opsional)")]
    public bool useStamina = false;
    public float maxStamina = 5f;
    public float staminaDrainRate = 1f;
    public float staminaRegenRate = 0.5f;
    private float currentStamina;

    private CharacterController controller;
    private float verticalVelocity;
    private float xRotation = 0f;
    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentStamina = maxStamina;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lookDownLimit, lookUpLimit);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 horizontalMove = transform.right * moveX + transform.forward * moveZ;
        horizontalMove = Vector3.ClampMagnitude(horizontalMove, 1f);

        isSprinting = Input.GetKey(KeyCode.LeftShift) && moveZ > 0.1f;

        if (useStamina)
        {
            if (isSprinting && currentStamina > 0f)
                currentStamina -= staminaDrainRate * Time.deltaTime;
            else
            {
                isSprinting = false;
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalMove = horizontalMove * currentSpeed;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }

    public bool IsSprinting() => isSprinting;
    public float GetStaminaPercent() => currentStamina / maxStamina;
}