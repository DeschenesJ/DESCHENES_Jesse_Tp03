using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ennemi : MonoBehaviour, IDamageable
{
    private GameManager manager;
    // Vatiable qui d�termine si l'ennemi peut �tre vancu (il ne le peut pas s'il n'existe pas
    private bool isDefeatable;

    public static Animator ennemiAnimator;

    // Les Pv de l'ennemi
    public static float ennemiPV;
    // La r�sistance de l'ennemi
    public static float ennemiRes;
    // La puissance d'attaque de l'ennemi
    public static float ennemiAtk;
    // Variable qui d�termine si l'ennemi est touch� ou non
    public static bool isEnnemiHit;
    // Variable qui d�termine si l'ennemi attaque ou non
    public static bool isEnnemiAtk;
    // Variable qui d�termine si l'ennemi est vaincu
    public static bool isEnnemiDefeat;

    // Start is called before the first frame update
    void Start()
    {
        isEnnemiHit = false;
        isDefeatable = true;
        // Va chercher le gamemanager afin d'avoir les variables dont ce script d�pends
        manager = FindObjectOfType<GameManager>();
        // Va chercher son propre animator afin de pouvoir l'utiliser pour lui faire jouer ses animations
        ennemiAnimator = GetComponent<Animator>();
        ennemiProperties();

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.IsPlayerTurn == false && ennemiPV > 0f)
        {
            StartCoroutine(Attack());
            
            isEnnemiAtk = true;
        }
        

    }
    // M�thode pour les propri�t�s des ennemies
    void ennemiProperties()
    {
        // les pv, la r�sistance et l'a force d'attaque de l'ennemi varient selon la vague
        
        if (manager.VagueCombat == 1)
        {
            ennemiPV = 20f;
            ennemiRes = 0f;
        }
        else
        {
            //D�termine les Pv de l'ennemi entre chaque vague   
            ennemiPV = 20f + (5f * (manager.VagueCombat-1));
            ennemiRes =  (5f * (manager.VagueCombat-1f));
        }
        ennemiAtk = 5f * manager.VagueCombat;
        Debug.Log($"Pv ennemi: {ennemiPV}; Res ennemi: {ennemiRes}");

    }



    // M�thode qui dit au script ennemi ce qu'il se passe quand il se prend des d�g�ts
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
        yield return new WaitForSeconds(0.01f);
        ennemiAnimator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(1f);
        StopCoroutine(Attack());


    }



    // Coroutine si l'ennemi se prend des d�g�ts
    IEnumerator AnimDegats()
    {
        yield return new WaitForSeconds(0.6f);
        ennemiAnimator.SetBool("IsHit", true);
        yield return new WaitForSeconds(0.01f);
        ennemiAnimator.SetBool("IsHit", false);
        
        // Le calcul des d�g�ts que l'ennemi subie
        ennemiPV -= (Player.joueurAtk - ennemiRes);
        if (ennemiPV <= 0 && isDefeatable)
        {
            if (ennemiPV <= 0f)
                ennemiPV = 0f;
            yield return new WaitForSeconds(0.6f);
            ennemiAnimator.SetBool("IsDefeated", true);
            // Je vais jouer un effet sonore et des particules lorsque l'ennemie est mort

            // Valeur de la vague du manager par d�faite de l'ennemi
            manager.VagueCombat++;
            Debug.Log($"Voici le combat {manager.VagueCombat}");
            isDefeatable = false;
        }
        Debug.Log($"Pv Ennemi: {ennemiPV}");
        StopCoroutine(AnimDegats());
    }
}
