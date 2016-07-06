using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SoundDesertLibrary;

public class DestroyObj : SoundAffected {

    public ParticleSystem myParticle;
    public GameObject[] obstacles;
    public AudioClip mySound;

    private MeshRenderer myMesh;
    private Collider myCollider;
    private NavMeshObstacle myObstacle;

    private List<Collider> myChildrenColliders = new List<Collider>();
    private List<MeshRenderer> myChildrenMeshes = new List<MeshRenderer>();
    private AudioSource mySource;
	
	void Awake()
	{
		myMesh = GetComponent<MeshRenderer> ();
		//myParticle = GetComponent<ParticleSystem> ();
        myCollider = GetComponent<Collider>();
        myObstacle = GetComponent<NavMeshObstacle>();
        mySource = GetComponent<AudioSource>();

        if (gameObject.tag == "Bridge")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                myChildrenMeshes.Add(transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
                myChildrenColliders.Add(transform.GetChild(i).gameObject.GetComponent<Collider>());
            }

            foreach (GameObject obstacle in obstacles)
            {
                obstacle.SetActive(false);
            }
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag != "Bridge" && !myMesh.enabled && !myParticle.isPlaying)
			gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
            Debug.Log(mySound.name);
            AudioLib.GeneralSound(mySound, mySource);
            if (gameObject.tag == "Bridge")
            {
                foreach (MeshRenderer child in myChildrenMeshes)
                    child.enabled = false;
                foreach (Collider child in myChildrenColliders)
                    child.enabled = false;
                foreach (GameObject obstacle in obstacles)
                {
                    obstacle.SetActive(true);
                }
            }
            else
            {
                myMesh.enabled = false;
                myCollider.enabled = false;
                myObstacle.enabled = false;
            }
            if (gameObject.tag == "Bridge")
                gameObject.SendMessage("FallEnemy");
            if (gameObject.tag != "Bridge")
                myObstacle.enabled = false;
            if(col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);
            myParticle.Play();
            //AudioLib.GeneralSound(mySound, mySource);
        }

	}
	
	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
            Debug.Log(mySound.name);
            AudioLib.GeneralSound(mySound, mySource);
            if (gameObject.tag == "Bridge")
                gameObject.SendMessage("FallEnemy");
            if (gameObject.tag == "Bridge")
            {
                foreach (MeshRenderer child in myChildrenMeshes)
                    child.enabled = false;
                foreach (Collider child in myChildrenColliders)
                    child.enabled = false;
                foreach (GameObject obstacle in obstacles)
                {
                    obstacle.SetActive(true);
                }
            }
            else
            {
                myMesh.enabled = false;
                myCollider.enabled = false;
            }
            if(gameObject.tag != "Bridge")
            myObstacle.enabled = false;
            if (col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);
			myParticle.Play();
            //AudioLib.GeneralSound(mySound, mySource);
        }
	}
}
