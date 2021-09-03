using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalCreate : MonoBehaviour
{
    public GameObject PortalTeleport,PortalTeleport1;
    public GameObject PortalEnemy,PortalEnemy1,PortalExit;
    public float timeShowPortal, timeShowPortalEnemy;
    private float timeRandom;
    private bool enemy, enemy1 = false;
    public GameObject ObjectToCreate,ObjectToCreate1;
    public Transform ObjectPosition, ObjectPosition1;
    private GameObject ObjectCreate,ObjectCreate1;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        PortalTeleport.SetActive(false);
        PortalTeleport1.SetActive(false);
        PortalEnemy.SetActive(false);
        PortalEnemy1.SetActive(false);
        CreatePortals();

    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.value == 0)
        {
            PortalEnemy.SetActive(false);
            PortalEnemy1.SetActive(false);
            PortalExit.SetActive(true);
        }

    }

    void CreatePortals()
    {
        if (healthBar.value!=0)
        {
            Invoke("ShowPortal", Random.Range(3, 8));
            Invoke("ShowPortalEnemy", Random.Range(3, 8));
            Invoke("ShowPortalEnemy1", Random.Range(3, 8));
        }
        
    }

    void ShowPortal()
    {
        PortalTeleport.SetActive(true);
        PortalTeleport1.SetActive(true); 
        Invoke("HidePortal", timeShowPortal);
       
  
    }


    void HidePortal()
    {
        PortalTeleport.SetActive(false);
        PortalTeleport1.SetActive(false);
        timeRandom = Random.Range(5, 15);
        Invoke("ShowPortal", timeRandom);
    }
    void ShowPortalEnemy()
    {
        
        PortalEnemy.SetActive(true);
        enemy = true;
        if (healthBar.value != 0)
        {
            CreateEnemy(enemy);
            Invoke("HidePortalEnemy", timeShowPortalEnemy);
        }
    }


    void HidePortalEnemy()
    {
        StopCoroutine(Enemy());
        PortalEnemy.SetActive(false);
        enemy = false;
        timeRandom = Random.Range(5, 15);
        Invoke("ShowPortalEnemy",timeRandom);
    }
    void ShowPortalEnemy1()
    {
        PortalEnemy1.SetActive(true);
        enemy1 = true;
        if (healthBar.value != 0)
        {
            CreateEnemy1(enemy1);
            Invoke("HidePortalEnemy1", timeShowPortalEnemy);
        }
    }


    void HidePortalEnemy1()
    {
        StopCoroutine(Enemy1());
        PortalEnemy1.SetActive(false);
        enemy1 = false;
        timeRandom = Random.Range(5, 15);
        Invoke("ShowPortalEnemy1",timeRandom);
    }

    public void CreateEnemy(bool enemy)
    {
        if (enemy)
        {
            StartCoroutine(Enemy());

        }
    }
   public void CreateEnemy1(bool enemy)
    {
        if (enemy1)
        {
            
            StartCoroutine(Enemy1());

        }
    }
    IEnumerator Enemy()
    {
        for (int i = 0; i < timeShowPortalEnemy; i++)
        {
            ObjectCreate = Instantiate(ObjectToCreate, ObjectPosition.transform);
            yield return new WaitForSeconds(0.5f);

        }
       
    }
    IEnumerator Enemy1()
    {
        for (int i = 0; i < timeShowPortalEnemy; i++)
        {
            ObjectCreate1 = Instantiate(ObjectToCreate1, ObjectPosition1.transform);
            yield return new WaitForSeconds(0.5f);
        }
    }

    

    
}
