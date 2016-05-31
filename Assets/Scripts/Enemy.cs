using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform player;
	public float enemyField;
	public bool isActive;
	public bool inRange;
	public bool canDestroyYou;
	
	private Rigidbody rb;
    private NavMeshAgent myAgent;
	private float timerAttack;
   // public Transform destination;
   // public float hearRange;


	// Use this for initialization
	void Awake () {
        myAgent = GetComponent<NavMeshAgent>();
		rb= GetComponent<Rigidbody>();

	}

	void Start(){
		isActive=false;
		inRange=false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
		Debug.Log ("inRange: "+inRange);
		Debug.Log ("timerAttack: "+timerAttack);
        MoveTo();
		if(inRange==true){
			Attack();
		}
    }

    void MoveTo() {
	/*	if(rb.velocity==Vector3.zero){
			isActive=false;
		}*/
		if(isActive==true && (player.position.x-transform.position.x) < enemyField || isActive==true && (player.position.z-transform.position.z) < enemyField ){
			myAgent.SetDestination(player.position);
		}
		if((player.position.x-transform.position.x) > enemyField || (player.position.z-transform.position.z) > enemyField){
			myAgent.SetDestination(transform.position);
		}
      /*  if (GameObject.FindWithTag("Sound") && (GameObject.FindWithTag("Sound").transform.position.x- gameObject.transform.position.x)< hearRange || GameObject.FindWithTag("Sound") && (GameObject.FindWithTag("Sound").transform.position.z - gameObject.transform.position.z) < hearRange) {
            agent.SetDestination(GameObject.FindWithTag("Sound").transform.position); 
        }
        */
    }

	void Attack(){

		timerAttack+=Time.deltaTime;
		if(/*attackDelay*/3 < timerAttack && timerAttack < 4 /*attackTime*/){
			transform.Translate(Vector3.zero*Time.deltaTime);
			canDestroyYou=true;
		}
		if(timerAttack>4 /*attackTime*/){
			timerAttack=0;
		}
		if(canDestroyYou==true && inRange==true){
			player.gameObject.SetActive(false);
		}

	}

    void OnTriggerEnter(Collider trig) {
        if (trig.gameObject.tag == "Sound"){
			isActive=true;
            // agent.SetDestination(trig.gameObject.transform.position);
			myAgent.SetDestination(trig.gameObject.transform.position);

			if((player.position.x-transform.position.x) < enemyField || (player.position.z-transform.position.z) < enemyField){
				myAgent.SetDestination(player.position);				
			}
        }
		if(trig.gameObject.tag=="LowerEdge"){
			this.gameObject.SetActive (false);
		}

    }
	public void SetInRange(bool condition){
		if(condition==true)
			inRange=true;
		if(condition==false){
			inRange=false;
			timerAttack=0;
		}
	}

	/*public void Destroy(){
		Destroy (gameObject);
	}
*/
}
