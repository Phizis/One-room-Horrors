using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerCamera = null;
    [SerializeField] float mouseSensetivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -5.0f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller;

    [SerializeField] float standingHeight = 2.0f;
    [SerializeField] float crawlHeight = 0.5f;
    [SerializeField] float crawlMultiplier = 0.25f;
    bool isSitting = false;

      
    [SerializeField] float stamina = 300f;
    [SerializeField] float staminaMax = 300f;
    float staminaMin = 1f;
    float runMultiplier = 1.5f;
    float staminaEmptyMultyplier = 0.7f;
    bool staminaFull = true;
    bool staminaHalf = false;

    bool isMouseLook = true;

    AudioSource stepSource;
    [SerializeField] float timer = 0.7f;
    [SerializeField] float timerDown = 0f;
    public AudioClip[] stepSound_AR;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        stepSource = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0f)
            isMouseLook = false;
        else
            isMouseLook = true;

        if (isMouseLook)
        {
            UpdateMouseLook();
            UpdateMovement();
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            StepSounds();
    }

    void StepSounds()
    {
        if (timerDown > 0)
            timerDown -= Time.deltaTime;
        if (timerDown <= 0)
        {
            stepSource.pitch = Random.Range(0.7f, 0.9f);
            stepSource.volume = Random.Range(0.05f, 0.15f);
            stepSource.PlayOneShot(stepSound_AR[Random.Range(0, stepSound_AR.Length)]);
            timerDown = timer;
        }
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

        timer = 0.7f;

        //run
        if (Input.GetKey(KeyCode.LeftShift) && !isSitting && staminaFull)
        {
            velocity *= runMultiplier;
            timer = 0.3f;
            stamina -= 0.5f;

            if (stamina > staminaMin && stamina < staminaMax)
            {
                staminaHalf = true;
            }

            if (stamina < 1.0f)
                staminaFull = false;
        }
        else
        {
            if (!staminaFull)
            {
                velocity *= staminaEmptyMultyplier;
            }

            if (!staminaFull || staminaHalf)
            {
                stamina += 0.5f;
                if (stamina > staminaMax)
                {
                    stamina = staminaMax;
                    staminaFull = true;
                    staminaHalf = false;
                }
            }
        }

        //Crawl
        if (Input.GetKey(KeyCode.LeftControl))
        {
            stepSource.volume = 0.1f;
            timer = 1.5f;
            controller.height = crawlHeight;
            velocity *= crawlMultiplier;
            isSitting = true;
        }
        else
        {
            controller.height = standingHeight;
            isSitting = false;
        }

        controller.Move(velocity * Time.deltaTime);        
    }
}
