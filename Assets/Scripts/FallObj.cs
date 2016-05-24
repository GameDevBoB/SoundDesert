using UnityEngine;
using System.Collections;

public class FallObj : MonoBehaviour {

    public GameObject soundObj;

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

    void MakeSound()
    {
        soundObj.SetActive(true);
        soundObj.SendMessage("Expand");
    }

    void OnCollisionEnter(Collision col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
			rb.useGravity = true;

        if (col.gameObject.tag == "SoundWave" && rb.useGravity && col.gameObject != soundObj)
            MakeSound();

        if (col.gameObject.tag == "Desert") {
            MakeSound();
        }
	}
	
	void OnTriggerEnter(Collider col)
	{
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
            rb.useGravity = true;

        if (col.gameObject.tag == "SoundWave" && rb.useGravity && col.gameObject != soundObj)
            MakeSound();

        if (col.gameObject.tag == "Desert")
        {
            MakeSound();
        }
    }
}
