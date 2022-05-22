using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    //les variables audio du joueur
    public AudioSource audiosJoueurDeath;
    public AudioSource audioJoueurAttack;
    public AudioSource audioJoueurHealing;
    public AudioSource audioJoueurBuffingAtk;
    public AudioSource audioJoueurBuffingRes;
    // Pv du joueur
    public static float joueurPVMax;
    public static float joueurPV;
    // la résistance au dégâts du joueur
    public static float joueurRes;
    // Inclue le modificateur de résistance utlilisé par les autres scripts pour les calculs
    public static float joueurResMod;
    // La puissance d'attaque du joueur
    public static float joueurAtk;
    // Inclue le modificateur de dégâts variable utlilisé par les autres scripts pour les calculs
    public static float joueurAtkMod;
    // Variable qui détermine si le joueur multiplie son attaque
    public static bool isBuffingAtk;
    // Variable qui détermine si le joueur multiplie sa résistance
    public static bool isBuffingRes;
    // Variable qui s'active lorsque le joueur augmente son attaque
    public static bool isAttackBuffed;
    // Variable qui s'active lorsque le joueur augmente sa résistance 
    public static bool isResistanceBuffed;
    // Variable qui détermine si le joueur se fait toucher ou non
    public static bool isPlayerHit;
    // Variable qui détermine si le joueur attaque ou non
    public static bool isPlayerAtk;
    // variable qui détermine si le joueur a fait une action
    public static bool isActing;
    // variable qui détermine si le joueur se soigne
    public static bool isHealing;
    //Variable qui détermine si le joueur est vaincu
    public static bool isDefeated;



    // Va chercher l'animator du Joueur
    public static Animator joueurAnimator;


    // Start is called before the first frame update
    void Start()
    {
        isDefeated = false;

        // Initialisation des Pv et de la résistance aux dégats du joueur
        joueurPVMax = 100f;
        joueurPV = joueurPVMax;
        joueurRes = joueurPVMax*0.1f;
        joueurAtk = 10f;
        joueurAtkMod = joueurAtk;
        joueurResMod = joueurRes;
        
        // Initialisation des variables booléennes du joueur
        //Lorsque isActing est false l'interface de combat va empêcher le joueur d'interagir avec ses boutons d'actions, sauf pour le bouton de passer le tour
        isActing = true;
        isPlayerHit = false;
        isPlayerAtk = false;
        isHealing = false;
        isBuffingAtk = false;
        isAttackBuffed = false;
        isBuffingRes = false;
        isResistanceBuffed = false;
        // Assigne l'animator du joueur pour ses animations de combat
        joueurAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        if (isDefeated == false)
        {// Si le joueur peut attaquer il joue son animation d'attaque
            if (isPlayerAtk)
                StartCoroutine(Attack());
            // Si le joueur est à 0 ou moins de Pv il est vaincu
            if (joueurPV <= 0 && isDefeated == false)
               StartCoroutine(Defeat());
            if (isHealing)
               StartCoroutine(Casting());
            if (isBuffingAtk)
                StartCoroutine(Casting());
            if (isBuffingRes)
                StartCoroutine(Casting());
        }

    }

    // Méthode qui dit au script du joueur ce qu'il se passe lorsqu'il se prend des dégâts
    public void TakeDamage(bool isHit)
    {
        if (isHit == true)
        {
            StartCoroutine(AnimDamage());
            isHit = false;
            isPlayerHit = isHit;
        }
    }

    

    // Méthode qui détermine si le joueur est vaincu
    IEnumerator Defeat()
    {
        joueurAnimator.SetBool("IsDefeated", true);
        yield return new WaitForSeconds(0.3f);
        // Je vais ajouter des effets sonores et des particules lorsque le personnage joueur meurt
        audiosJoueurDeath.Play();
        isDefeated = true;
        StopCoroutine(Defeat());

    }

    // Coroutine pour l'animation d'attaque
    IEnumerator Attack()
    {
        joueurAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.4f);
        audioJoueurAttack.Play();
        joueurAnimator.SetBool("IsAttacking", false);
        Debug.Log("Vous devez Passer votre tour");
        StopCoroutine(Attack());

    }

    // Coroutine pour l'animation de soins du joueur et autre cast
    IEnumerator Casting()
    {
        // Lorsque le joueur se soigne
        if (isHealing == true)
        {
            joueurAnimator.SetBool("IsCasting", true);
            //audio de soins
            audioJoueurHealing.Play();
            yield return new WaitForSeconds(0.4f);
            joueurAnimator.SetBool("IsCasting", false);

            // Le joueur se soinge s'il a perdu de la vie
            if (joueurPV < joueurPVMax)
            {
                joueurPV += joueurPVMax * 0.2f;
                //Le joueur ne peut pas dépasser ses Pv max
                if (joueurPV >= joueurPVMax)
                    joueurPV = joueurPVMax;
            }
            // Va mettre à jour la vie de l'ennemi à l'écran
            FindObjectOfType<UI_Manager>().UIupdatePV(FindObjectOfType<UI_Manager>().txt_PvJoueur, joueurPV, joueurPVMax);
            isHealing = false;
        }

        // lorsque le joueur buff son attaque
        if (isBuffingAtk == true && isAttackBuffed == false)
        {
            audioJoueurBuffingAtk.Play();
            joueurAnimator.SetBool("IsCasting", true);
            yield return new WaitForSeconds(0.4f);
            joueurAnimator.SetBool("IsCasting", false);
            joueurAtkMod = joueurAtk * 5f;
            isBuffingAtk = false;
            isAttackBuffed = true;
        }

        // lorsque le joueur buff sa resistance
        if (isBuffingRes == true && isResistanceBuffed == false)
        {
            audioJoueurBuffingRes.Play();
            joueurAnimator.SetBool("IsCasting", true);
            yield return new WaitForSeconds(0.4f);
            joueurAnimator.SetBool("IsCasting", false);
            joueurResMod = joueurRes * 4f;
            isBuffingRes = false;
            isResistanceBuffed = true;
        }
        StopCoroutine(Casting());
    }

    // Coroutine pour l'animation lorsque le joueur se prende des dégâts
    IEnumerator AnimDamage()
    {
        if (isDefeated == false)
        {
            //yield return new WaitForSeconds(2.6f);
            joueurAnimator.SetBool("IsHit", true);
            yield return new WaitForSeconds(0.01f);
            joueurAnimator.SetBool("IsHit", false);
            // Le joueur se prend les dégâts
            if (Ennemi.ennemiAtk - joueurResMod <= 0)
                joueurPV--;
            else
            {
                joueurPV -= (Ennemi.ennemiAtk - joueurResMod);
                if (joueurPV <= 0f)
                    joueurPV = 0f;
            }
            if (isResistanceBuffed == true)
            {
                isResistanceBuffed = false;
            }
            // Va mettre à jour la vie du joueur à l'écran
            FindObjectOfType<UI_Manager>().UIupdatePV(FindObjectOfType<UI_Manager>().txt_PvJoueur, joueurPV, joueurPVMax);
        }
        StopCoroutine(AnimDamage());
    }
}
