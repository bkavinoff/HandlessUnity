using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts;

public class SceneTransition : MonoBehaviour
{

    public static SceneTransition sharedInstance;
    public Canvas txtProgDiseño, txtGestionDocumentacion, txtSonidoAgradecimiento;
    // Start is called before the first frame update
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }


    void Start()
    {


        txtProgDiseño.enabled = false;
        txtGestionDocumentacion.enabled = false;
        txtSonidoAgradecimiento.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void NextScene()
    {
        SceneManager.LoadScene((int)Escenas.Nro.Ganaste);
    }

    public void DisableCredits()
    {
        txtProgDiseño.enabled=false;
        txtGestionDocumentacion.enabled=false;
        txtSonidoAgradecimiento.enabled=false;
        Invoke("NextScene", 2f);


    }

     public void ShowCredit()
    {
        Debug.Log("Mostrando creditos");
        txtProgDiseño.enabled=true;
        Invoke("ShowCredit2", 5f);
    }
    public void ShowCredit2()
    {
        txtProgDiseño.enabled=false;
        txtGestionDocumentacion.enabled=true;
        Invoke("ShowCredit3", 5f);
    }
    public void ShowCredit3()
    {
        txtProgDiseño.enabled=false;
        txtGestionDocumentacion.enabled=false;
        txtSonidoAgradecimiento.enabled=true;
        Invoke("DisableCredits", 5f);

       

    }
}
