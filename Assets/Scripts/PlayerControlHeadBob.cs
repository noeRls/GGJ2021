using UnityEngine;

public class PlayerControlHeadBob : MonoBehaviour
{
    public float walkBoobingSpeed = 8f;
    public float runBoobingSpeed = 12f;
    public float bobbingAmount = 0.2f;

    public PlayerStats playerStats;
    public Camera playerCamera;

    float midpoint = 0f;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //lol
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (playerStats.moving)
        {
            //Player is moving
            timer += Time.deltaTime * (playerStats.running ? runBoobingSpeed : walkBoobingSpeed);

            transform.localPosition = new Vector3(
                transform.localPosition.x, 
                midpoint + Mathf.Sin(timer) * bobbingAmount, 
                transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(
                transform.localPosition.x, 
                Mathf.Lerp(
                    transform.localPosition.y, 
                    midpoint, 
                    Time.deltaTime * walkBoobingSpeed), 
                transform.localPosition.z);
        }
    }
}
