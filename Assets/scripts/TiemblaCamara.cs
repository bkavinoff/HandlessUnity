using UnityEngine;
using System.Collections;

public class TiemblaCamara : MonoBehaviour {

    public Animator AnimCam;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void CamTiembla()
    {
        AnimCam.SetTrigger("Tiembla");
    }
}
