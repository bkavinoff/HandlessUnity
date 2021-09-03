
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Assets.scripts;

public class ControlDelJuego : MonoBehaviour
{
    private CaballeroPlayer controller;
    private string timeStringFormat = "0:00.000";
    private string playerTime;
    private Cronometer cronometer;
    private int levelId;
    private API api;
    public int numeroNivel;

    private void Start()
    {
        //seteo el numero de nivel en el PlayerPrefs
        PlayerPrefs.SetInt("numeroNivel",numeroNivel);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        cronometer = GameObject.FindWithTag("Player").GetComponent<Cronometer>();
        levelId = cronometer.GetLevelID();

        if (col.gameObject.tag == "Finish")
        {
            //FINISH es para cuando Perdés el juego

            //Paro el cronómetro
            cronometer.StopCronometer();

            //obtengo el tiempo del jugador
            float finalTime = cronometer.GetFinalTime();
            playerTime = cronometer.TimeFloatToString(finalTime, timeStringFormat);
            Debug.Log("Tiempo Final del Jugador: " + playerTime);

            //guardo el tiempo local
            cronometer.SaveFinalTime(finalTime);

            //guardo el tiempo local
            cronometer.SavePartialTime(finalTime);

            //reinicio el cronometro si el usuario no respawnea
            if (PlayerPrefs.GetInt("userCanRespawn") == 0){ 
                cronometer.RestartCronometer();
            }

            //Obtengo la posicion del jugador al perder
            float playerPosX = PlayerPrefs.GetFloat("PlayerLastPosX");
            float playerPosY = PlayerPrefs.GetFloat("PlayerLastPosY");
            Debug.Log("Posicion del jugador: x: " + playerPosX.ToString() + " - y: "+ playerPosY.ToString() + " - Nivel: " + numeroNivel.ToString());

            //cargo la escena
            SceneManager.LoadScene((int)Escenas.Nro.FinDelJuego);


        }
        if (col.gameObject.tag == "Respawn")
        {
            //Paro el cronómetro
            cronometer.StopCronometer();

            //obtengo el tiempo del jugador
            float finalTime = cronometer.GetFinalTime();
            playerTime = cronometer.TimeFloatToString(finalTime, timeStringFormat);
            Debug.Log("Tiempo Final del Jugador: " + playerTime);

            //reinicio el cronometro
            cronometer.RestartCronometer();
            PlayerPrefs.SetInt("userCanRespawn", 0);

            SceneManager.LoadScene((int)Escenas.Nro.Nivel2);
        }
        if (col.gameObject.tag == "Respawn_lvl_1")
        {
            SceneManager.LoadScene(levelId);

        }

        //paso de nivel 1 a nivel 2
        if (col.gameObject.tag == "SiguienteNivel2")
        {
            //Obtengo la escena que acabo de finalizar
            int escenaId = cronometer.GetLevelID();

            //PlayerPrefs.DeleteKey("LocalRecordTime_lvl_" + escenaId);

            //Paro el cronómetro (Este ya guarda el tiempo localmente con PlayerPrefs.SetFloat("FinalTime", newRecord);)
            cronometer.StopCronometer();

            //obtengo el tiempo del jugador
            float tiempo = PlayerPrefs.GetFloat("FinalTime");

            playerTime = cronometer.TimeFloatToString(tiempo, timeStringFormat);
            Debug.Log("Tiempo Final del Jugador: " + playerTime);

            //verificamos los tiempos
            cronometer.VerifyLocalTimeRecord(escenaId, tiempo);

            //Actualizo el jugadorOnline
            UpdateOnlinePlayer(tiempo);

            //reinicio el cronometro
            cronometer.RestartCronometer();
            PlayerPrefs.SetInt("userCanRespawn", 0);

            SceneManager.LoadScene((int)Escenas.Nro.Capitulo2);
        }

        //paso de nivel 2 a nivel 3
        if (col.gameObject.tag == "SiguienteNivel3")
        {
            //Obtengo la escena que acabo de finalizar
            int escenaId = cronometer.GetLevelID();

            //PlayerPrefs.DeleteKey("LocalRecordTime_lvl_" + escenaId);

            //Paro el cronómetro (Este ya guarda el tiempo localmente con PlayerPrefs.SetFloat("FinalTime", newRecord);)
            cronometer.StopCronometer();

            //obtengo el tiempo del jugador
            float tiempo = PlayerPrefs.GetFloat("FinalTime");

            playerTime = cronometer.TimeFloatToString(tiempo, timeStringFormat);
            Debug.Log("Tiempo Final del Jugador: " + playerTime);

            //verificamos los tiempos
            cronometer.VerifyLocalTimeRecord(escenaId, tiempo);

            //Actualizo el jugadorOnline
            UpdateOnlinePlayer(tiempo);

            //reinicio el cronometro
            cronometer.RestartCronometer();
            PlayerPrefs.SetInt("userCanRespawn", 0);

            SceneManager.LoadScene((int)Escenas.Nro.Capitulo3);
        }
        
    }

    public void UpdateOnlinePlayer(float tiempo)
    {
        //obtengo el objeto API
        api = GameObject.FindGameObjectWithTag("OnlineRecordGUI").GetComponent<API>();

        //obtengo el player local
        Player localPlayer = api.GetPlayerLocal();

        //gener un nuevo score
        Score score = GenerarScore(levelId, tiempo);

        //parseo el score a json
        string jsonBody = Player.CreateJsonFromScorePatchData(score);

        //conecto al servidor para actualizar los datos del jugador
        string url = "https://handless-backend-dev-dot-istea-handless.appspot.com/api/v1/players/" + localPlayer.id.ToString();
        StartCoroutine(api.DoPatch(url, jsonBody));

        //solicito el mejor tiempo al server
        StartCoroutine(api.GetPlayerBestRecordByLevel());
    }

    private Score GenerarScore(int nivel, float score)
    {
        Score newScore = new Score();
        newScore.levelId = levelId;
        newScore.score = score;

        return newScore;
    }

}
