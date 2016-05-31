using UnityEngine;
using System.Collections;

public class FallObj : SoundAffected {
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
		//Debug.Log (rb.velocity);
	
	}

    void OnCollisionEnter(Collision col)
	{

		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
			rb.useGravity = true;

		}


        if (col.gameObject.tag == "SoundWave" && rb.useGravity && col.gameObject != soundObj) {
			MakeSound (col.transform.position);

		}

        if (col.gameObject.tag == "Desert") {
            MakeSound(col.transform.position);
        }

		if (col.gameObject.tag == "Enemy" && rb.velocity.magnitude > 0.5) {

			col.gameObject.SetActive (false);
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
            rb.useGravity = true;

        if (col.gameObject.tag == "SoundWave" && rb.useGravity && col.gameObject != soundObj)
            MakeSound(col.transform.position);

        if (col.gameObject.tag == "Desert")
        {
            MakeSound(col.transform.position);
        }
		if (col.gameObject.tag == "Enemy" && rb.velocity.magnitude>0.5f) {

			col.gameObject.SetActive (false);
		}
    }
}
