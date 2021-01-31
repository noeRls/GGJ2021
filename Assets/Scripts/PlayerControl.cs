using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float baseGravity = 20f;
    public float baseLookSpeed = 2f;
    public float baseLookXLimit = 80f;

    public float decreaserEndurance = 20f;
    public float increaserEndurance = 5f;

    public Vector3 moveDirection = new Vector3();
    float rotationX = 0f;

    public Camera playerCamera;
    public CharacterController playerController;

    public GameObject flashlight;
    public bool isFlashlightOn = true;

    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = stats.lockMouse ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !stats.lockMouse;
        if (!stats.canMove)
        {
            stats.running = false;
            stats.moving = false;
            return;
        }
        
        bool doesWannaRun = Input.GetKey(KeyCode.LeftShift);
        bool doesMove = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")) > 0;
        bool isJumping = Input.GetKey(KeyCode.Space);

        bool willRun = false;

        if (Input.GetKeyDown(KeyCode.F))
            isFlashlightOn = !isFlashlightOn;
        flashlight.SetActive(isFlashlightOn);

        if (doesWannaRun)
        {
            if (stats.endurance > 0)
            {
                if (doesMove)
                {
                    stats.endurance -= decreaserEndurance * Time.deltaTime;
                    willRun = true;
                }
            } else
            {
                stats.endurance = 0;
            }
        } 
        else if (stats.endurance < 100)
        {
            stats.endurance += increaserEndurance * Time.deltaTime;
        }

        stats.running = willRun;
        stats.moving = doesMove;

        float moveSpeedY = moveDirection.y;

        moveDirection = (transform.TransformDirection(Vector3.forward) 
            * (willRun ? stats.getRunSpeed() : stats.getWalkSpeed()) 
            * Input.GetAxis("Vertical")) 
            + (transform.TransformDirection(Vector3.right) 
            * (willRun ? stats.getRunSpeed() : stats.getWalkSpeed()) 
            * Input.GetAxis("Horizontal"));

        moveDirection.y = isJumping && playerController.isGrounded 
            ? stats.jumpSpeed 
            : moveSpeedY;

        if (!playerController.isGrounded)
            moveDirection.y -= baseGravity * Time.deltaTime;

        playerController.Move(moveDirection * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * baseLookSpeed;
        rotationX = Mathf.Clamp(rotationX, -baseLookXLimit, baseLookXLimit);
        
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * baseLookSpeed, 0);
        flashlight.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        Vector3 newRotation = transform.rotation.eulerAngles;
        newRotation.z = 0;
        transform.rotation = Quaternion.Euler(newRotation); // freeze Z
    }
}
