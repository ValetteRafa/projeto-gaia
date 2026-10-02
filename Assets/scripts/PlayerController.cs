using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;

    [Header("Player Configurations")]
    [SerializeField] private float movementSpeed;

    [Header("Camera")]
    [SerializeField] private GameObject CameraBehind;

    private Vector3 direction;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle =
                Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.rotation =
                Quaternion.Euler(0, targetAngle, 0);

            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        controller.Move(direction * movementSpeed * Time.deltaTime);

    } 


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "CamTrigger": 
                CameraBehind.SetActive(true);
                break;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "CamTrigger": 
                CameraBehind.SetActive(false);
                break;
        }
    }
}