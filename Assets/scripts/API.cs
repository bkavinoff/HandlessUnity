using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;
using Assets.scripts;

namespace Assets.scripts
{
    public class API : MonoBehaviour
    {
        public GameObject ApiObject;
        public Canvas menuMain;
        private int userRegistered;

        void Start()
        {
            //PlayerPrefs.SetInt("userRegistered", 0);
            //userRegistered = PlayerPrefs.GetInt("userRegistered", 0);
            switch (ApiObject.tag)
            {
                case "RegistroAPI":

                    if (menuMain.enabled)
                    {
                        Debug.Log("API - Start: Obtengo al player local");
                        Player player = GetPlayerLocal();
                        string jsonString = Player.CreateJsonFromPlayerWithoutScores(player);

                        Debug.Log("API - Start: Llamo al DoPost y le paso los datos del player");
                        StartCoroutine(DoPost("https://handless-backend-dev-dot-istea-handless.appspot.com/api/v1/players", jsonString, player));
                        StartCoroutine(GetPlayerBestRecordByLevel());
                    }
                    break;
                case "OnlineRecordGUI":
                    {
                        StartCoroutine(GetPlayerBestRecordByLevel());
                        break;
                    }
            }
        }
        void Update()
        {

        }
        private void BlankLocalRecordsByLevel()
        {
            PlayerPrefs.DeleteKey("PlayerRecordLevel1");
            PlayerPrefs.DeleteKey("PlayerRecordLevel2");
            PlayerPrefs.DeleteKey("PlayerRecordLevel3");
        }

        public Player GetPlayerLocal()
        {
            //obtego el player guardado
            int id = PlayerPrefs.GetInt("PlayerId", -1);
            string alias = PlayerPrefs.GetString("PlayerAlias", "");
            string email = PlayerPrefs.GetString("PlayerEmail", "");
            string firstName = PlayerPrefs.GetString("PlayerFirstName", "");
            string lastName = PlayerPrefs.GetString("PlayerLastName", "");
            List<Score> scores = GetPlayerBestScores();

            Player player = new Player(id, alias, email, firstName, lastName, scores);
            Debug.Log("API - GetPlayerLocal - Player Local: id: " + id.ToString());
            Debug.Log("API - GetPlayerLocal - Player Local: alias: " + alias);
            Debug.Log("API - GetPlayerLocal - Player Local: email: " + email);
            Debug.Log("API - GetPlayerLocal - Player Local: scores: ");
            PrintScoresLog(scores);
            return player;
        }

        private void PrintScoresLog(List<Score> scores)
        {
            foreach (Score score in scores)
            {
                Debug.Log("API - GetPlayerLocal - Player Local: idScore:" + score.id.ToString());
                Debug.Log("API - GetPlayerLocal - Player Local: score:" + score.score.ToString());
                Debug.Log("API - GetPlayerLocal - Player Local: level:" + score.levelId.ToString());
            }
        }

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

