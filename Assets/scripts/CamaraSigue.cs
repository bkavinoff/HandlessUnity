using UnityEngine;
using System.Collections;

public class CamaraSigue : MonoBehaviour {
 
    public Transform objetivo;
    public float suavizado = 5f;

    Vector3 desface;

	// Use this for initialization
	void Start () {
        desface = transform.position - objetivo.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 posicionObjetivo = objetivo.position + desface;
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, suavizado * Time.deltaTime);
	}
}
