using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    private NavMeshAgent agent;
   // public Transform destination;
   // public float hearRange;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
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

    void OnTriggerEnter(Collider trig) {
        if (trig.gameObject.tag == "Sound") {
            // agent.SetDestination(trig.gameObject.transform.position);
            agent.destination = trig.gameObject.transform.position;
        }
    }
}
