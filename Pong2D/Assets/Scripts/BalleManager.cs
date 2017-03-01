using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleManager : MonoBehaviour
{

    public Rigidbody2D rb;
    public AudioManager audioManager;


    private float vitesseDeplacementBalleX;
    private float vitesseDeplacementBalleY;
    private float dureeVieBalleEnCours;
    private float timerComparaison;


    // Use this for initialization
    void Start()
    {
        this.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        //INIT DES VARIABLES SERVANT A GERER LE TEMPS D'INSTANCE DE CHAQUE BALLE
        this.dureeVieBalleEnCours = 0;
        this.timerComparaison = Time.time;

        this.AppliquerDirectionAleatoireSurBalle();

    }

    // Update is called once per frame
    void Update()
    {
        this.dureeVieBalleEnCours = Time.time - timerComparaison;

        this.GererVitesseBalleEnFonctionDuTemps(this.dureeVieBalleEnCours);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // GESTION COLLISION AVEC MURS HAUT ET BAS
        if (collision.gameObject.tag == "MurHaut" || collision.gameObject.tag == "MurBas")
        {
            this.rb.velocity = new Vector2(vitesseDeplacementBalleX, -vitesseDeplacementBalleY);
            this.vitesseDeplacementBalleY = -vitesseDeplacementBalleY;

            this.audioManager.jouerSonCollisionBalleContreMur();
        }

        // GESTION COLLISION AVEC JOUEURS 1 ET 2
        if (collision.gameObject.tag == "Joueur1" || collision.gameObject.tag == "Joueur2")
        {
            this.rb.velocity = new Vector2(-vitesseDeplacementBalleX, vitesseDeplacementBalleY);
            this.vitesseDeplacementBalleX = -vitesseDeplacementBalleX;
            this.audioManager.jouerSonCollisionBalleContreJoueur();
        }


        // GESTION COLLISION AVEC LES BUTS (MURS GAUCHE ET DROITE)
        if (collision.gameObject.name.Contains("GO_Mur"))
        {
            if (collision.gameObject.tag == "MurGauche" || collision.gameObject.tag == "MurDroite")
            {
                // GESTION COLLISION AVEC BUTS JOUEURS
                if (collision.gameObject.tag == "MurGauche")
                {
                    GameManager.scoreJ2 = GameManager.scoreJ2 + 1;
                }
                else if (collision.gameObject.tag == "MurDroite")
                {
                    GameManager.scoreJ1 = GameManager.scoreJ1 + 1;
                }

                this.audioManager.jouerSonBut();

                GameObject.Destroy(this.gameObject);
            }

        }
    }

    private void AppliquerDirectionAleatoireSurBalle()
    {
        float randNumX = Random.value;
        float randNumY = Random.value;

        if (randNumX > 0.5f)
            this.vitesseDeplacementBalleX = -5;
        else
            this.vitesseDeplacementBalleX = 5;

        if (randNumY > 0.5f)
            this.vitesseDeplacementBalleY = -5;
        else
            this.vitesseDeplacementBalleY = 5;

        //ON APPPLIQUE LA FORCE AU RIGIBODY
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.velocity = new Vector3(vitesseDeplacementBalleX, vitesseDeplacementBalleY, 0);


    }

    private void GererVitesseBalleEnFonctionDuTemps(float durreVieBalleEnCoursMethod)
    {

        if (this.vitesseDeplacementBalleX > 0)
            this.vitesseDeplacementBalleX += 0.003f;
        else
            this.vitesseDeplacementBalleX -= 0.003f;

        if (this.vitesseDeplacementBalleY > 0)
            this.vitesseDeplacementBalleY += 0.003f;
        else
            this.vitesseDeplacementBalleY -= 0.003f;

    }


}
