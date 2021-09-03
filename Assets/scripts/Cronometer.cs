using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.scripts;
using UnityEngine.SceneManagement;

public class Cronometer : MonoBehaviour
{
    protected float timeStart = 0f;
    public float finalTime = 0f;
    public bool timerActive = false;
    protected int actualLevel;

    public TextMeshProUGUI tmpUsuario; //usuario
    public TextMeshProUGUI tmpTime; //cronometro

    public TextMeshProUGUI tmpTxtOnlineRecord;
    public TextMeshProUGUI tmpMaxOnlineRecordAlias;
    public TextMeshProUGUI tmpMaxOnlineRecordTime;

    public TextMeshProUGUI tmpLocalRecordTime;

    protected bool isMaxOnlineRecordSaved = false;
    protected bool isMaxOnlineRecordLoaded = false;

    private Player localPlayer;

    private string onlineRecordAlias;
    private string onlineRecordTime;
    private float localRecord;
    private List<Score> GetPlayerBestScores()
    {
        List<Score> tiempos = new List<Score>();
        Score score = new Score();
        score.id = 0;
        score.score = PlayerPrefs.GetFloat("PlayerRecordLevel1", 0);
        score.levelId = 1;
        tiempos.Add(score);

        score.id = 1;
        score.score = PlayerPrefs.GetFloat("PlayerRecordLevel2", 0);
        score.levelId = 2;
        tiempos.Add(score);

        score.id = 2;
        score.score = PlayerPrefs.GetFloat("PlayerRecordLevel3", 0);
        score.levelId = 3;
        tiempos.Add(score);

        return tiempos;
    }

    public Player GetPlayerLocal()
    {
        //obtego el player guardado
        int id = PlayerPrefs.GetInt("PlayerId", 0);
        string alias = PlayerPrefs.GetString("PlayerAlias", "");
        string email = PlayerPrefs.GetString("PlayerEmail", "");
        string firstName = PlayerPrefs.GetString("PlayerFirstName", "");
        string lastName = PlayerPrefs.GetString("PlayerLastName", "");
        List<Score> scores = GetPlayerBestScores();

        Player player = new Player(id, alias, email, firstName, lastName, scores);
        return player;
    }

    private void Awake()
    {
        localPlayer = GetPlayerLocal();
        actualLevel = GetLevelID();
        localRecord = GetLocalTimeRecordByLevel(GetLevelID());
        //onlineRecordAlias = GetMaxOnlineRecordAlias(actualLevel);
        //tmpMaxOnlineRecordAlias.text = onlineRecordAlias;
    }

    // Start is called before the first frame update
    void Start()
    {
        //cargo el usuario en pantalla
        tmpUsuario.text = localPlayer.alias;

        //seteo el cronómetro
        float partialTime = PlayerPrefs.GetFloat("PartialTime");
        if (PlayerPrefs.GetInt("userCanRespawn") == 1)
        {
            RestartCronometer(partialTime);
            tmpTime.text = partialTime.ToString();
        }
        else {
            tmpTime.text = timeStart.ToString();
        }

        tmpTxtOnlineRecord.text = "Record Online Nivel " + actualLevel.ToString();
        //seteo el Alias del record online
        onlineRecordAlias = GetMaxOnlineRecordAlias(actualLevel);
        tmpMaxOnlineRecordAlias.text = onlineRecordAlias;

        //seteo el Tiempo del record online
        onlineRecordTime = TimeFloatToString(0, "0:00.000");
        tmpMaxOnlineRecordTime.text = onlineRecordTime;

        //Desactivo los textos en pantalla
        OnlineTextEnabler(false);

        //Muestro el record local del nivel:
        tmpLocalRecordTime.text = TimeFloatToString(localRecord, "0:00.000");

        //activo el cronómetro
        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //cargo el usuario en pantalla
        tmpUsuario.text = localPlayer.alias;

        if (timerActive)
        {
            timeStart += Time.deltaTime;
            tmpTime.text = TimeFloatToString(timeStart, "0:00.000");
            finalTime = timeStart;
        }

        //verifico si ya tengo guardados los datos del record online
        if (!isMaxOnlineRecordSaved)
        {
            int recordOnlineSaved = PlayerPrefs.GetInt("isMaxOnlineRecordSaved", 0);
            if (recordOnlineSaved == 1)
                isMaxOnlineRecordSaved = true;
        }
        else
        {
            //el record online fue seteado, lo muestro en pantalla
            if (!isMaxOnlineRecordLoaded)
            {
                //Activo los textos en pantalla
                OnlineTextEnabler(true);

                //seteo el Alias del record online
                onlineRecordAlias = GetMaxOnlineRecordAlias(actualLevel);
                tmpMaxOnlineRecordAlias.text = onlineRecordAlias;

                //seteo el Tiempo del record online
                onlineRecordTime = TimeFloatToString(GetMaxOnlineRecordTime(actualLevel), "0:00.000");
                tmpMaxOnlineRecordTime.text = onlineRecordTime;

                isMaxOnlineRecordLoaded = true;
            }
        }
    }

