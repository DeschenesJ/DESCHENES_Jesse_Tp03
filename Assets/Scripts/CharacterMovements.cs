using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    public float speed = 1.5f;
    public float jumpHeight = 1f;  

    // Private vars
    float groundDistance = 0.25f;
    LayerMask groundLayerMask = 1;
    Vector3 moveDirection;
    Rigidbody rb;
    bool isGrounded;

    float vertical, horizontal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Ground check
        // Créer un layer pour le personnage pour qu'il évite de se détecter lui-même
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayerMask, QueryTriggerInteraction.Ignore);

        // 1.2 Inputs
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        // Déplacements
        moveDirection = transform.forward * vertical;
        moveDirection += transform.right * horizontal;

        // ------------------------------------------------------------

        // Jump -------------------------------------------------------
        if (Input.GetButtonDown("Jump") && isGrounded)
        {            
            Jump();
        }

        // Respawn ------------------------------------------------
        if (transform.position.y < -15f)
            transform.position = Vector3.zero;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {        
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
