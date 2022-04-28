using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour
{



    public float walkSpeed = 1.5f;
    public float runSpeed = 5f;

    private float speed = 1f;

    private float inputVertical;
    private float inputHorizontal;

    private Vector3 moveDirection;
    private Vector3 rotateDirection;

    private Transform playerDirection;
    private Rigidbody rb;

    private Animator playerAnimatior;



    // Start is called before the first frame update
    void Start()
    {
        // Assigne le rigidbody
        rb = GetComponent<Rigidbody>();

        // Assigne l'animator
        playerAnimatior = GetComponent<Animator>();

        playerDirection = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verifier les inputs du joueur
        // Vertical
        inputVertical = Input.GetAxis("Vertical");
        // Horizontal
        inputHorizontal = Input.GetAxis("Horizontal");

        rotateDirection.x = inputHorizontal;
        rotateDirection.y = 0f;
        rotateDirection.z = inputVertical;


        playerDirection.forward = Vector3.RotateTowards(transform.forward, rotateDirection, 10f * Time.deltaTime, 0f);


        // Vecteur de movement (les 4 directions)
        moveDirection = transform.forward * inputVertical + transform.right * inputHorizontal;

        //Animations de mouvements
        // Lorsque le joueur cours
        if (Input.GetKey(KeyCode.LeftShift))
        {
            typeDeplacement(runSpeed, 2f);
        }
        else
        {
            typeDeplacement(walkSpeed, 1f);
        }



    }

    private void FixedUpdate()
    {
        // Déplacer le personnage selon le vecteur de direction
        rb.MovePosition(rb.position + rotateDirection * speed * Time.fixedDeltaTime);
    }

    private void typeDeplacement(float speedValue, float typeAnimation)
    {
        speed = speedValue;
        playerAnimatior.SetFloat("Vertical", inputVertical * typeAnimation);
        playerAnimatior.SetFloat("Horizontal", inputHorizontal * typeAnimation);

    }

}
