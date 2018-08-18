using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpjump : MonoBehaviour
{
    public float force;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D colisor)
    {

        if (colisor.gameObject.tag == "Player")
        {
            //var player = colisor.gameObject.transform.GetComponentInChildren<hp>();
            //player.lose_life();
            colisor.gameObject.GetComponent<Rigidbody2D>().Sleep();
            colisor.gameObject.GetComponent<Rigidbody2D>().WakeUp();
            colisor.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * this.force);
        }

    }
}
