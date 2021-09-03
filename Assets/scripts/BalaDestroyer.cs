using UnityEngine;
using System.Collections;

public class BalaDestroyer : MonoBehaviour
{

    void OnBecameInvisible()
    {
        //Destruimos el objeto padre cuando salga fuera de la pantalla
        //Destroy(transform.parent.gameObject);
        if (gameObject.name == "bala(Clone)")
        {
            Destroy(gameObject, 1f);
        }
    }
}
