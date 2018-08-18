using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avenida : MonoBehaviour
{
    public float time;
    private float elapsed;
    public GameObject pref;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= time)
        {
            Instantiate(pref, transform.position, transform.rotation);
            elapsed = 0;
        }
	}
}
