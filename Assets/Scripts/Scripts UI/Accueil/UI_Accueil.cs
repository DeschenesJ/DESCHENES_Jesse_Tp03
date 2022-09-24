using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Accueil : MonoBehaviour
{
    // Les UI du menu d'accueil
    public GameObject menuAccueil;
    public GameObject menuInformation;
    public GameObject menuAudio;


    // Start is called before the first frame update
    void Start()
    {
        menuAccueil.SetActive(true);
        menuInformation.SetActive(false);
        menuAudio.SetActive(false);

        

    }


    // M�thode pour le bouton jouer
    public void Jouer()
    {
        SceneManager.LoadScene("Main");
    }

    //M�thode pour ouvrir le menu des informations
    public void BtnInformations()
    {
        menuInformation.SetActive(true);
        menuAccueil.SetActive(false);
        
    }

    //M�thode pour fermer le menu des informations
    public void BtnRetour()
    {
        if (menuInformation.activeSelf == true)
            menuInformation.SetActive(false);
        if (menuAudio.activeSelf == true)
            menuAudio.SetActive(false);
        menuAccueil.SetActive(true);
        
    }

    //m�thode pour ouvrir le menu audio
    public void BtnAudio()
    {
        menuAudio.SetActive(true);
        menuAccueil.SetActive(false);
    
    
    }
}
