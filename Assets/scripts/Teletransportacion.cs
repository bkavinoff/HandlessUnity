using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransportacion : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    public bool canTeleport;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && canTeleport)
        {
            StartCoroutine(Teleport());
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            canTeleport = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            canTeleport = false;
        }
    }

    public IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.2f);
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
        StopCoroutine(Teleport());
    }
}
