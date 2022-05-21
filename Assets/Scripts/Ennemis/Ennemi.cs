using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ennemi : MonoBehaviour, IDamageable
{
    //les variables audio du joueur
    public AudioSource audioEnnemiDeath;
    public AudioSource audioEnnemiAttack;

    // Vatiable qui détermine si l'ennemi peut être vancu (il ne le peut pas s'il n'existe pas
    private bool isDefeatable;

    // l'animator de l'ennemi
    public static Animator ennemiAnimator;

    // Les Pv de l'ennemi
    public static float ennemiPV;
    // La résistance de l'ennemi
    public static float ennemiRes;
    // La puissance d'attaque de l'ennemi
    public static float ennemiAtk;
    // Variable qui détermine si l'ennemi est touché ou non
    public static bool isEnnemiHit;
    // Variable qui détermine si l'ennemi attaque ou non
    public static bool isEnnemiAtk;
    // Variable qui détermine si l'ennemi est vaincu
    public static bool isEnnemiDefeat;

    // Start is called before the first frame update
    void Start()
    {
        isEnnemiHit = false;
        isDefeatable = true;
        // Va chercher son propre animator afin de pouvoir l'utiliser pour lui faire jouer ses animations
        ennemiAnimator = GetComponent<Animator>();
        ennemiProperties();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPlayerTurn == false && ennemiPV > 0f)
        {
            StartCoroutine(Attack());
            
            isEnnemiAtk = true;
        }
    }
    // Méthode pour les propriétés des ennemies
    void ennemiProperties()
    {
        // les pv, la résistance et l'a force d'attaque de l'ennemi varient selon la vague
        
        if (GameManager.vagueCombat == 1)
        {
            ennemiPV = 20f;
            ennemiRes = 0f;
        }
        else
        {
            //Détermine les Pv de l'ennemi entre chaque vague   
            ennemiPV = 20f + (5f * (GameManager.vagueCombat-1));
            ennemiRes =  (5f * (GameManager.vagueCombat-1f));
        }
        ennemiAtk = 8f * GameManager.vagueCombat;
        Debug.Log($"Pv ennemi: {ennemiPV}; Res ennemi: {ennemiRes}");

    }



    // Méthode qui dit au script ennemi ce qu'il se passe quand il se prend des dégâts
    public void TakeDamage(bool isHit)
    {
        if (isHit == true)
        {
            StartCoroutine(AnimDegats());
            if (!(ennemiPV <= 0 && isDefeatable))
            {
                isHit = false;
                isEnnemiHit = isHit;
            }
            
        }
    }

   

    //Coroutine si l'ennemi attaque
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        ennemiAnimator.SetBool("IsAttacking", true); ;
        yield return new WaitForSeconds(0.4f);
        audioEnnemiAttack.Play();
        ennemiAnimator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(1f);
        InterfaceCombat.isFinActive = true;
        StopCoroutine(Attack());
    }

    // Coroutine si l'ennemi se prend des dégâts
    IEnumerator AnimDegats()
    {
        yield return new WaitForSeconds(0.6f);
        ennemiAnimator.SetBool("IsHit", true);
        yield return new WaitForSeconds(0.01f);
        ennemiAnimator.SetBool("IsHit", false);

        // Le calcul des dégâts que l'ennemi subie
        if (Player.joueurAtk - ennemiRes == 0)
            ennemiPV--;
        else
        {
            ennemiPV -= (Player.joueurAtk - ennemiRes);
            if (ennemiPV <= 0f)
                ennemiPV = 0f;
        }
        if (ennemiPV <= 0 && isDefeatable)
        {
            
            yield return new WaitForSeconds(0.6f);
            ennemiAnimator.SetBool("IsDefeated", true);
            audioEnnemiDeath.Play();
            // Valeur de la vague du manager par défaite de l'ennemi
            GameManager.vagueCombat++;
            Debug.Log($"Voici le combat {GameManager.vagueCombat}");
            isDefeatable = false;
        }
        Debug.Log($"Pv Ennemi: {ennemiPV}");
        StopCoroutine(AnimDegats());
    }
}
