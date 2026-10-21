
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;

    [Header("Player Configurations")]
    [SerializeField] private float movementSpeed = 5f;

    [Header("Camera")]
    [SerializeField] private GameObject CameraBehind;

    private float horizontal;
    private float vertical;

    private Vector3 direction;
    private bool cameraAtiva;
    private bool isWalk;
    private bool isAttack;

    [Header("Attack")]
    [SerializeField] private ParticleSystem fxAttack;
    [SerializeField] private Transform hitbox;

    [Range(0.1f, 1f)]
    [SerializeField] private float hitRange = 0.5f;

    [SerializeField] private LayerMask hitMsk;
    [SerializeField] private int amountDamage = 10;

    private Collider[] hitInfo;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ControllerInput();
        Movimento();
        UpdateAnimator();
        ControllerCamera();
    }

    private void ControllerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1") && !isAttack)
        {
            Attack();
        }
    }

    private void Movimento()
    {
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle =
                Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.rotation =
                Quaternion.Euler(0f, targetAngle, 0f);

            isWalk = true;
        }
        else
        {
            isWalk = false;
        }

        controller.Move(direction * movementSpeed * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        anim.SetBool("Walk", isWalk);
    }

    private void Attack()
    {
        isAttack = true;
        anim.SetTrigger("Attack");

        if (fxAttack != null)
        {
            fxAttack.Emit(1);
        }

        if (hitbox != null)
        {
            hitInfo = Physics.OverlapSphere
             (
                hitbox.position,
                hitRange,
                hitMsk
            );

            foreach (Collider hit in hitInfo)
            {
                hit.gameObject.SendMessage ( "getHit", amountDamage, SendMessageOptions.DontRequireReceiver );
            }
        }
    }

    public void AttackDone()
    {
        isAttack = false;
    }

    private void ControllerCamera()
    {
        if (CameraBehind != null)
        {
            CameraBehind.SetActive(cameraAtiva);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CamTrigger"))
        {
            cameraAtiva = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CamTrigger"))
        {
            cameraAtiva = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitbox != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitbox.position, hitRange);
        }
    }
}
