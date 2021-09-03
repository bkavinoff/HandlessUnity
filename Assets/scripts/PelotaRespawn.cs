using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaRespawn : MonoBehaviour
{
    Vector3 originalPos;
    public GameObject Pelota;
    private string triggerRespawn = "CartelRespawnPelota";
    public GameObject PelotaRespawnPosition;
    private bool triggerPrimeraVez;

    // Start is called before the first frame update
    void Start()
    {
        //obtengo la posicion inicial del objeto
        originalPos = Pelota.transform.position;
        triggerPrimeraVez = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && triggerRespawn == "CartelRespawnPelota" && triggerPrimeraVez)
        {
            triggerPrimeraVez = false;
        }
        else
        {
            if (collision.gameObject.tag == "Player" && triggerRespawn == "CartelRespawnPelota" && !triggerPrimeraVez)
            {
                triggerPrimeraVez = false;
                Pelota.GetComponent<Rigidbody2D>().isKinematic = true;
                Pelota.transform.position = PelotaRespawnPosition.transform.position; //originalPos;
            }
        }
    }
}
