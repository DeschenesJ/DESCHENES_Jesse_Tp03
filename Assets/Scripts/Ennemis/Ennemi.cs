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
   

    //public float EnnemiPV { get { return ennemiPV; } set { ennemiPV = value; } }
    //public float EnnemiRes { get { return ennemiRes; } set { ennemiRes = value; } }
    //public float EnnemiAtk { get { return ennemiAtk; } }
    //public bool IsEnnemiHit { get { return isEnnemiHit; } set { isEnnemiHit = value; } }

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
        if (ennemiPV <= 0 && isDefeatable)
        {
            if (ennemiPV < 0)
                ennemiPV = 0;
            ennemiDefeat();
        }

        if (manager.IsPlayerTurn == false && ennemiPV > 0)
        {
            StartCoroutine(Attack());
            StopCoroutine(Attack());
            isEnnemiAtk = true;
        }
        

    }
    // M�thode pour les propri�t�s des ennemies
    void ennemiProperties()
    {
        // les pv, la r�sistance et l'a force d'attaque de l'ennemi varient selon la vague
        
        if (manager.VagueCombat == 1)
        {
            ennemiPV = 30f;
            ennemiRes = 0f;
        }
        else
        {
            ennemiPV = 30f + (5 * manager.VagueCombat);
            ennemiRes = ennemiPV * 0.1f;
        }
        ennemiAtk = 5f * manager.VagueCombat;
        Debug.Log($"Pv ennemi: {ennemiPV}; Res ennemi: {ennemiRes}");

    }

    //M�thode qui s'active lorsque l'ennemi est vaincu
    protected void ennemiDefeat()
    {

        ennemiAnimator.SetBool("IsDefeated", true);
        // Je vais jouer un effet sonore et des particules lorsque l'ennemie est mort

        // Valeur de la vague du manager par d�faite de l'ennemi
        manager.VagueCombat++;
        Debug.Log($"Voici le combat {manager.VagueCombat}");
        isDefeatable = false;
        // Vas se d�truire lui m�me apr�s deux secondes
       // Destroy(this.gameObject, 5f);
       

    }

    public void TakeDamage(bool isHit)
    {
        if (isHit == true)
        {
            StartCoroutine(AnimDegats());
            StopCoroutine(AnimDegats());
            // Le calcul des d�g�ts que l'ennemi subie
            ennemiPV -= (Player.joueurAtk - ennemiRes);
            Debug.Log($"L'ennemi se prend {Player.joueurAtk - ennemiRes} de d�g�ts. Il ne lui reste que {ennemiPV} Pv");
            
            isEnnemiHit = false;
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
         
    }



    // Coroutine si l'ennemi se prend des d�g�ts
    IEnumerator AnimDegats()
    {
        yield return new WaitForSeconds(0.6f);
        ennemiAnimator.SetBool("IsHit", true);
        yield return new WaitForSeconds(0.01f);
        ennemiAnimator.SetBool("IsHit", false);
        
    }
}
