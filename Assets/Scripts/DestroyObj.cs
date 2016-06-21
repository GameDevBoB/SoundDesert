using UnityEngine;
using System.Collections;

public class DestroyObj : SoundAffected {

    public ParticleSystem myParticle;

    private MeshRenderer myMesh;
    private Collider myCollider;
    private NavMeshObstacle myObstacle;
	
	void Awake()
	{
		myMesh = GetComponent<MeshRenderer> ();
		//myParticle = GetComponent<ParticleSystem> ();
        myCollider = GetComponent<Collider>();
        myObstacle = GetComponent<NavMeshObstacle>();


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
            if (gameObject.tag == "Bridge")
                gameObject.SendMessage("FallEnemy");
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
            if (gameObject.tag == "Bridge")
                gameObject.SendMessage("FallEnemy");
			myMesh.enabled = false;
            myCollider.enabled = false;
            myObstacle.enabled = false;
            if (col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);
			myParticle.Play();
		}
	}
}
