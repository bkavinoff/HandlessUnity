using UnityEngine;
using System.Collections;
using System;

public class Enemigo2 : MonoBehaviour
{
        public float speed;
        private bool movingRight = false;
        public Transform groundDetection;
        public float distance;
        Animator enemigo1Anim;

    bool puedeMover = true;
    bool visto = false;
    bool muere = false;

    Rigidbody2D rb;
        private TiemblaCamara tiemblaCamara;
        public AudioSource musicEnemigoImpacto;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            tiemblaCamara = GameObject.FindGameObjectWithTag("PantallaTiembla").GetComponent<TiemblaCamara>();
            musicEnemigoImpacto = GetComponent<AudioSource>();
            enemigo1Anim = GetComponent<Animator>();
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
            print("Ha ocurrido una colision con Enemigo2");
            //Aca llamamos a la funcion que realiza el efecto de camara temblando.
            if (collision.gameObject.tag == "Player")
            {
                musicEnemigoImpacto.Play();
                StartCoroutine("returne");
            }
            if (collision.gameObject.tag == "Bala")
            {
                print("Enemigo2 muere");
                speed = 0;
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                enemigo1Anim.SetBool("muere", true);
                muere = true;
                musicEnemigoImpacto.Play();
                Destroy(gameObject, 1.3f);
            }
        }

    void OnTriggerEnter2D(Collider2D col)
    {

        print("Ha ocurrido un trigger en Enemigo2");

        if (col.gameObject.tag == "Player")
        {
            speed = 10;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            enemigo1Anim.SetBool("visto", true);
            visto = true;
            StartCoroutine("returne");
        }
    }

    IEnumerator returne()
        {
            yield return new WaitForSeconds(1f);
            enemigo1Anim.SetBool("visto", false);
            visto = false;
            speed = 5;
        }
    }
