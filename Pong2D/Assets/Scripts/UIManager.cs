using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject menuPause;
    public GameObject menuFinPartie;

    public Text scoreJ1;
    public Text scoreJ2;
    public Text DecompteBalle;

    // Use this for initialization
    void Start()
    {
        this.scoreJ1.text = "0";
        this.scoreJ2.text = "0";
        this.DecompteBalle.enabled = false;
        this.menuPause.SetActive(false);
        this.menuFinPartie.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        this.scoreJ1.text = GameManager.scoreJ1.ToString();
        this.scoreJ2.text = GameManager.scoreJ2.ToString();

        this.DecompteBalle.text = GameManager.decompteBalle.ToString();
    }

    public void AfficherDecompteBalle()
    {
        this.DecompteBalle.enabled = true;
    }

    public void CacherDecompteBalle()
    {
        this.DecompteBalle.enabled = false;
    }

    public void AfficherMenuPause()
    {
        this.menuPause.SetActive(true);
    }

    public void CacherMenuPause()
    {
        this.menuPause.SetActive(false);
    }

    public void AfficherMenuFin(string pseudoJoueurGagnant, int scoreJ1, int scoreJ2)
    {
        this.menuFinPartie.SetActive(true);        

        //On affiche le pseudo du gagant
        Text textFelicitation = GameObject.Find("TextFelicitation").GetComponent<Text>();
        textFelicitation.text = "Félicitation " + pseudoJoueurGagnant + " !";


        //On affiche les scores des deux joueurs
        Text TextScoreJ1 = GameObject.Find("TextScoreJ1").GetComponent<Text>();
        TextScoreJ1.text = scoreJ1.ToString();

        Text TextScoreJ2 = GameObject.Find("TextScoreJ2").GetComponent<Text>();
        TextScoreJ2.text = scoreJ2.ToString();

    }

}

