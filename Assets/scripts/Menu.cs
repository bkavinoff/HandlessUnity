using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.scripts;

public class Menu : MonoBehaviour {
    public void RestartScene()
    {
        SceneManager.LoadScene(GetActiveScene());
        ClearPlayerPosAtStart();
        PlayerPrefs.SetInt("userCanRespawn", 0);
    }

    public void RetomarCamino()
    {
        SceneManager.LoadScene(GetActiveScene());
        PlayerPrefs.SetInt("userCanRespawn", 1);
        
}

    private void ClearPlayerPosAtStart()
    {
        PlayerPrefs.SetFloat("PlayerLastPosX", 0.1f);
        PlayerPrefs.SetFloat("PlayerLastPosy", 0.1f);
    }

    public void SaltarScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void FinalSceneHistoria()
    {
        SceneManager.LoadScene((int)Escenas.Nro.Ganaste);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene((int)Escenas.Nro.Menu2);
    }

    public void RepeatLevel3()
    {
        SceneManager.LoadScene((int)Escenas.Nro.Nivel3);
    }
    public void RepeatLevel2()
    {
        SceneManager.LoadScene((int)Escenas.Nro.Nivel2);
    }
    public void RepeatLevel1()
    {
        SceneManager.LoadScene((int)Escenas.Nro.Nivel1);
    }

    public int GetActiveScene()
    {
        return PlayerPrefs.GetInt("ActiveLevel", 1);
    }
    
}
