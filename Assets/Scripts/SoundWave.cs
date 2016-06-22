﻿using UnityEngine;
using System.Collections;

public class SoundWave : MonoBehaviour {

    private float duration;
    //private float startTimeShoot;
    private Rigidbody rb;
    //private Vector3 spawPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        //rb.velocity
    }

    public void Shoot(float[] parameters)
    {
        duration = parameters[1];
        //startTimeShoot = Time.time;
        //rb.AddForce(transform.forward * parameters[0]);
		rb.velocity = transform.forward * parameters [0];
        //Debug.Log(parameters[0]);
        Destroy(this.gameObject, duration);
        //spawPosition = transform.position;
    }

	void OnCollisionEnter(Collision col)
	{
        //Debug.Log(col.gameObject.tag);
		//Debug.Log (col.gameObject.name);
		if(col.gameObject.tag == "SoundAffected" || col.gameObject.tag == "Cube" || col.gameObject.tag == "Bridge" || col.gameObject.tag == "Desert" || col.gameObject.tag == "Column")
			Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider col)
	{
        //Debug.Log(col.gameObject.tag);
        //Debug.Log (col.gameObject.name);
        if (col.gameObject.tag == "SoundAffected" || col.gameObject.tag == "Cube" || col.gameObject.tag == "Bridge" || col.gameObject.tag == "Desert" || col.gameObject.tag == "Column")
			Destroy(this.gameObject);
	}
}
