using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private bool walk;
    private CharacterController controller;


    [Header("Player Configurations")]

    [SerializeField] private float movementSpeed;

    private Vector3 direction;



    void Start()
    {

        anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();

    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");// utiliza predefinições da unity para se mover na horizontal no eixo X. Get.Axis para suavizar a desaceleração do movimento do personagem.
        float vertical = Input.GetAxis("Vertical");// utiliza predefinições da unity para se mover na vertical no eixo Z. Get.Axis para suavizar a desaceleração do movimento do personagem.


        if (Input.GetButtonDown("Fire1")) // se quiser pode-se substituir o getbutton por GetMouseButtonDown (0)
        {
            anim.SetTrigger("Attack");
        }



        //eixo X,eixo Y,eixo Z
        direction = new Vector3(horizontal, 0f, vertical).normalized; //normaliza a soma dos vertores.

        // magnitude é uma propriedade que permite que vc leia mas n altere. caso o valor de velocidade passar de 0,1 
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            anim.SetBool("Walk", true);

        }
        else
        {
            anim.SetBool("Walk", false);
        }


        controller.Move(direction * movementSpeed * Time.deltaTime);



    }



}