using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SimpleJSON;

namespace Assets.scripts
{
    public class Escenas
    {
        public enum Nro
        { 
            Menu = 0,
            Menu2 = 1,
            Capitulo1=2,
            Nivel1=3,
            Capitulo2=4,
            Nivel2=5,
            Capitulo3=6,
            Nivel3=7,
            CapituloFinal=8,
            Ganaste=9,
            FinDelJuego=10
        }
    }

    [Serializable]
    public class PlayerOnlineRecord
    {
        public int id;
        public string alias;
        public string firstName;
        public string lastName;
        public string email;
        public int levelId;
        public float score;

        public static PlayerOnlineRecord CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<PlayerOnlineRecord>(jsonString);
        }
    }
        [Serializable]
    public class Score
    {
        public int id;
        public float score;
        public int levelId;
    }

    [Serializable]
    public class Player
    {
        public int id;
        public string alias;
        public string firstName;
        public string lastName;
        public string email;
        public List<Score> scores;

        public Player(string alias, string email) {
            this.id = -1;
            this.alias = alias;
            this.email = email;
            this.firstName = "";
            this.lastName = "";
            this.scores = new List<Score>();
        }

        public Player(int id, string alias, string email, string firstName, string lastName, List<Score> scores)
        {
            this.id = id;
            this.alias = alias;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.scores = scores;
        }
        public static Player CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<Player>(jsonString);
        }

        public static string CreateJsonFromPlayer(Player player)
        {
            return JsonUtility.ToJson(player);
        }

        public static string CreateJsonFromPlayerWithoutScores(Player player)
        {
            string json = JsonUtility.ToJson(player);
            JSONNode data = JSON.Parse(json);
            data.Remove(data["id"]);//remuevo el id
            data.Remove(data["scores"]);//remuevo los scores
            json = data.ToString();
            return json;
        }

        public static string CreateJsonFromScorePatchData(Score score)
        {
            string json = JsonUtility.ToJson(score);
            JSONNode data = JSON.Parse(json);
            data.Remove(data["id"]);//remuevo el id
            json = data.ToString();
            return json;
        }

        public void SavePlayerLocal()
        {
            PlayerPrefs.SetInt("PlayerId", this.id);
            PlayerPrefs.SetString("PlayerAlias", this.alias);
            PlayerPrefs.SetString("PlayerEmail", this.email);
            PlayerPrefs.SetString("PlayerFirstName", this.firstName);
            PlayerPrefs.SetString("PlayerLastName", this.lastName);
            PlayerPrefs.SetFloat("PlayerRecordLevel1", GetPlayerRecordByLevel(1));
            PlayerPrefs.SetFloat("PlayerRecordLevel2", GetPlayerRecordByLevel(2));
            PlayerPrefs.SetFloat("PlayerRecordLevel3", GetPlayerRecordByLevel(3));
            Debug.Log("PLAYER - Jugador local guardado correctamente.");
            Debug.Log("PLAYER - id: " + this.id);
            Debug.Log("PLAYER - alias: " + this.alias);
            Debug.Log("PLAYER - email: " + this.email);
            Debug.Log("PLAYER - records: ");
        }

        public float GetPlayerRecordByLevel(int level)
        {
            float maxScore = -1;
            List<Score> lista = this.scores;

            if (lista.Count > 0)
            {
                //hay puntajes guardados, los recorro
                foreach (Score puntaje in lista)
                {
                    //si el score pertenece al nivel deseado, verifico
                    if (puntaje.levelId == level)
                    {
                        //si maxScore es -1, seteo ese score como maximo
                        if (maxScore == -1)
                        {
                            maxScore = puntaje.score;
                        }
                        else
                        {
                            //ya tengo un maxScore guardado, entonces verifico si el tiempo es menor a ese maximo
                            if (puntaje.score < maxScore)
                            {
                                maxScore = puntaje.score;
                            }
                        }
                    }
                }
            }
            Debug.Log("PLAYER - record Level " + level +": " + maxScore);
            return maxScore;
        }
    }

    [Serializable]
    public class PlayersList
    {
        public List<Player> Players;
    }
}
