using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.scripts
{
    public class BolaConCadena : MonoBehaviour
    {
        Rigidbody2D rb;
        //private TiemblaCamara tiemblaCamara;
        //public AudioSource musicPiedraImpacto;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            //tiemblaCamara = GameObject.FindGameObjectWithTag("PantallaTiembla").GetComponent<TiemblaCamara>();
            //musicPiedraImpacto = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            print("Ha ocurrido una colision en Bola Con Cadena");
            //Aca llamamos a la funcion que realiza el efecto de camara temblando.
            //if (collision.gameObject.tag != "Player")
            //{
            //    musicPiedraImpacto.Play();
            //}
            //if (collision.gameObject.tag != "Player")
            //{
            //    tiemblaCamara.CamTiembla();
            //}
        }

        void OnTriggerEnter2D(Collider2D col)
        {

            print("Ha ocurrido un trigger en Bola Con Cadena");

            if (col.gameObject.tag == "Player")
            {
                rb.isKinematic = false;
            }

            if (col.gameObject.tag == "ZonaMuerta")
            {

                print("Bola Con Cadena toco Zona Muerta y se destruyo el objeto");
                Destroy(gameObject);
            }
        }
    }
}
