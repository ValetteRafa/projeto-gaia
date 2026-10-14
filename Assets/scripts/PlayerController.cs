using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;

   
    [SerializeField] private transform hitbox;
    [SerializeField] private float hitRange = 0.5
    [serializeField] private LayerMask hitMsk;
    [SerializeField] private float movementSpeed;
    [Header("Player Configurations")]
    [Header("Camera")]
    [SerializeField] private GameObject CameraBehind;

    // Variáveis de Input, movimento e câmera
    private float horizontal;
    private float vertical;

    private Vector3 direction;

    private bool cameraAtiva;

    private bool isAttack;


    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        ControllerInput();
        Movimento();
        ControllerCamera();
    }


    // INPUTS --------------------------------------------------------------------------------------
    private void ControllerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1") && !Attack)
        {
            anim.SetTrigger("Attack");
        }
    }


    // MOVIMENTO -----------------------------------------------------------------------------

    private void Movimento()
    {
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


    //  CAMERA -------------------------------------------------------------------------------------

    private void ControllerCamera()
    {
        CameraBehind.SetActive(cameraAtiva);
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "CamTrigger":
                cameraAtiva = true;
                break;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "CamTrigger":
                cameraAtiva = false;
                break;
        }
    }
    public void Attackdone ()
    {
        isAttack = true 
    }
}