using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumponlyright : MonoBehaviour {
    public float force;
    public int direction;
    public const int RIGHT = 1, LEFT = 2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D colisor)
    {

        if (colisor.gameObject.tag == "Player")
        {
            if (this.direction == RIGHT)
            {
                colisor.gameObject.GetComponent<Rigidbody2D>().Sleep();
                colisor.gameObject.GetComponent<Rigidbody2D>().WakeUp();
                colisor.gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.right) * this.force);
            }
            else
            {
                colisor.gameObject.GetComponent<Rigidbody2D>().Sleep();
                colisor.gameObject.GetComponent<Rigidbody2D>().WakeUp();
                colisor.gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.left) * this.force);
            }
            //var player = colisor.gameObject.transform.GetComponentInChildren<hp>();
            //player.lose_life();

        }

    }
}
