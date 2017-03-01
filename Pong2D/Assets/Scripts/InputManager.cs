using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject GO_Joueur1;
    public GameObject GO_Joueur2;

    private float vitesseJoueur1;
    private float vitesseJoueur2;

   
    // Use this for initialization
    void Start()
    {
        this.vitesseJoueur1 = 10;
        this.vitesseJoueur2 = 10;

        //gameManager = new GameManager();
    }

    // Update is called once per frame
    void Update()
    {
        //TOUCHE UP J1
        if(Input.GetKey(KeyCode.Z))
        {            
            if(this.GO_Joueur1.activeSelf)
                seDeplacerVersHaut(GO_Joueur1);
        }

        //TOUCHE DOWN J1
        if (Input.GetKey(KeyCode.S))
        {
            if (this.GO_Joueur1.activeSelf)
                seDeplacerVersBas(GO_Joueur1);
        }

        //TOUCHE UP J2
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (this.GO_Joueur2.activeSelf)
                seDeplacerVersHaut(GO_Joueur2);
        }

        //TOUCHE DOWN J2
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (this.GO_Joueur2.activeSelf)
                seDeplacerVersBas(GO_Joueur2);
        }

        //TOUCHE ECHAPE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gameManager.estEnPause)
                this.gameManager.MettreEnPause();
            else
                this.gameManager.ReprendreLeJeu();
        }        

    }



    public void seDeplacerVersHaut(GameObject GO_Joueur)
    {
        GO_Joueur.transform.Translate(Vector3.up * Time.deltaTime * vitesseJoueur1);
    }

    public void seDeplacerVersBas(GameObject GO_Joueur)
    {
        GO_Joueur.transform.Translate(Vector3.down * Time.deltaTime * vitesseJoueur2);
    }




}
