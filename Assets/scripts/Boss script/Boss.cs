using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{


    public int health;
    public int damage;
    public Animator camAnim;
    public Slider healthBar;
    private Animator anim;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDamage", false);
        
    }

    // Update is called once per frame
    void Update()
    {
       //  toma el valor de slice declarado
        healthBar.value = health;
       
        if (healthBar.value== 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisiona al boss");
        if (collision.gameObject.tag == "Bala")
        {
            Debug.Log("daño al boss");
            anim.SetBool("isDamage", true);
            // para poder restar el canvas que contiene el valor maximo del slice debe ser igual al valorde health que agregemos
           health -= damage;
           
        }
         
    }

    
}
