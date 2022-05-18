using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{

    // Pv du joueur
    private float joueurPV;
    // la résistance au dégâts du joueur
    private float joueurRes;
    // La puissance d'attaque du joueur
    private float joueurAtk;
    // Variable qui détermine si le joueur se fait toucher ou non
    private bool isPlayerHit;

    public float JoueurPV { get { return joueurPV; } set { joueurPV = value; } }
    public float JoueurRes { get { return joueurRes; } set { joueurRes = value; } }
    public float JoueurAtk { get { return joueurAtk; } set { joueurAtk = value; } }
    public bool IsPlayerHit { get { return isPlayerHit; } set { isPlayerHit = value; } }

    // Va chercher le rigid body
    private Rigidbody rb;

    // Va chercher l'animator du Joueur
    private Animator joueurAnimator;

    // détermine si la coroutine Attack est en cours
    bool isRoutined;

    // Start is called before the first frame update
    void Start()
    {
        isRoutined = false;
        // Initialisation des Pv et de la résistance aux dégats du joueur
        joueurPV = 50f;
        joueurRes = joueurPV*0.1f;
        joueurAtk = 10f;
        Debug.Log(joueurPV);
        Debug.Log(joueurRes);
        Debug.Log(joueurAtk);
        isPlayerHit = false;
        // Assigne l'animator du joueur pour ses animations de combat
        joueurAnimator = GetComponent<Animator>();
        // Assigne le rigidbody
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        // Pour des besoins de test, j'ai fait en sorte que je peux attacker avec la touche A
        if (Input.GetKeyDown("a") && joueurAnimator.GetBool("IsAttacking") == false && isRoutined == false)
        {
            StartCoroutine(Attack());
            //joueurAnimator.SetBool("IsAttacking", true);
        }
        if (isRoutined == true)
        { 
            StopCoroutine(Attack());
            isRoutined = false;
        }
    }

    public void TakeDamage(bool isHit)
    {
        if (isHit == true)
            joueurPV -= (FindObjectOfType<Ennemi>().EnnemiAtk-JoueurRes);
        isPlayerHit = false;

        
    }
    IEnumerator Attack()
    {
        isRoutined = true;
        joueurAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.01f);
        joueurAnimator.SetBool("IsAttacking", false);
        


    }

}
