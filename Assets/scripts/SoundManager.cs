using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerWalkPlataforma, playerWalkMadera, playerWalkPasto, playerJump, playerFires, enemyDeath;

    public static void PlaySound(string clip, AudioSource audioSrc)
    {
        if (clip == "disparar")
            audioSrc.PlayOneShot(playerFires);

        if (!audioSrc.isPlaying)
        {
            switch (clip)
            {
                case "caminar":
                    //SetAudioByGroundType();
                    audioSrc.PlayOneShot(SetAudioByGroundType());
                    break;
                case "saltar":
                    audioSrc.PlayOneShot(playerJump);
                    break;
                case "enemigoMuere":
                    audioSrc.PlayOneShot(enemyDeath);
                    break;
            }
        }
    }

    public static void setAudios()
    {
        //caminata:
        playerWalkPlataforma = Resources.Load<AudioClip>("pasos");
        playerWalkMadera = Resources.Load<AudioClip>("caminataMadera");
        playerWalkPasto = Resources.Load<AudioClip>("caminataPasto");

        playerJump = Resources.Load<AudioClip>("salto");
        playerFires = Resources.Load<AudioClip>("disparo");
        enemyDeath = Resources.Load<AudioClip>("zombie");
    }

    private static AudioClip SetAudioByGroundType()
    {
        AudioClip clip;
        string groundType = GetGroundType();
        switch (groundType)
        {
            case "Plataforma":
            {
                    clip = playerWalkPlataforma;
                break;
            }
            case "CajaMadera":
            {
                    clip = playerWalkMadera;
                break;
            }
            case "PlataformaPasto":
            {
                    clip = playerWalkPasto;
                break;
            }
            default:
            {
                clip = playerWalkPlataforma;
                    break;
            }
        }
        return clip;
    }

    public static void StopSound(AudioSource audioSrc)
    {
        if (audioSrc.isPlaying == true)
            audioSrc.Stop();
    }

    public static bool IsPlayingSound(AudioSource audioSrc)
    {
        return (audioSrc.isPlaying == true);
    }

    private static string GetGroundType()
    {
        //con el cero pongo un valor por defecto, en caso de que todavía no se haya escrito el archivo PlayerPrefs
        return PlayerPrefs.GetString("GroundType", "Null");
    }

    // Start is called before the first frame update
    void Start()
    {
        //sonidos:
        setAudios();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
