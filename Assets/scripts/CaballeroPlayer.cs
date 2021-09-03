using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CaballeroPlayer : MonoBehaviour
{
    //Ultima posicion del jugador
    public Vector2 PlayerLastPos = new Vector2(0.1f, 0.1f);

    //Creamos una variable pública donde asignar nuestro prefab 'Shuriken'
    public GameObject ShurikenPrefab;

    Rigidbody2D caballeroRB;
    public float maxVelocidad;
    Animator caballeroAnim;

    //tipoTerreno
    public string terreno;

    bool puedeMover = true;

    //Saltar.
    bool enSuelo = false;
    float chequearRadioSuelo = 0.2f;
    public LayerMask capaSuelo;
    public Transform chequearSuelo;
    public float poderSalto;
    private BoxCollider2D playerCollider;
    private AudioSource audioSrc;

    //Voltear caballero.
    bool voltearCaballero = true;
    SpriteRenderer caballeroRender;

    private GameObject cam;

    //puntos de guardado parcial:
    //Nivel 1:
    Vector2 playerRepawn_N1_Inicio = new Vector2(-16f, 36f);
    Vector2 playerRepawn_N1_PortalAzul = new Vector2(270f, 38.88367f);
    Vector2 playerRepawn_N1_PortalVerde = new Vector2(476f, 120f);

    //Nivel 2:
    Vector2 playerRepawn_N2_Inicio = new Vector2(-16f, 36f);
    Vector2 playerRepawn_N2_Respawn1 = new Vector2(144.9154f, -1.105029f);
    Vector2 playerRepawn_N2_Respawn2 = new Vector2(289.4424f, -1.525053f);
    Vector2 playerRepawn_N2_Respawn3 = new Vector2(434.591f, 2.7711725f);
    Vector2 playerRepawn_N2_Respawn4 = new Vector2(591.1599f, 3.207695f);
    Vector2 playerRepawn_N2_Respawn5 = new Vector2(746.0909f, -0.1562037f);
    Vector2 playerRepawn_N2_Respawn6 = new Vector2(864.8422f, 5.961369f);

    bool respawnPosition = false;

    // Use this for initialization
    void Start()
    {
        //cam = GameObject.Find("Camara");
        //seteo el nivel activo:
        SetActiveLevel(SceneManager.GetActiveScene().buildIndex);
        int numeroNivel = PlayerPrefs.GetInt("numeroNivel");

        caballeroRB = GetComponent<Rigidbody2D>();
        caballeroRender = GetComponent<SpriteRenderer>();
        caballeroAnim = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        audioSrc = GetComponent<AudioSource>();

        //seteo los audios:
        SoundManager.setAudios();

        //obtengo la posición del jugador al perder
        float lastPosX = PlayerPrefs.GetFloat("PlayerLastPosX");

        //verifico ultima posición del jugador para setear el flag
        switch (numeroNivel)
        {
            case 1:
                if (lastPosX >= playerRepawn_N1_PortalAzul.x)
                {
                    respawnPosition = true;
                }
                break;
            case 2:
                if (
                    lastPosX >= playerRepawn_N2_Respawn1.x ||
                    lastPosX >= playerRepawn_N2_Respawn2.x ||
                    lastPosX >= playerRepawn_N2_Respawn3.x ||
                    lastPosX >= playerRepawn_N2_Respawn4.x ||
                    lastPosX >= playerRepawn_N2_Respawn5.x ||
                    lastPosX >= playerRepawn_N2_Respawn6.x
                    )
                {
                    respawnPosition = true;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (respawnPosition && PlayerPrefs.GetInt("userCanRespawn") == 1)
            
        {
            //obtengo la posición del jugador al perder
            float lastPosX = PlayerPrefs.GetFloat("PlayerLastPosX");
            //obtengo el numero de nivel
            int numeroNivel = PlayerPrefs.GetInt("numeroNivel");

            switch (numeroNivel)
            {
                case 1:
                    if (lastPosX >= playerRepawn_N1_PortalAzul.x)
                    {
                        RespawnJugador(playerRepawn_N1_PortalVerde);
                    }
                    break;
                case 2:
                    if (lastPosX >= playerRepawn_N2_Respawn2.x && lastPosX < playerRepawn_N2_Respawn3.x) {
                        RespawnJugador(playerRepawn_N2_Respawn2);
                    }
                    if (lastPosX >= playerRepawn_N2_Respawn3.x && lastPosX < playerRepawn_N2_Respawn4.x)
                    {
                        RespawnJugador(playerRepawn_N2_Respawn3);
                    }
                    if (lastPosX >= playerRepawn_N2_Respawn4.x && lastPosX < playerRepawn_N2_Respawn5.x)
                    {
                        RespawnJugador(playerRepawn_N2_Respawn4);
                    }
                    if (lastPosX >= playerRepawn_N2_Respawn5.x && lastPosX < playerRepawn_N2_Respawn6.x)
                    {
                        RespawnJugador(playerRepawn_N2_Respawn5);
                    }
                    if (lastPosX >= playerRepawn_N2_Respawn6.x)
                    {
                        RespawnJugador(playerRepawn_N2_Respawn6);
                    }
                    break;
            }
        }

        //salto
        if (puedeMover && enSuelo && Input.GetAxis("Jump") > 0)
        {
            caballeroAnim.SetBool("estaEnSuelo", false);
            caballeroRB.velocity = new Vector2(caballeroRB.velocity.x, 0f);
            caballeroRB.AddForce(new Vector2(0, poderSalto), ForceMode2D.Impulse);
            enSuelo = false;

            //detenemos sonido de caminata
            //reproducimos sonido de salto
            if (SoundManager.IsPlayingSound(audioSrc))
                SoundManager.StopSound(audioSrc);

            SoundManager.PlaySound("saltar", audioSrc);
        }

        enSuelo = Physics2D.OverlapCircle(chequearSuelo.position, chequearRadioSuelo, capaSuelo);
        caballeroAnim.SetBool("estaEnSuelo", enSuelo);

        //caminata
        float mover = Input.GetAxis("Horizontal");

        if (puedeMover)
        {
            if (mover > 0 && !voltearCaballero)
            {
                Voltear();
            }
            else if (mover < 0 && voltearCaballero)
            {
                Voltear();
            }
            caballeroRB.velocity = new Vector2(mover * maxVelocidad, caballeroRB.velocity.y);

            if (enSuelo && mover != 0)
            {
                //reproducimos sonido de caminata
                SoundManager.PlaySound("caminar", audioSrc);
            }

            //Hacer que caballero corra.
            caballeroAnim.SetFloat("VelMovimiento", Mathf.Abs(mover));
        }
        else
        {
            //detenemos sonido de caminata
            SoundManager.StopSound(audioSrc);

            caballeroRB.velocity = new Vector2(0, caballeroRB.velocity.y);

            caballeroAnim.SetFloat("VelMovimiento", 0);
        }

        PlayerPrefs.SetFloat("PlayerLastPosX", caballeroRB.position.x);
        PlayerPrefs.SetFloat("PlayerLastPosY", caballeroRB.position.y);

        Debug.Log("Posicion del jugador: x: " + caballeroRB.position.x.ToString() + " - y: " + caballeroRB.position.y.ToString());


        //Si pulsamos el botón 'Fire1'...
        if (ShurikenPrefab != null && Input.GetButtonDown("Fire1"))
        {
            //detenemos sonido de caminata
            if (SoundManager.IsPlayingSound(audioSrc))
                SoundManager.StopSound(audioSrc);

            //reproducimos sonido de disparo
            SoundManager.PlaySound("disparar", audioSrc);

            //Accedemos al script 'ArmaArrojadiza.cs' del prefab
            DisparoBala scriptShuriken = ShurikenPrefab.GetComponent<DisparoBala>();

            if (Input.GetAxis("Vertical") > 0)
            {
                //Ataque hacia arriba
                scriptShuriken.DireccionArma = Direccion.Vertical;
                scriptShuriken.Velocidad = Math.Abs(scriptShuriken.Velocidad);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                //Ataque hacia abajo
                scriptShuriken.DireccionArma = Direccion.Vertical;
                scriptShuriken.Velocidad = -Math.Abs(scriptShuriken.Velocidad);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                //Ataque hacia la derecha
                scriptShuriken.DireccionArma = Direccion.Horizontal;
                scriptShuriken.Velocidad = Math.Abs(scriptShuriken.Velocidad);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                //Ataque hacia la izquierda
                scriptShuriken.DireccionArma = Direccion.Horizontal;
                scriptShuriken.Velocidad = -Math.Abs(scriptShuriken.Velocidad);
            }

            //Creamos una instancia del prefab en nuestra escena, concretamente en la posición de nuestro personaje
            Instantiate(ShurikenPrefab, transform.position, Quaternion.identity);
        }

    }

    public void RespawnJugador(Vector2 pos) {
        //muevo al jugador
        caballeroRB.transform.position = pos;
        respawnPosition = false;
    }

    void Voltear()
    {
        voltearCaballero = !voltearCaballero;
        caballeroRender.flipX = !caballeroRender.flipX;
    }

    public void togglePuedeMover()
    {
        puedeMover = !puedeMover;
    }


    public void StartGame()
    {
        Invoke("RestartPosition", 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        terreno = col.gameObject.tag;
        SetGroundType(terreno);
    }

    public void SetGroundType(string groundType)
    {
        PlayerPrefs.SetString("GroundType", groundType);
    }

    public void SetActiveLevel(int levelId)
    {
        PlayerPrefs.SetInt("ActiveLevel", levelId);
    }
}
