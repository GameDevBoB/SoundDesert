using UnityEngine;
using System.Collections;
using System;

public enum EnemyState
{
    Disactive,
    SoundCheck,
    PlayerFollow,
    Attack,
    Repair,
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
    public float hitAngle = 30;
    public float hitRange = 10;
    public float checkTime;
    public float repairRange;
    public Color disactiveColor;
    public Color soundCheckColor;
    public Color playerFollowColor;
    public Color attackColor;
    public Color idleColor;
    public Color repairColor;

    private NavMeshAgent myAgent;
    private SphereCollider soundTrigger;
    public EnemyState myState;
    private Material myMaterial;
    // public Transform destination;
    // public float hearRange;
    private GameObject player;
    private Rigidbody playerRB;
    private float startSoundPerceived;
    private float startAttack;
    private RaycastHit hit;
    private bool isAttacking;
    private GameObject soundObj;
    //private bool isActive;





    // Use this for initialization
    void Awake () {
        soundTrigger = GetComponent<SphereCollider>();
        myAgent = GetComponent<NavMeshAgent>();
        myMaterial = GetComponent<MeshRenderer>().material;
        player = GameObject.FindWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
	}

    void Start()
    {
        //isActive = false;
        myState = EnemyState.Disactive;
        SetEmissive(disactiveColor);
        soundTrigger.radius = disactiveSoundPerception / 2;
        isAttacking = false;
        SetEmissive(Color.black);
        myAgent.enabled = false;
        myAgent.stoppingDistance = myAgent.radius + 0.5f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (myState != EnemyState.Disactive)
        {
            if ((myAgent.remainingDistance <= myAgent.stoppingDistance && myAgent.pathStatus == NavMeshPathStatus.PathComplete) && myState != EnemyState.Attack)
            {
                Debug.Log(myState);
                if (myState == EnemyState.Repair)
                {
                    Debug.Log(soundObj.name);
                    soundObj.SendMessage("Repair");
                }
                myState = EnemyState.Idle;
                SetEmissive(idleColor);
            }
            else
            {
                myAgent.Resume();
            }
            if ((Time.time - startSoundPerceived) > checkTime && myState != EnemyState.Attack)
            {
                CheckIfPlayerInSight();
            }
            if (myState == EnemyState.Idle)
                CheckIfRepairable();
        }
    }

    void FixedUpdate()
    {
        
    }

    void CheckIfPlayerInSight()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < viewRange)
        {
            //Debug.Log(CheckIfPlayerInAttackRange());
            if (CheckIfPlayerInAttackRange())
            {
                Debug.Log("Giocatore in vista! Attacco");
                myState = EnemyState.Attack;
                SetEmissive(attackColor);
                myAgent.Stop();
                if (((Time.time - startAttack) > attackDelay || startAttack == 0) && !isAttacking)
                {
                    startAttack = Time.time;
                    StartCoroutine("Attack");
                }
            }
            else if(playerRB.velocity.magnitude != 0)
            {
                myAgent.Resume();
                myState = EnemyState.PlayerFollow;
                SetEmissive(playerFollowColor);
                myAgent.SetDestination(player.transform.position);
            }
        }
    }

    bool CheckIfPlayerInAttackRange()
    {
        Vector3 rayDirection = player.transform.position - transform.position;
        //Debug.Log("Distanza " + Physics.Raycast(transform.position, rayDirection, out hit, attackRange, 1 << LayerMask.NameToLayer("Player")));
        //Debug.Log("Angolo " + (Vector3.Angle(rayDirection, transform.forward) < attackAngle) + " " + Vector3.Angle(rayDirection, transform.forward));
        bool isPlayerInSight = (Physics.Raycast(transform.position, rayDirection, out hit, attackRange, 1 << LayerMask.NameToLayer("Player")) && (Vector3.Angle(rayDirection, transform.forward) < attackAngle)
                                && !(Physics.Raycast(transform.position, rayDirection, out hit, attackRange, 1 << LayerMask.NameToLayer("Obstacle"))));
        return isPlayerInSight;
    }

    bool CheckIfPlayerHit()
    {
        Vector3 rayDirection = player.transform.position - transform.position;
        bool isPlayerHit = (Physics.Raycast(transform.position, rayDirection, out hit, hitRange, 1 << LayerMask.NameToLayer("Player")) && (Vector3.Angle(rayDirection, transform.forward) < hitAngle));
        return isPlayerHit;
    }

    void CheckIfRepairable()
    {
        Debug.Log("Controllo se posso riparare qualcosa");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, repairRange, 1 << LayerMask.NameToLayer("Repairable"));
        if(hitColliders.Length > 0)
        {
            myState = EnemyState.Repair;
            SetEmissive(repairColor);
            soundObj = hitColliders[0].transform.parent.gameObject;
            myAgent.Resume();
            myAgent.SetDestination(soundObj.transform.position);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Sound" && !isAttacking) {
            if (myState == EnemyState.Disactive)
            {
                myAgent.enabled = true;
                GetComponent<Rigidbody>().isKinematic = true;
                soundTrigger.radius = activeSoundPerception / 2;
            }
            myState = EnemyState.SoundCheck;
            SetEmissive(soundCheckColor);
            startSoundPerceived = Time.time;
            myAgent.Resume();
            myAgent.SetDestination(col.gameObject.transform.position);
            //myAgent.destination = col.gameObject.transform.position;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        float attackTimer = 0;
        while(attackTimer < attackTime)
        {
            attackTimer += 0.01f;
            if (Math.Round(attackTimer,2) == (attackTime/2))
            {
                //bool alreadyDamaged = true;
                if (CheckIfPlayerHit())
                {
                    player.SendMessage("GetDamage");
                    myState = EnemyState.Idle;
                    SetEmissive(idleColor);
                    //Debug.Log("Giocatore colpito!");
                }
            }
            //Debug.Log("Sto attaccando!" + attackTimer);
            yield return new WaitForSeconds(0.01f);
        }
        myState = EnemyState.Idle;
        SetEmissive(idleColor);
        isAttacking = false;
    }

	public void GetDamage(){
		gameObject.SetActive(false);
	}

    private void SetEmissive(Color newColor)
    {
        myMaterial.SetColor("_node_217", newColor);
    }
}
