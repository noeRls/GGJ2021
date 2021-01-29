using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float baseWalkSpeed = 6f;
    public float baseRunSpeed = 10f;
    public float baseJumpSpeed = 8f;

    public float baseGravity = 20f;
    public float baseLookSpeed = 2f;
    public float baseLookXLimit = 80f;

    public float endurance = 100f;
    public float decreaserEndurance = 20f;
    public float increaserEndurance = 5f;

    public bool canRun = true;
    public bool canMove = true;

    Vector3 moveDirection = new Vector3();
    float rotationX = 0f;

    public Camera playerCamera;
    public CharacterController characterController;

    public GameObject flashlight;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool doesWannaRun = Input.GetKey(KeyCode.LeftShift);
        bool doesMove = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")) > 0;
        bool isJumping = Input.GetKey(KeyCode.Space);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool willRun = false;
        if (doesWannaRun)
        {
            if (endurance > 0)
            {
                if (doesMove)
                {
                    endurance -= decreaserEndurance * Time.deltaTime;
                    willRun = true;
                }
            } else
            {
                endurance = 0;
            }
        } 
        else if (endurance < 100)
        {
            endurance += increaserEndurance * Time.deltaTime;
        }

        float curSpeedX = canMove ? (willRun ? baseRunSpeed : baseWalkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (willRun ? baseRunSpeed : baseWalkSpeed) * Input.GetAxis("Horizontal") : 0;

        float moveDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (isJumping && canMove && characterController.isGrounded)
        {
            moveDirection.y = baseJumpSpeed;
        } else
        {
            moveDirection.y = moveDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= baseGravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * baseLookSpeed;
            rotationX = Mathf.Clamp(rotationX, -baseLookXLimit, baseLookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * baseLookSpeed, 0);
        }

        flashlight.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
