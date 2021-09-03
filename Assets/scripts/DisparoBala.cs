using UnityEngine;
using System.Collections;

//Creamos un tipo enumerado para definir la dirección
public enum Direccion { Horizontal, Vertical }

public class DisparoBala : MonoBehaviour {

    //Variables públicas
    public Direccion DireccionArma = Direccion.Horizontal;
    public float Velocidad = 50.0F;

    //Variables privadas
    private Rigidbody2D thisRigidbody;

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Establecemos su velocidad y su dirección
        if (DireccionArma == Direccion.Horizontal)
        {
            //Movemos el arma en horizontal
            thisRigidbody.transform.Translate(new Vector3(Velocidad, 0, 0) * Time.deltaTime);
        }
        else
        {
            //Movemos el arma en vertical
            thisRigidbody.transform.Translate(new Vector3(0, Velocidad, 0) * Time.deltaTime);
        }

        if (gameObject.name == "bala(Clone)")
        {
            Destroy(gameObject, 0.3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemigo")
        {
            //Si el ataque colisiona contra un objeto con el tag 'Enemigo', se decrementan las vidas de dicho enemigo
            //other.gameObject.GetComponent<ComportamientoEnemigo>().Vidas--;

            //Destruimos el objeto cuando colisione contra un enemigo
            //Destroy(gameObject);
        }
    }
}
