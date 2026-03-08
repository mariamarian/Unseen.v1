using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    [Header("Mișcare")]
    public float speed = 5f;
    public float gravity = 9.81f;

    [Header("Mouse Look")]
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private CharacterController controller;
    private float verticalVelocity = 0f; // gravitație
    private float rotationX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * lookSpeed);

        // Input WASD
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Direcție mișcare conform orientării playerului
        Vector3 move = transform.right * inputX + transform.forward * inputZ;

        // Gravitație pe Y
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f; // mic offset ca să rămână pe sol
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Aplică mișcarea + gravitația
        Vector3 finalMove = move * speed * Time.deltaTime;
        finalMove.y = verticalVelocity * Time.deltaTime;

        controller.Move(finalMove);
    }
}