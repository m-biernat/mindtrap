using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private float speed;

    public float walkSpeed = 6f,
                 sprintSpeed = 10f,
                 crouchSpeed = 4f;

    public float mouseSensitivity = 2.0f;

    public float jumpForceValue = 10f;

    [SerializeField]
    private GameObject cam = null;

    private Rigidbody rb;

    private float distToGround;

    private Vector3 velocity = Vector3.zero, jumpForce = Vector3.zero,
                    rotationY = Vector3.zero, rotationX = Vector3.zero;

    private float xAxisMovement = 0f, zAxisMovement = 0f,
                  yAxisRotation = 0f, xAxisRotation = 0f;

    [HideInInspector] public float camRotation = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    private void Update()
    {
        xAxisMovement = Input.GetAxisRaw("Horizontal");
        zAxisMovement = Input.GetAxisRaw("Vertical");

        yAxisRotation = Input.GetAxisRaw("Mouse X");
        xAxisRotation = Input.GetAxisRaw("Mouse Y");

        jumpForce = Vector3.zero;
        if (Input.GetButtonDown("Jump"))
        { jumpForce = Vector3.up * jumpForceValue; }

        if (Input.GetButton("Sprint") && (zAxisMovement > 0))
        { speed = sprintSpeed; }
        else
        { speed = walkSpeed; }

        if (Input.GetButton("Crouch") && !Input.GetButton("Sprint"))
        { speed = crouchSpeed; }

        rotationY = CalculateRotationY(yAxisRotation);
        rotationX = CalculateRotationX(xAxisRotation);

        velocity = CalculateVelocity(xAxisMovement, zAxisMovement);

        ApplyMovement();
        ApplyRotation();
    }

    private Vector3 CalculateVelocity(float xAxisMovement, float zAxisMovement)
    {
        Vector3 horizontalMovement = transform.right * xAxisMovement;
        Vector3 verticalMovement = transform.forward * zAxisMovement;

        return (horizontalMovement + verticalMovement).normalized * speed;
    }

    private Vector3 CalculateRotationY(float yAxisRotation)
    {
        return new Vector3(0f, yAxisRotation, 0f) * mouseSensitivity;
    }

    private Vector3 CalculateRotationX(float xAxisRotation)
    {
        camRotation += xAxisRotation * mouseSensitivity;
        camRotation = Mathf.Clamp(camRotation, -80f, 80f);
        return new Vector3(camRotation, 0f, 0f);
    }

    private void ApplyMovement()
    {
        if (velocity != Vector3.zero)
        { rb.MovePosition(rb.position + velocity * Time.deltaTime); }

        if (jumpForce != Vector3.zero && IsGrounded())
        {
            rb.AddForce(jumpForce, ForceMode.Impulse);

            // This prevents from shooting into the universe.
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 0f);
        }
    }

    private void ApplyRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationY));
        cam.transform.localEulerAngles = -rotationX;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + .5f);
    }
}

