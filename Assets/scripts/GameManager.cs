using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.inGame;
    public static GameManager sharedInstance;
    public string ActualScene;
    public float playerPosX;
    public float playerPosY;
    private CaballeroPlayer controller;
    
    void Awake()
    {
        if(sharedInstance== null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<CaballeroPlayer>();
        currentGameState = GameState.inGame;
    }

    // Update is called once per frame
    void Update()
    {
        SetGameState(currentGameState);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentGameState = GameState.menu;
        }
    }

   public void PlayGame()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void OptionMenu()
    {
        SetGameState(GameState.optionMenu);
    }

    private void SetGameState ( GameState gameState)
    {
        if(gameState == GameState.menu)
        {
            MenuManager.sharedInstance.ShowOptionMenu();
            Time.timeScale = 0f;

        }
        else if (gameState == GameState.inGame)
        {
            
            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.ShowGameMenu();
            Time.timeScale = 1f;


        }
        else if (gameState == GameState.optionMenu)
        {
            MenuManager.sharedInstance.ShowOptionMenu();
        }

        this.currentGameState = gameState;
    }
    
    void Reloadlevel()
    {
        controller.StartGame();
        
    }
}

public enum GameState
{
    menu,
    inGame,
    gameOver,
    optionMenu

}
