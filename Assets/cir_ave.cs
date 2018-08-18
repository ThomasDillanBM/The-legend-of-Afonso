using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cir_ave : MonoBehaviour
{
    public float step;
    public bool esq;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 20f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (esq)
        {
            transform.Translate(Vector2.left * step * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * step * Time.deltaTime);
        }
	}
}
