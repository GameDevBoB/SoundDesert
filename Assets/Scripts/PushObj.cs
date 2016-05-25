using UnityEngine;
using System.Collections;

public class PushObj : SoundAffected {
	//public Vector3 fallRotation;
	public float pullForce;

    //private bool hasPulled;
	private Rigidbody rb;
	
	// Use this for initialization
	void Awake () {
		//hasPulled = false;
		rb = GetComponent<Rigidbody> (); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject!=soundObj) {
			//transform.RotateAround (transform.position, transform.right, -90);
			rb.MovePosition(transform.position + col.transform.forward * pullForce);
            MakeSound(col.transform.position);
			//hasPulled = true;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
			//Debug.Log(col.gameObject.transform.position);
			//transform.RotateAround (transform.position, transform.right, -90);
			
			rb.MovePosition(transform.position + col.transform.forward * pullForce);
            MakeSound(col.transform.position);
			//hasPulled = true;
		}
	}
}
