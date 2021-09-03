using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalExit : MonoBehaviour
{
    private API api;
    private Cronometer cronometer;
    private ControlDelJuego controlJuego;

    // Start is called before the first frame update
    void Start()
    {
        cronometer = GameObject.FindGameObjectWithTag("Player").GetComponent<Cronometer>();
        controlJuego = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlDelJuego>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entro al portal");
        //if (collision.gameObject.tag == "Player")
        //{
        //    Invoke("NextScene", 1f);
        //}

        if (collision.gameObject.tag == "Player" && gameObject.tag == "PortalCapituloFinal")
        {
            //Obtengo la escena que acabo de finalizar
            int escenaId = cronometer.GetLevelID();

            //PlayerPrefs.DeleteKey("LocalRecordTime_lvl_" + escenaId);

            //Paro el cronómetro (Este ya guarda el tiempo localmente con PlayerPrefs.SetFloat("FinalTime", newRecord);)
            cronometer.StopCronometer();

            //obtengo el tiempo del jugador
            float tiempo = PlayerPrefs.GetFloat("FinalTime");

            string playerTime = cronometer.TimeFloatToString(tiempo, "0:00.000");
            Debug.Log("Tiempo Final del Jugador: " + playerTime);

            //verificamos los tiempos
            cronometer.VerifyLocalTimeRecord(escenaId, tiempo);

            //Actualizo el jugadorOnline
            controlJuego.UpdateOnlinePlayer(tiempo);

            //reinicio el cronometro
            cronometer.RestartCronometer();

            SceneManager.LoadScene((int)Escenas.Nro.CapituloFinal);
        }
    }


    //void NextScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}
