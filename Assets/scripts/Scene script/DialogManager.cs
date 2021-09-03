using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.scripts;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    public TextMeshProUGUI textDisplay;
    public Dialog dialogue;
    Queue<string> sentences;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject exitButton;
    public GameObject text, background, imageScene;
    private string activeSentence;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        imageScene.SetActive(true);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        text.SetActive(true);
        if (sentences.Count <= 0)
        {
            text.SetActive(false);
            exitButton.SetActive(false);
            continueButton.SetActive(false);
            ChangeScene();
        }
        else
        {
            activeSentence = sentences.Dequeue();
            textDisplay.text = activeSentence;
        }
        Debug.Log(activeSentence);    
    }
    
    void ChangeScene()
    {
        if (imageScene.tag!="PortalCapituloFinal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (imageScene.tag == "PortalCapituloFinal")
        {
            DisableObject();
            Invoke("LoadCredits", 2f);
        }
       
    }
    // En caso de querer usar un efecto de pantalla negra como si fuera transicion, pero sin animacion 
    // Se debe usar con el funcion Invoke para darle tiempo de ejecucion.
    void ActiveObject()
    {
        imageScene.SetActive(true);
        text.SetActive(true);
        exitButton.SetActive(true);
        continueButton.SetActive(true);
    }
    // En caso de queres usar un efecto de pantalla negra como si fuera transicion, pero sin animacion
    // Se debe ajustar el tamaño del object background para que ocupe toda la pantalla al desactivar los otros objectos.
    void DisableObject()
    {
        imageScene.SetActive(false);
        text.SetActive(false);
        exitButton.SetActive(false);
        continueButton.SetActive(false);
    }
    public void LoadCredits()
    {
        SceneTransition.sharedInstance.ShowCredit();
    }
}