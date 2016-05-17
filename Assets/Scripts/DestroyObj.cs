using UnityEngine;
using System.Collections;

public class DestroyObj : MonoBehaviour {
	private MeshRenderer mymesh;
	private ParticleSystem myparticle;
	
	void Awake()
	{
		mymesh = GetComponent<MeshRenderer> ();
		myparticle = GetComponent<ParticleSystem> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!mymesh.enabled && !myparticle.isPlaying)
			gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Sound") {
			mymesh.enabled = false;
			myparticle.Play();
		}

	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Sound") {
			mymesh.enabled = false;
			myparticle.Play();
		}
	}
}
