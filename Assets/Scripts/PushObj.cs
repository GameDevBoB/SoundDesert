using UnityEngine;
using System.Collections;
using SoundDesertLibrary;

public class PushObj : SoundAffected {
	//public Vector3 fallRotation;
	public float pushForce;
    public AudioClip sound;

    //private bool hasPulled;
	private Rigidbody rb;
    private bool isBlocked;
    private AudioSource mySource;
	
	// Use this for initialization
	void Awake () {
        //hasPulled = false;
        //generalSource = GameObject.Find("general source").GetComponent<AudioSource>();
        mySource = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody> (); 
	}
	
    void Start()
    {
        isBlocked = false;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Cube"), true);
    }

	// Update is called once per frame
	void Update () {
        mySource.volume = mySource.volume / 2;
	}

    void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "SoundWave" && !isBlocked) {
            //transform.RotateAround (transform.position, transform.right, -90);
            Vector3 pushVector = new Vector3(col.transform.forward.x, 0, col.transform.forward.z);
			rb.AddForce(transform.position + pushVector * pushForce);
                MakeSound(col.transform.position);
            AudioLib.GeneralSound(sound, mySource);
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
            AudioLib.GeneralSound(sound, mySource);
            //hasPulled = true;
        }
	}

    public void Block()
    {
        //isBlocked = true;
        rb.velocity = Vector3.zero;
    }
}
