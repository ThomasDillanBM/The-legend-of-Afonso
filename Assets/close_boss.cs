using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_boss : MonoBehaviour
{
    public GameObject wall, boss;
    private bool active;
    
	// Use this for initialization
	void Start ()
    {
        active = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !active)
        {
            wall.gameObject.SetActive(true);
            active = true;
            boss.SetActive(true);
        }
    }
}
