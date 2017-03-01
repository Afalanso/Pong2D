using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static int scoreJ1, scoreJ2;
    public static float decompteBalle;

    public GameObject joueur1;
    public GameObject joueur2;
    public GameObject balle;
    public AudioManager audioManager;   
    public UIManager uiManager;

    public float scoreWinCondition;

    private GameObject nouvelleBalle;
    private bool _decompteBalleEnCours;

    public bool estEnPause;
    
    // Use this for initialization
    void Start()
    {
        this.audioManager.jouerSonPrincipal();
        //sonJeu.GetComponent<AudioSource>().Play();

        GameManager.scoreJ1 = 0;
        GameManager.scoreJ2 = 0;

        this.estEnPause = false;
        this.scoreWinCondition = 5;

        this.InstancierNouvelleBalle();
    }

    // Update is called once per frame
    void Update()
    {
        //On test si fin de partie
        if (GameManager.scoreJ1 == this.scoreWinCondition || GameManager.scoreJ2 == this.scoreWinCondition)
        {
            this.FinPartie();
        }
        else
        {
            //ON TEST SI LA BALLE EN COURS EST DETRUITE ET SI UN COMPTEUR DE LANCEMENT BALLE N'EST PAS EN COURS
            if (nouvelleBalle == null && _decompteBalleEnCours != true)
            {
                //SON BUT
                //son.GetComponent<AudioSource>().Play();
                this.InstancierNouvelleBalle();
            }
        }
    }


    private void InstancierNouvelleBalle()
    {
        this._decompteBalleEnCours = true;
        uiManager.AfficherDecompteBalle();
        GameManager.decompteBalle = 3;

        this.executeWait();
    }

    private void executeWait()
    {
        //Debug.Log("Execute Wait start");
        StartCoroutine(Wait(1.0f));
        //Debug.Log("Execute Wait After Coroutine");
    }

    private IEnumerator Wait(float seconds)
    {
        //Debug.Log("Enum start");
        yield return new WaitForSeconds(seconds);
        GameManager.decompteBalle = GameManager.decompteBalle - 1f;
        if (GameManager.decompteBalle > 0)
            this.executeWait();
        else
        {
            this.nouvelleBalle = Instantiate(balle, new Vector3(-0.03f, -0.01f, 0), Quaternion.identity) as GameObject;
            this._decompteBalleEnCours = false;
            uiManager.CacherDecompteBalle();
        }
    }

    public void MettreEnPause()
    {
        Time.timeScale = 0;
        this.estEnPause = true;
        uiManager.AfficherMenuPause();
    }

    public void ReprendreLeJeu()
    {
        Time.timeScale = 1;
        this.estEnPause = false;
        uiManager.CacherMenuPause();
    }

    public void Rejouer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Pong2D");
    }

    public void RevenirAuMenuPrincipal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Pong2D_MenuPrincipal");
    }

    public void FinPartie()
    {
        string pseudoJoueurGagnant;

        if (GameManager.scoreJ1 == this.scoreWinCondition)
            pseudoJoueurGagnant = "Joueur 1";
        else
            pseudoJoueurGagnant = "Joueur 2";

        this.uiManager.scoreJ1.enabled = false;
        this.uiManager.scoreJ2.enabled = false;

        this.uiManager.AfficherMenuFin(pseudoJoueurGagnant, GameManager.scoreJ1, GameManager.scoreJ2);

        this.joueur1.SetActive(false);
        this.joueur2.SetActive(false);       

    }


    public void QuitterLeJeu()
    {
        Application.Quit();
    }

}
