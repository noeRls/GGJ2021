using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float baseWalkSpeed = 8f;
    public float baseRunSpeed = 12f;
    public float baseJumpSpeed = 8f;

    public float basePlayerGravity = 20f;
    public float basePlayerLookSpeed = 2f;
    public float basePlayerLookXLimit = 80f;

    public bool canRun = true;
    public bool canMove = true;

    Vector3 moveDirection = new Vector3();
    float rotationX = 0f;

    public Camera playerCamera;
    public CharacterController characterController;

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
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKey(KeyCode.Space);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? (isRunning ? baseRunSpeed : baseWalkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? baseRunSpeed : baseWalkSpeed) * Input.GetAxis("Horizontal") : 0;

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
            moveDirection.y -= basePlayerGravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * basePlayerLookSpeed;
            rotationX = Mathf.Clamp(rotationX, -basePlayerLookXLimit, basePlayerLookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * basePlayerLookSpeed, 0);
        }
    }
}
