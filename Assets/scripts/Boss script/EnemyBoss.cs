using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float speed;
    private bool movingRight = false;
    public Transform groundDetection;
    public float distance;
    Animator enemigo1Anim;

    bool puedeMover = true;
    bool contacto = false;
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
        print("Ha ocurrido una colision con Enemigo1");
        //Aca llamamos a la funcion que realiza el efecto de camara temblando.
        if (collision.gameObject.tag == "Player")
        {
            //Hacer que enemigo ataque.
            enemigo1Anim.SetBool("contacto", true);
            contacto = true;
            musicEnemigoImpacto.Play();
            StartCoroutine("returne");
            Destroy(gameObject,0.5f);
        }
        if (collision.gameObject.tag == "Bala")
        {
            print("Enemigo1 muere");
            speed = 0;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            enemigo1Anim.SetBool("muere", true);
            muere = true;
            musicEnemigoImpacto.Play();
            Destroy(gameObject, 1.3f);
        }
    }

    IEnumerator returne()
    {
        yield return new WaitForSeconds(0.2f);
        enemigo1Anim.SetBool("contacto", false);
        contacto = false;
    }
}

