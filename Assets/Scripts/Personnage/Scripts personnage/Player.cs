using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{

    // Pv du joueur
    public static float joueurPV;
    // la résistance au dégâts du joueur
    public static float joueurRes;
    // La puissance d'attaque du joueur
    public static float joueurAtk;
    // Variable qui détermine si le joueur se fait toucher ou non
    public static bool isPlayerHit;
    // Variable qui détermine si le joueur attaque ou non
    public static bool isPlayerAtk;
    // variable qui détermine si le joueur a fait une action
    public static bool isActing;
    // variable qui détermine si le joueur se soigne
    public static bool isHealing;


    //public float JoueurPV { get { return joueurPV; } set { joueurPV = value; } }
    //public float JoueurRes { get { return joueurRes; } set { joueurRes = value; } }
    //public float JoueurAtk { get { return joueurAtk; } set { joueurAtk = value; } }
    //public bool IsPlayerHit { get { return isPlayerHit; } set { isPlayerHit = value; } }

    // Va chercher le rigid body
    private Rigidbody rb;

    // Va chercher l'animator du Joueur
    public static Animator joueurAnimator;


    // Start is called before the first frame update
    void Start()
    {
        // Initialisation des Pv et de la résistance aux dégats du joueur
        joueurPV = 50f;
        joueurRes = joueurPV*0.1f;
        joueurAtk = 10f;

        //Lorsque isActing est false l'interface de combat va empêcher le joueur d'interagir avec ses boutons d'actions, sauf pour le bouton de passer le tour
        isActing = true;
        isPlayerHit = false;
        isPlayerAtk = false;
        isHealing = false;
        // Assigne l'animator du joueur pour ses animations de combat
        joueurAnimator = GetComponent<Animator>();
        // Assigne le rigidbody
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        // Pour des besoins de test, j'ai fait en sorte que je peux attacker avec la touche A
        if (isPlayerAtk)
        {
            StartCoroutine(Attack());
            StopCoroutine(Attack());
        }
        if (joueurPV <= 0)
            Defeat();


    }

    public void TakeDamage(bool isHit)
    {
        if (isHit == true)
        {
            StartCoroutine(AnimDamage());
            StopCoroutine(AnimDamage());
            // Les dégats que le joueur subie
            if (Ennemi.ennemiAtk - joueurRes == 0)
                joueurPV--;
            else
                joueurPV -= (Ennemi.ennemiAtk - joueurRes);
            Debug.Log($"Pv Joueur: {joueurPV}");
            isPlayerHit = false;
        }
    }

    // Méthode qui détermine si le joueur est vaincu
    void Defeat()
    {
        joueurAnimator.SetBool("IsDefeated", true);
        // Je vais ajouter des effets sonores et des particules lorsque le personnage joueur meurt
        
    }

    // Coroutine pour l'animation d'attaque
    IEnumerator Attack()
    {
        joueurAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.01f);
        joueurAnimator.SetBool("IsAttacking", false);
        
    }
    // Coroutine pour l'animation lorsque le joueur se prende des dégâts
    IEnumerator AnimDamage()
    {
        yield return new WaitForSeconds(2.6f);
        joueurAnimator.SetBool("IsHit", true);
        yield return new WaitForSeconds(0.01f);
        joueurAnimator.SetBool("IsHit", false);
    }
}
