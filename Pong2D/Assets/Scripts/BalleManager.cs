using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleManager : MonoBehaviour
{

    enum ConfigBalle
    {
        VitesseBalle = 6
    }

    public Rigidbody2D rb;
    public AudioManager audioManager;
           
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

        #region Collision Joueur
        if (collision.gameObject.tag == "Player")
        {
            GameObject raquette = collision.gameObject;

            float positionBasseRaquette = raquette.transform.position.y - (raquette.GetComponent<BoxCollider2D>().size.y / 4);
            float positionHauteRaquette = raquette.transform.position.y + (raquette.GetComponent<BoxCollider2D>().size.y / 4);

            if (this.rb.transform.position.y > positionHauteRaquette)
            {
                print("HAUT");
                this.rb.velocity = new Vector2(this.rb.velocity.x * -1f, this.rb.velocity.y + 1);
            }
            else if (this.rb.transform.position.y < positionBasseRaquette)
            {
                print("BAS");
                this.rb.velocity = new Vector2(this.rb.velocity.x * -1f, this.rb.velocity.y - 1);
            }
            else
            {
                print("MILIEU");
                //this.rb.velocity = new Vector2(this.rb.velocity.x * -1f, this.rb.velocity.y); = ANCIENNE CONFIG
                this.rb.velocity = new Vector2(this.rb.velocity.x * -1f, this.rb.velocity.y);
            }

            this.audioManager.jouerSonCollisionBalleContreJoueur();

            //print("positionHaute : " + positionHauteRaquette);
            //print("positionBasse : " + positionBasseRaquette);
            //print("Differentre entre Haute et Basse " + (positionHauteRaquette - positionBasseRaquette));

            //print("Raquette position Y : " + raquette.transform.position.y);
            //print("Balle position Y : " + this.transform.position.y);
            //print((this.transform.position.y - raquette.transform.position.y) + "= Diff");            
        }
        #endregion


        // GESTION COLLISION AVEC MURS HAUT ET BAS
        if (collision.gameObject.tag == "MurHaut")
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x, this.rb.velocity.y * -1f);
            //this.vitesseDeplacementBalleY = -vitesseDeplacementBalleY;
            this.audioManager.jouerSonCollisionBalleContreMur();
        }

        if (collision.gameObject.tag == "MurBas")
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x, this.rb.velocity.y * -1f);
            //this.vitesseDeplacementBalleY = -vitesseDeplacementBalleY;

            this.audioManager.jouerSonCollisionBalleContreMur();
        }

        #region Collision Buts
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
        #endregion

    }



    private void AppliquerDirectionAleatoireSurBalle()
    {
        this.rb = GetComponent<Rigidbody2D>();
        Vector2 velocityBalle;

        float randNumX = Random.value;
        float randNumY = Random.value;

        if (randNumX > 0.5f)
            velocityBalle.x = -6;
        else
            velocityBalle.x = 6;

        if (randNumY > 0.5f)
            velocityBalle.y = -0.5f;
        else
            velocityBalle.y = 0.5f;

        //ON APPPLIQUE LA FORCE AU RIGIBODY
        this.rb.velocity = velocityBalle;
    }


    private void GererVitesseBalleEnFonctionDuTemps(float durreVieBalleEnCoursMethod)
    {
        Vector2 velocity = this.rb.velocity;
        float velocityX = velocity.x;
        float velocityY = velocity.y;

        //if (this.vitesseDeplacementBalleX > 0)
        //    this.vitesseDeplacementBalleX += 0.003f;
        //else
        //    this.vitesseDeplacementBalleX -= 0.003f;

        //if (this.vitesseDeplacementBalleY > 0)
        //    this.vitesseDeplacementBalleY += 0.003f;
        //else
        //    this.vitesseDeplacementBalleY -= 0.003f;

        if (this.rb.velocity.x > 0)
        {
            velocityX += 0.003f;
            this.rb.velocity = new Vector2(velocityX, this.rb.velocity.y);
        }
        else
        {
            velocityX -= 0.003f;
            this.rb.velocity = new Vector2(velocityX, this.rb.velocity.y);
        }

        if (this.rb.velocity.y > 0)
        {
            velocityY += 0.003f;
            this.rb.velocity = new Vector2(this.rb.velocity.x, velocityY);
        }
        else
        {
            velocityX -= 0.003f;
            this.rb.velocity = new Vector2(this.rb.velocity.x, velocityY);
        }

    }


}
