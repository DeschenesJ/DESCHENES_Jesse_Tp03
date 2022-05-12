using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour
{


    // Variables pour les dplacments du personnage joueur
    public float walkSpeed = 2.5f;
    public float runSpeed = 6f;

    private float speed;
    private float lerpSpeed;
    private float speedTarget;

    private float inputVertical;
    private float inputHorizontal;

    private Vector3 moveDirection;
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

        // Verifier les inputs du joueur s'il peut se déplacer
        if (playerAnimatior.GetBool("CanMove") == true)
        {
            // Vertical
            inputVertical = Input.GetAxis("Vertical");
            // Horizontal
            inputHorizontal = Input.GetAxis("Horizontal");

            moveDirection.x = inputHorizontal;
            moveDirection.y = 0f;
            moveDirection.z = inputVertical;


            playerDirection.forward = Vector3.RotateTowards(transform.forward, moveDirection, 10f * Time.deltaTime, 0f);
            lerpSpeed = Time.deltaTime * 5f;
            speed = Mathf.Lerp(speed, speedTarget, lerpSpeed);

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
    }

    private void FixedUpdate()
    {  
            // Déplacer le personnage selon le vecteur de direction
            rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
    }

    private void typeDeplacement(float speedValue, float typeAnimation)
    {
        speedTarget = speedValue;
        playerAnimatior.SetFloat("Vertical", inputVertical * typeAnimation);
        playerAnimatior.SetFloat("Horizontal", inputHorizontal * typeAnimation);

    }

}
