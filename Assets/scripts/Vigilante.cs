using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigilante : MonoBehaviour
{
    public float speed;
    private bool movingRight = false;
    public Transform groundDetection;
    public float distance;

    Rigidbody2D rb;
    private TiemblaCamara tiemblaCamara;
    public AudioSource musicEnemigoImpacto;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tiemblaCamara = GameObject.FindGameObjectWithTag("PantallaTiembla").GetComponent<TiemblaCamara>();
        musicEnemigoImpacto = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Ha ocurrido una colision con Vigilante");
        //Aca llamamos a la funcion que realiza el efecto de camara temblando.
        if (collision.gameObject.tag == "Player")
        {
            musicEnemigoImpacto.Play();
        }
        if (collision.gameObject.tag == "Bala")
        {
            print("Recibio disparo");
            //tiemblaCamara.CamTiembla();
            musicEnemigoImpacto.Play();
            Destroy(gameObject, 1.3f);
        }
    }
}
