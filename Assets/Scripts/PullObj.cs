using UnityEngine;
using System.Collections;

public class PullObj : MonoBehaviour {
	//public Vector3 fallRotation;
	public float pullForce;
    public GameObject soundObj;

    //private bool hasPulled;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		//hasPulled = false;
		rb = GetComponent<Rigidbody> (); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MakeSound()
    {
        soundObj.SetActive(true);
        soundObj.SendMessage("Expand");
    }

    void OnCollisionEnter(Collision col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject!=soundObj) {
			//transform.RotateAround (transform.position, transform.right, -90);
			rb.MovePosition(transform.position + col.transform.forward * pullForce);
            MakeSound();
			//hasPulled = true;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
			//Debug.Log(col.gameObject.transform.position);
			//transform.RotateAround (transform.position, transform.right, -90);
			
			rb.MovePosition(transform.position + col.transform.forward * pullForce);
            MakeSound();
			//hasPulled = true;
		}
	}
}
