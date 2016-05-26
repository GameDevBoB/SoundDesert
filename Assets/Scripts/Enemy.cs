using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    private NavMeshAgent myAgent;
   // public Transform destination;
   // public float hearRange;


	// Use this for initialization
	void Awake () {
        myAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        MoveTo();
    }

    void MoveTo() {
        
      /*  if (GameObject.FindWithTag("Sound") && (GameObject.FindWithTag("Sound").transform.position.x- gameObject.transform.position.x)< hearRange || GameObject.FindWithTag("Sound") && (GameObject.FindWithTag("Sound").transform.position.z - gameObject.transform.position.z) < hearRange) {
            agent.SetDestination(GameObject.FindWithTag("Sound").transform.position); 
        }
        */
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Sound") {
            // agent.SetDestination(trig.gameObject.transform.position);
            myAgent.destination = col.gameObject.transform.position;
        }
    }

	public void Destroy(){
		Destroy (gameObject);
	}
}
