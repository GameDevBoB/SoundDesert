using UnityEngine;
using System.Collections;

public enum EnemyState
{
    Disactive,
    SoundCheck,
    PlayerFollow,
    Attack,
    Idle
};

public class Enemy : MonoBehaviour {
    public float activeSoundPerception;
    public float disactiveSoundPerception;
    public float viewRange;
    public float attackTime;
    public float attackDelay;
    public float attackAngle = 20;
    public float attackRange = 5;
    public float checkTime;

    private NavMeshAgent myAgent;
    private SphereCollider soundTrigger;
    private EnemyState myState;
    // public Transform destination;
    // public float hearRange;
    private GameObject player;
    private float startSoundPerceived;
    //private bool isActive;


    


	// Use this for initialization
	void Awake () {
        soundTrigger = GetComponent<SphereCollider>();
        myAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
	}

    void Start()
    {
        //isActive = false;
        myState = EnemyState.Disactive;
        soundTrigger.radius = disactiveSoundPerception / 2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (myState != EnemyState.Disactive)
        {
            if ((Time.time - startSoundPerceived) > checkTime)
            {
                CheckIfPlayerInSight();
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    void CheckIfPlayerInSight() {

        if (Vector3.Distance(player.transform.position, transform.position) < viewRange)
            myAgent.SetDestination(player.transform.position);
    }

    void CheckIfPlayerInAttackRange()
    {

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Sound") {
            if (myState == EnemyState.Disactive)
            {
                soundTrigger.radius = activeSoundPerception / 2;
            }
            myState = EnemyState.SoundCheck;
            startSoundPerceived = Time.time;
            myAgent.SetDestination(col.gameObject.transform.position);
            //myAgent.destination = col.gameObject.transform.position;
        }
    }

	public void Destroy(){
		Destroy (gameObject);
	}
}
