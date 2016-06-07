using UnityEngine;
using System.Collections;

public class DestroyObj : SoundAffected {

    private MeshRenderer myMesh;
	private ParticleSystem myParticle;
    private Collider myCollider;
    private NavMeshObstacle myObstacle;
	
	void Awake()
	{
		myMesh = GetComponent<MeshRenderer> ();
		myParticle = GetComponent<ParticleSystem> ();
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
		if (!myMesh.enabled && !myParticle.isPlaying)
			gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
            myMesh.enabled = false;
            myCollider.enabled = false;
            myObstacle.enabled = false;
            if(col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);
            myParticle.Play();
        }

	}
	
	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
			myMesh.enabled = false;
            myCollider.enabled = false;
            myObstacle.enabled = false;
            if (col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);
			myParticle.Play();
		}
	}
}
