using UnityEngine;
using System.Collections;

public class FallObj : MonoBehaviour {

	private Rigidbody rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Sound")
			rb.useGravity = true;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Sound")
			rb.useGravity = true;
	}
}
