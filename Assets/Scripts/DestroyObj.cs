using UnityEngine;
using System.Collections;

public class DestroyObj : MonoBehaviour {
    public GameObject soundObj;

    private MeshRenderer mymesh;
	private ParticleSystem myparticle;
    private Collider myCollider;
    private NavMeshObstacle myObstacle;
	
	void Awake()
	{
		mymesh = GetComponent<MeshRenderer> ();
		myparticle = GetComponent<ParticleSystem> ();
        myCollider = GetComponent<BoxCollider>();
        myObstacle = GetComponent<NavMeshObstacle>();


	}

    void MakeSound()
    {
        soundObj.SetActive(true);
        soundObj.SendMessage("Expand");
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
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
            mymesh.enabled = false;
            myCollider.enabled = false;
            myObstacle.enabled = false;
            MakeSound();
            myparticle.Play();
        }

	}
	
	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
			mymesh.enabled = false;
            myCollider.enabled = false;
            myObstacle.enabled = false;
            MakeSound();
			myparticle.Play();
		}
	}
}
