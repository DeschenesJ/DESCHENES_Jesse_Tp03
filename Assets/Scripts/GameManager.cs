using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // L'animator du joueur
    public Animator playerAnimator;

    // le spawnpoint des ennemis
    public Transform ennemyPositionner;

   


    // Valeur pour activer d?sactiver coroutine
    private bool isRoutineStarted;
    
    public bool IsRoutineStarted { get { return isRoutineStarted; } }

    // d?termine c'est le tour ? qui
    private bool isPlayerTurn;
    // d?termine s'il y a un combat ou non
    private bool isFighting;

    public bool IsPlayerTurn { get { return isPlayerTurn; } set { isPlayerTurn = value; } }
    public bool IsFighting { get { return isFighting; } set { isFighting = value; } }

    // Start is called before the first frame update
    void Start()
    {

        isPlayerTurn = true;
        isFighting = false;
        isRoutineStarted = false;
        playerAnimator = GetComponent<Animator>();
        ennemyPositionner = GetComponent<Transform>();
        // Je vais faire une coroutine qui spawn les ennemis seulement au début du combat
        // et dans cette coroutine je vais mettre une distance en x par rapport au spawner pour avoir plusieur ennemis.
        // Ça ne devrait pas être compliqué, puisque les ennemis et encounters ne sont pas randoms et c'est un nombre fixe
        // de combats avant le boss et le boss ou la mort du joueur termine la partie.

    }

    // Update is called once per frame
    void Update()
    {
        // Coroutine entre les combats si nécessaire
        //if (isFighting == true && isRoutineStarted == false)
        //{
        //    isRoutineStarted = true;
        //    StartCoroutine(StartCombat());
        //}
        //else if (isFighting == false && isRoutineStarted == true)
        //{
        //    isRoutineStarted = false;
        //    StopAllCoroutines();
        //}

        if (Input.GetKeyDown("p"))
        {
            Debug.Log("lol");
            Time.timeScale = 0f;
            if (Time.timeScale == 0f)
                Time.timeScale = 1f;
        }
        


    }
    
    // Coroutine des combats va me permettre de faire apparaitre les mobs et possiblement faire les transitions entre les combats
    IEnumerator Combats(float bidon)
    {
        // Pour le moment c'est une valeur bidon que je retourne afin de ne pas avoir de problème de code
        yield return bidon;
    }

    //Je vais me baser sur le spawner des ennemis ci-dessous--------------------
    //IEnumerator Spawner()
    //{
    //    //la valeiur de la vague
    //    iVague++;
    //    // le nombre de squelette par vague
    //    iEnnemiS = 1 + iVague;
    //    // le nombre de Nightshade par vague
    //    iEnnemiN = iVague - 1;
    //    // le nommbre de Warrok par vague--
    //    iEnnemiW = iVague - 2;
    //    if (iEnnemiW <= 0)
    //        iEnnemiW = 0;
    //    //---------------------------------
    //    // La quantité d'ennemi dans la variable qui détermine si tous les ennemis sont mort
    //    deadAll = iEnnemiS + iEnnemiN + iEnnemiW;
    //    //Variable qui sert à arrêter la boucle while
    //    int iW = 0;
    //    //le délais entre chaque vague
    //    yield return new WaitForSeconds(spawnVagueInterval);
    //    // Boucle qui va mettre les ennemis dans la vague
    //    while (iW < iEnnemiS + iEnnemiN + iEnnemiW)
    //    {
    //        // Spawn des ennemis (je vais devoir faire une boucle selon la vague)            
    //        for (int iSpawn = 0; iSpawn < iEnnemiS; iSpawn++, iW++)
    //        {
    //            EnnemiSpawn(ennemiS);
    //            yield return new WaitForSeconds(spawnDelay);
    //        }
    //        //----------------
    //        for (int iSpawn = 0; iSpawn < iEnnemiN; iSpawn++, iW++)
    //        {
    //            EnnemiSpawn(ennemiN);
    //            yield return new WaitForSeconds(spawnDelay);
    //        }
    //        //----------------
    //        for (int iSpawn = 0; iSpawn < iEnnemiW; iSpawn++, iW++)
    //        {
    //            EnnemiSpawn(ennemiW);
    //            yield return new WaitForSeconds(spawnDelay + 0.5f);
    //        }

    //    }

    //}
    //// Méthode pour faire apparaître les types d'ennemis
    //void EnnemiSpawn(GameObject ennemiType)
    //{
    //    // fait apparaitre un préfab de l'ennemi désiré
    //    GameObject objEnnemi = Instantiate(ennemiType, spawnpoint.position, Quaternion.Euler(180f, 0f, 0f)).gameObject;
    //    // détermine la cible de l'ennemi
    //    Ennemies ennemies = objEnnemi.GetComponent<Ennemies>();
    //    ennemies.SetTarget(endPoint);
    //}
}
