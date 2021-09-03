using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int damage;
   


    
    public Slider healthBar;
   
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //  toma el valor de slice declarado
        healthBar.value = health;

        if (healthBar.value == 0)
        {
            SceneManager.LoadScene("FinDelJuego");

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisiona al player");
        if (collision.gameObject.tag == "Vigilante")
        {
            Debug.Log("daño al player");
            // para poder restar el canvas que contiene el valor maximo slice debe ser igual al valor de health que agregemos
            health -= damage;

        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Crayon toca player");
        if (collision.gameObject.tag == "Untagged")
        {
            // para poder restar el canvas que contiene el valor maximo slice debe ser igual al valor de health que agregemos
            health -= damage;
            Debug.Log("Crayon resta vida");
        }
    }
}
