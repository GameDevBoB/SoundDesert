using UnityEngine;
using System.Collections;

public class FallObj : SoundAffected {
    private Rigidbody rb;
    private bool hasFallen;

	void Awake()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
        hasFallen = false;
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

        if (col.gameObject.tag == "Desert" && !hasFallen) {
            // MakeSound(col.transform.position);
            /*Vector3 posSound = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            MakeSound(posSound);*/
            Vector3 posSound = col.contacts[0].point;
            MakeSound(posSound);
            hasFallen = true;
        }

		if (col.gameObject.tag == "Enemy" && rb.velocity.magnitude > 1) {

			col.gameObject.SetActive (false);
		}
	}

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
            rb.useGravity = true;

        if (col.gameObject.tag == "SoundWave" && rb.useGravity && col.gameObject != soundObj)
            MakeSound(col.transform.position);

        if (col.gameObject.tag == "Desert" && !hasFallen)
        {
            // MakeSound(col.transform.position);
            /*Vector3 posSound = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            MakeSound(posSound);*/
            Vector3 posSound = col.transform.position;
            MakeSound(posSound);
            hasFallen = true;
        }
        if (col.gameObject.tag == "Enemy" && rb.velocity.magnitude > 0.5f)
        {

            col.gameObject.SetActive(false);
        }
    }
}
