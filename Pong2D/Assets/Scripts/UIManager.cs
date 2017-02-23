using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject menuPause;

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
       

}

