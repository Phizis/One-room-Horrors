using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensetivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -5.0f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller;

    public float standingHeight = 2.0f;
    public float crouchHeight = 1.5f;    
    public float crouchMultiplier = 0.5f;

    public float crawlHeight = 0.5f;
    public float crawlMultiplier = 0.25f;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
        
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensetivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensetivity);

    }

    void UpdateMovement()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * walkSpeed + Vector3.up * velocityY;

        //Crouch
        /*if (Input.GetKeyDown(KeyCode.C))
        {
            controller.height = crouchHeight;
            velocity *= crouchMultiplier;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = standingHeight;
        }
        */
        //Crawl
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = crawlHeight;
            velocity *= crawlMultiplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        controller.Move(velocity * Time.deltaTime);
    }    


        
}
