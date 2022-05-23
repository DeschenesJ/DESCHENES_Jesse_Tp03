using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Variable qui va déterminer si le prochain ennemi peut apparaître
    public static bool isSpawnTime;

    // Variable qui détermine le skin de l'ennemi et possiblement les actions de l'ennemi
    System.Random rnCheck = new System.Random();
    
    // Le menu de transition entre les combats
    public GameObject menuTransition;
    //Le menu de combat peut-être le faire dnas le UI manager si j'ai le temps
    public GameObject menuCombat;
    // Le GameObject du joueur
    public GameObject joueur;
    // Le script du joueur
    private Player scriptJoueur;


    // le spawnpoint des ennemis
    public Transform ennemiPositionner;
    // les types d'ennemies-----------------
    public GameObject ennemiPaladin;
    public GameObject ennemiBrute;
    public GameObject ennemiBoss;
    //Le script de l'ennemi
    private Ennemi scriptEnnemi;

    // Variable qui détermine si y a un combat
    public static bool isFightOn;
    // La variable qui détermine quel combat que le joueur fait. Je vais peut-être la changer en public static
    [SerializeField]
    public static float vagueCombat;

    // Valeur pour activer d?sactiver coroutine
    public static bool isRoutineStarted;
  
    // determine c'est le tour de qui
    public static bool isPlayerTurn;




    // Start is called before the first frame update
    void Start()
    {
        isSpawnTime = false;
        vagueCombat = 1f;
        isPlayerTurn = true;
      //  isEnnemiTurn = false;
        isRoutineStarted = false;
        scriptJoueur = joueur.GetComponent<Player>();
        

        // La position d'apparition de l'ennemi
        ennemiPositionner = ennemiPositionner.GetComponent<Transform>();
        // Le premier ennemi en jeu est toujours le Paladin
        EnnemiSpawn(ennemiPaladin);
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie si l'ennemi dois apparaître
        if (vagueCombat > 1 && isSpawnTime == true && vagueCombat < 11)
            Spawner();

        if (isFightOn == true && scriptEnnemi == true)
        {
            // vérifie si le joueur attaque
            if (Player.isPlayerAtk == true)
            {
                Ennemi.isEnnemiHit = true;
                Player.isPlayerAtk = false;
                if (Player.isAttackBuffed == true)
                    StartCoroutine(Debuff());
                if (Player.isResistanceBuffed == true)
                    StartCoroutine(Debuff());
            }

            // va vérifier si quelqu'un se prend des dégâts
            if (Player.isPlayerHit == true)
                scriptJoueur.TakeDamage(Player.isPlayerHit);

            if (Ennemi.isEnnemiHit == true)
                scriptEnnemi.TakeDamage(Ennemi.isEnnemiHit);

            //vérifie si la partie est terminé et que l'ennemi est vaincu
            
            if (Ennemi.ennemiAnimator == true)
            {
                if (Ennemi.ennemiAnimator.GetBool("IsDefeated") == true)
                {

                    isFightOn = false;
                    //Ajouter une condition si je suis à la 10e vague
                    if (vagueCombat < 11)
                    {
                        Ennemi.ennemiAnimator.SetBool("IsDefeated", false);
                        StartCoroutine(Transition());
                        // Va détruire l'ennnemi s'il le trouve
                        Destroy(FindObjectOfType<Ennemi>().gameObject, 5f);
                    }
                    else
                    {
                        menuCombat.SetActive(false);
                        GameOver();
                    }
                }
            }
            if (vagueCombat == 11)
                GameOver();
            if (Player.joueurAnimator.GetBool("IsDefeated") == true)
                GameOver();
        }

    }

    // Méthode qui redonne le tour au joueur qui sera utilisé par l'ennemi à la fin de son tour
    public void PlayerTurnRestored()
    {
        //Le joueur reprend son tours
        isPlayerTurn = true;
        Player.isActing = true;
        Ennemi.isEnnemiAtk = false;
    }

    // Va servir à déterminer le type d'ennemi qui apparait
    void Spawner()
    {
        int checkSkin = rnCheck.Next(0, 101);
        isSpawnTime = false;
        Debug.Log($"Le check est: {checkSkin}");
        if (vagueCombat < 10 && vagueCombat > 1)
        {
            if (checkSkin <= 49)
                EnnemiSpawn(ennemiPaladin);
            else
                EnnemiSpawn(ennemiBrute);
        }
        if (vagueCombat == 10)
            EnnemiSpawn(ennemiBoss);
        // Juste si le jeu continu après la vague 10 afin de ne pas causer de problème
        if (vagueCombat > 10)
            return;
        

    }

    // Méthode pour faire apparaître les types d'ennemis
    void EnnemiSpawn(GameObject ennemiType)
    {
        // fait apparaitre un préfab de l'ennemi désiré
        GameObject objEnnemi = Instantiate(ennemiType, ennemiPositionner.position, Quaternion.Euler(0f, 180f, 0f)).gameObject;
        // détermine la cible de l'ennemi
        scriptEnnemi = objEnnemi.GetComponent<Ennemi>();
        isFightOn = true;
    }
    // Méthode qui détermine si la partie se termine sur une victoire ou sur un échec
    void GameOver()
    {
        StopAllCoroutines();
        isFightOn = false;
        if (Player.joueurAnimator.GetBool("IsDefeated") == true)
            UI_GameOver.isGameOver = true;
        else if (vagueCombat == 11)
            UI_GameOver.isGameOver = true;
        
    }

    // Coroutine pour le lapse de temps de la transition
    public IEnumerator Transition()
    {
        yield return new WaitForSeconds(6f);
        menuTransition.SetActive(true);
    }

    // Coroutine Debuff le joueur après qu'il ait attaqué avec un buff
    IEnumerator Debuff()
    {
        // Va debuff l'attaque
        if (Player.isAttackBuffed == true)
        {
            yield return new WaitForSeconds(3f);
            Player.isAttackBuffed = false;
            Player.joueurAtkMod = Player.joueurAtk;
        }

        // Va debuff la résistance
        if (Player.isResistanceBuffed == true)
        {
            yield return new WaitForSeconds(3f);
            Player.isResistanceBuffed = false;
            Player.joueurResMod = Player.joueurRes;
        }
        StopCoroutine(Debuff());
    }
}
