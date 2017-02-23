using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static int scoreJ1, scoreJ2;
    public static float decompteBalle;

    public GameObject son;
    public GameObject sonJeu;
    public GameObject balle;
    public UIManager uiManager;

    private GameObject nouvelleBalle;
    private bool _decompteBalleEnCours;

    public bool estEnPause;


    // Use this for initialization
    void Start()
    {
        sonJeu.GetComponent<AudioSource>().Play();

        GameManager.scoreJ1 = 0;
        GameManager.scoreJ2 = 0;

        this.estEnPause = false;

        this.InstancierNouvelleBalle();
    }

    // Update is called once per frame
    void Update()
    {
        //ON TEST SI LA BALLE EN COURS EST DETRUITE ET SI UN COMPTEUR DE LANCEMENT BALLE N'EST PAS EN COURS
        if (nouvelleBalle == null && _decompteBalleEnCours != true)
        {
            //SON BUT
            son.GetComponent<AudioSource>().Play();

            this.InstancierNouvelleBalle();
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


    public void QuitterLeJeu()
    {
        Application.Quit();
    }

}
