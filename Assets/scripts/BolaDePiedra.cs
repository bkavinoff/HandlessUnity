using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BolaDePiedra : MonoBehaviour {

    Rigidbody2D rb;
    private TiemblaCamara tiemblaCamara;
    private AudioSource sonidoImpacto;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tiemblaCamara = GameObject.FindGameObjectWithTag("PantallaTiembla").GetComponent<TiemblaCamara>();
        sonidoImpacto = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Ha ocurrido una colision en Bola De Piedra");
        //Aca llamamos a la funcion que realiza el efecto de camara temblando.
        if (collision.gameObject.tag != "Player" && ((collision.gameObject.tag == "Plataforma") || collision.gameObject.tag == "PlataformaPasto" || collision.gameObject.tag == "CajaMadera"))
        {
            sonidoImpacto.Play();
        }
        if (collision.gameObject.tag != "Player")
        {
            //tiemblaCamara.CamTiembla();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        print("Ha ocurrido un trigger en Bola De Piedra");

        if (col.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
        }

        if (col.gameObject.tag == "ZonaMuerta")
        {
            
            print("Piedra toco Zona Muerta y se destruyo el objeto");
            Destroy(gameObject);
        }
    }
}
