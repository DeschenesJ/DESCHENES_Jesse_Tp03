using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public GameObject menuAudio;
    public GameObject menuPause;
    // Variable qui détermine si le menu audio est ouvert
    bool isMenuOpen;

    // Start is called before the first frame update
    void Start()
    {
        isMenuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Lorsque le bouton pour ouvrir le menu audio est activé
        if (MenuPause.isAudioOpen == true && isMenuOpen == false)
        {
            menuAudio.SetActive(true);
            menuPause.SetActive(false);
            isMenuOpen = true;
        }
    }

    // Méthode pour le bouton retour du menu audio
    public void btnRetour()
    {
        MenuPause.isAudioOpen = false;
        menuAudio.SetActive(false);
        isMenuOpen = false;
        menuPause.SetActive(true);
        
    }
}