    private void OnlineTextEnabler(bool status)
    {
        tmpTxtOnlineRecord.enabled = status;
        tmpMaxOnlineRecordAlias.enabled = status;
        tmpMaxOnlineRecordTime.enabled = status;
    }

    public void StopCronometer()
    {
        timerActive = false;
        SaveFinalTime(finalTime);
    }

    public void RestartCronometer()
    {
        timeStart = 0f;
    }

    public void RestartCronometer(float partialTime)
    {
        timeStart = partialTime;
    }

    public float GetLocalTimeRecordByLevel(int escenaId)
    {
        //con el cero pongo un valor por defecto, en caso de que todavía no se haya escrito el archivo PlayerPrefs
        return PlayerPrefs.GetFloat("LocalRecordTime_lvl_" + escenaId, 0f);
    }

    public void SaveLocalTimeRecord(float newRecord, int escenaId)
    {
        //con el cero pongo un valor por defecto, en caso de que todavía no se haya escrito el archivo PlayerPrefs
        PlayerPrefs.SetFloat("LocalRecordTime_lvl_" + escenaId, newRecord);
    }

    public float GetFinalTime()
    {
        //con el cero pongo un valor por defecto, en caso de que todavía no se haya escrito el archivo PlayerPrefs
        return PlayerPrefs.GetFloat("FinalTime", 0f);
    }

    public void SaveFinalTime(float newRecord)
    {
        //con el cero pongo un valor por defecto, en caso de que todavía no se haya escrito el archivo PlayerPrefs
        PlayerPrefs.SetFloat("FinalTime", newRecord);
    }

    public void SavePartialTime(float newRecord)
    {
        //con el cero pongo un valor por defecto, en caso de que todavía no se haya escrito el archivo PlayerPrefs
        PlayerPrefs.SetFloat("PartialTime", newRecord);
    }

    public string GetMaxOnlineRecordAlias(int level)
    {
        //con el Sin Alias pongo un valor por defecto, en caso de que todavía no se haya escrito el archivo PlayerPrefs
        return PlayerPrefs.GetString("MaxOnlineRecordAlias_lvl_" + level.ToString(), "Sin Alias");
    }
    public float GetMaxOnlineRecordTime(int level)
    {
        //con el cero pongo un valor por defecto, en caso de que todavía no se haya escrito el archivo PlayerPrefs
        return PlayerPrefs.GetFloat("MaxOnlineRecordTime_lvl_" + level.ToString(), 0);
    }

    public bool LocalTimeIsLocalRecord(int escenaId, float tiempo)
    {
        bool isRecord = false;
        float record = GetLocalTimeRecordByLevel(escenaId);

        //si el record es 0, grabamos tiempo actual como record record
        //si el record es mayor que el tiempo obtenido, tenemos nuevo record
        if (record == 0f || tiempo < record)
        {
            isRecord = true;
        }
        return isRecord;
    }

    public void VerifyLocalTimeRecord(int escenaId, float tiempo)
    {
        if (LocalTimeIsLocalRecord(escenaId, tiempo))
        {
            SaveLocalTimeRecord(finalTime, escenaId);
        }
    }

    public int GetLevelID()
    {
        int id;
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
            case 3:
                {
                    //La escena con index 3 es el nivel 1
                    id = 1;
                    break;
                }
            case 4:
            case 5:
                {
                    //La escena con index 5 es el nivel 2
                    id = 2;
                    break;
                }
            case 6:
            case 7:
                {
                    //La escena con index 5 es el nivel 2
                    id = 3;
                    break;
                }
            default:
                {
                    id = 1;
                    break;
                }
        }
        return id;
    }

    public string TimeFloatToString(float toConvert, string format)
    {
        switch (format)
        {
            case "00.0":
                return string.Format("{0:00}:{1:0}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
            case "#0.0":
                return string.Format("{0:#0}:{1:0}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
            case "00.00":
                return string.Format("{0:00}:{1:00}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
            case "00.000":
                return string.Format("{0:00}:{1:000}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
            case "#00.000":
                return string.Format("{0:#00}:{1:000}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
            case "#0:00":
                return string.Format("{0:#0}:{1:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60);//seconds
            case "#00:00":
                return string.Format("{0:#00}:{1:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60);//seconds
            case "0:00.0":
                return string.Format("{0:0}:{1:00}.{2:0}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
            case "#0:00.0":
                return string.Format("{0:#0}:{1:00}.{2:0}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
            case "0:00.00":
                return string.Format("{0:0}:{1:00}.{2:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
            case "#0:00.00":
                return string.Format("{0:#0}:{1:00}.{2:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
            case "0:00.000":
                return string.Format("{0:0}:{1:00}.{2:000}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
            case "#0:00.000":
                return string.Format("{0:#0}:{1:00}.{2:000}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
        }
        return "error";
    }
}
