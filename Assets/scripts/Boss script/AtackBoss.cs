using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AtackBoss : MonoBehaviour
{
    public GameObject gameObjectToCreate;
    public GameObject gameObjectPosition;
    private GameObject gameObjectCreate;
    public static AtackBoss sharedInstance;
    public Slider healthBar;
    private float health;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreatedObject",Random.Range(4,8));

    }

    // Update is called once per frame
    void Update()
    {
        //  toma el valor de slice declarado
        health = healthBar.value;
    }

    public void CreatedObject()
    {
        gameObjectCreate = Instantiate(gameObjectToCreate, gameObjectPosition.transform);
        Invoke("DestroyObject", 3f);
    }


    public void DestroyObject()
    {
        Destroy(gameObjectCreate);
        if (health!=0)
        {
            Invoke("CreatedObject", 5f);
        }
        
    }

  
}
