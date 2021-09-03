using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Assets;
using Assets.scripts;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    public Canvas menuMain, optionsMenu, playMenu, gameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        switch (SceneManager.GetActiveScene().buildIndex) {
            case 0:
                //Menu
                ShowPlayMenu();
                HideMainMenu();
                HideOptionMenu();
                break;
            case 1:
                //Menu2
                menuMain.enabled = true;
                HidePlayMenu();
                HideOptionMenu();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ShowMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //obtengo los textos ingresados
            string playerName = GetInputText("InputFieldName");
            string email = GetInputText("InputFieldMail");

            Player player = new Player(playerName, email);

            player.SavePlayerLocal();

            //reset userCanRespawn
            PlayerPrefs.SetInt("userCanRespawn", 0);
        }
        menuMain.enabled = true;
        HidePlayMenu();
        HideOptionMenu();
        HideOptionMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private string GetInputText(string tag)
    { 
        return this.CleanString(GameObject.FindGameObjectWithTag(tag).GetComponent<TextMeshProUGUI>().text);
    }

    private string CleanString(string input)
    {
        return input.Trim().Replace("\u200B", "");
    }

    public void ShowOptionMenu()
    {
        optionsMenu.enabled = true;
        
    }

   
    public void ShowPlayMenu()
    {
       playMenu.enabled = true;
    }
    public void ShowGameMenu()
    {
        gameCanvas.enabled = true;

    }

    public void HideMainMenu()
    {
        menuMain.enabled = false;
    }
    public void HideOptionMenu()
    {

        optionsMenu.enabled = false;

    }
    public void HidePlayMenu()
    {
       playMenu.enabled = false;
    }
    public void HideGameMenu()
    {
        gameCanvas.enabled = false;
    }
    public void ExitGame()
    {
        ////if de plataforma, sirve para ejecutar comando segun el tipo de plataforma
        //#if UNITY_EDITOR

        //UnityEditor.EditorApplication.isPlaying = false;

        //#else 
        //Aplication.Quit();

        //#endif
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainGame()
    {
        SceneManager.LoadScene(0);
        
    }
   
}