        public IEnumerator GetPlayerById(int id)
        {
            string getPlayerByIdUrl = string.Format("https://handless-backend-dev-dot-istea-handless.appspot.com/api/v1/players/" + id.ToString());
            using (UnityWebRequest www = UnityWebRequest.Get(getPlayerByIdUrl))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (www.isDone)
                    {
                        Player player = Player.CreateFromJson(www.downloadHandler.text);
                        Debug.Log(player.alias);
                    }
                }
            }
        }

        public int GetLevelID()
        {
            int id;
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                {
                    //La escena con index 0 es el menu principal
                    id = 1;
                    break;

                }
                case 1:
                {
                    //La escena con index 0 es el menu 2
                    id = 1;
                    break;
                }
                case 3:
                {
                    //La escena con index 3 es el nivel 1
                    id = 1;
                    break;
                }
                case 5:
                {
                    //La escena con index 5 es el nivel 2
                    id = 2;
                    break;
                }
                case 7:
                {
                    //La escena con index 5 es el nivel 2
                    id = 2;
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

        public IEnumerator GetPlayerBestRecordByLevel()
        {
            //obtengo el id del nivel actual
            int levelId = GetLevelID();
            string getPlayeRecordUrl = string.Format("https://handless-backend-dev-dot-istea-handless.appspot.com/api/v1/players/best?levelId=" + levelId.ToString());
            using (UnityWebRequest www = UnityWebRequest.Get(getPlayeRecordUrl))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("API - GetPlayerBestRecordByLevel - Error al obtener mejor record level " + levelId.ToString());
                }
                else
                {
                    if (www.isDone)
                    {
                        PlayerOnlineRecord player = PlayerOnlineRecord.CreateFromJson(www.downloadHandler.text);
                        Debug.Log(player.alias);
                        SetOnlineRecordsByLevel(player, levelId);
                        PlayerPrefs.SetInt("isMaxOnlineRecordSaved", 1);
                    }
                }
            }
        }

        public IEnumerator GetPlayerByEmail(string email)
        {
            string getPlayerByEmailrUrl = "https://handless-backend-dev-dot-istea-handless.appspot.com/api/v1/players/email/" + email;
            using (UnityWebRequest www = UnityWebRequest.Get(getPlayerByEmailrUrl))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("API - GetPlayerBestRecordByLevel - Error al obtener jugador con email " + email);
                }
                else
                {
                    if (www.isDone)
                    {
                        Player player = Player.CreateFromJson(www.downloadHandler.text);
                        Debug.Log("API - GetPlayerEmail - Player: " + player.alias);
                        player.SavePlayerLocal();
                    }
                }
            }
        }

        private IEnumerator DoPost(string url, string bodyJsonString, Player player)
        {
            var request = new UnityWebRequest(url);
            //"\u200B": este es el caracter especial que llega en el json, por eso el replace
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString.Replace("\u200B", ""));

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                print(request.error);

                //si es error 422 el mail ya existe, entonces traigo los datos de ese player
                if (request.responseCode == 422)
                {
                    Debug.Log("API - DoPost Player ya existe, obteniendo player por email");
                    StartCoroutine(GetPlayerByEmail(player.email));
                }
            }
            else 
            {
                //se creó correctamente
                if (request.isDone)
                {
                    Player playerCreado = Player.CreateFromJson(request.downloadHandler.text);
                    Debug.Log("API - DoPost Player creado - Player: " + player.alias);
                    player.SavePlayerLocal();
                }
            }

            Debug.Log("Status Code: " + request.responseCode);
        }

        public IEnumerator DoPatch(string url, string bodyJsonString)
        {
            var request = new UnityWebRequest(url);
            //"\u200B": este es el caracter especial que llega en el json, por eso el replace
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString.Replace("\u200B", ""));

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.method = "PATCH";
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("API - DoPatch: Error al actualziar jugador:");
                Debug.Log(request.error.ToString());
            }
            else {
                Debug.Log("API - DoPatch: Jugador actualizado correctamente");
            }
        }

        private Player GenerateMockedPlayer()
        {
            List<Score> scores = new List<Score>();
            Score score = new Score();
            score.id = 0;
            score.score = 10;
            score.levelId = 1;
            scores.Add(score);
            score.id = 1;
            score.score = 20;
            score.levelId = 2;
            scores.Add(score);
            score.id = 2;
            score.score = 30;
            score.levelId = 3;
            scores.Add(score);
            Player player = new Player(1, "alias", "bk@bk.com", "", "", scores);
            return player;
        }

        public void SetOnlineRecordsByLevel(PlayerOnlineRecord player, int level)
        {
            string setAlias;
            string setTime;

            setAlias = "MaxOnlineRecordAlias_lvl_" + level.ToString();
            setTime = "MaxOnlineRecordTime_lvl_" + level.ToString();

            PlayerPrefs.SetString(setAlias, player.alias);
            PlayerPrefs.SetFloat(setTime, player.score);

            Debug.Log("API - SetRecordsByLevel: " + setAlias + ": " + player.alias);
            Debug.Log("API - SetRecordsByLevel: " + setTime + ": " + player.score);
        }
    }
}
