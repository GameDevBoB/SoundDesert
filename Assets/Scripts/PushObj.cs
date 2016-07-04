using UnityEngine;
using System.Collections;

public class PushObj : SoundAffected {
	//public Vector3 fallRotation;
	public float pushForce;

    //private bool hasPulled;
	private Rigidbody rb;
    private bool isBlocked;
	
	// Use this for initialization
	void Awake () {
		//hasPulled = false;
		rb = GetComponent<Rigidbody> (); 
	}
	
    void Start()
    {
        isBlocked = false;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Cube"), true);
    }

	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "SoundWave" && !isBlocked) {
            //transform.RotateAround (transform.position, transform.right, -90);
            Vector3 pushVector = new Vector3(col.transform.forward.x, 0, col.transform.forward.z);
			rb.AddForce(transform.position + pushVector * pushForce);
                MakeSound(col.transform.position);
			//hasPulled = true;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "SoundWave" && !isBlocked) {
            //Debug.Log(col.gameObject.transform.position);
            //transform.RotateAround (transform.position, transform.right, -90);
            Vector3 pushVector = new Vector3(col.transform.forward.x, 0, col.transform.forward.z);
            rb.AddForce(transform.position + pushVector * pushForce);
            MakeSound(col.transform.position);
			//hasPulled = true;
		}
	}

    public void Block()
    {
        //isBlocked = true;
        rb.velocity = Vector3.zero;
    }
}
