using UnityEngine;
using System.Collections;
using System;

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
    public float hitAngle = 30;
    public float hitRange = 10;
    public float checkTime;
    public Color disactiveColor;
    public Color soundCheckColor;
    public Color playerFollowColor;
    public Color attackColor;
    public Color idleColor;

    private NavMeshAgent myAgent;
    private SphereCollider soundTrigger;
    public EnemyState myState;
    private Material myMaterial;
    // public Transform destination;
    // public float hearRange;
    private GameObject player;
    private float startSoundPerceived;
    private float startAttack;
    private RaycastHit hit;
    private bool isAttacking;
    //private bool isActive;





    // Use this for initialization
    void Awake () {
        soundTrigger = GetComponent<SphereCollider>();
        myAgent = GetComponent<NavMeshAgent>();
        myMaterial = GetComponent<MeshRenderer>().material;
        player = GameObject.FindWithTag("Player");
	}

    void Start()
    {
        //isActive = false;
        myState = EnemyState.Disactive;
        SetEmissive(disactiveColor);
        soundTrigger.radius = disactiveSoundPerception / 2;
        isAttacking = false;
        SetEmissive(Color.black);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (myState != EnemyState.Disactive)
        {
            if (myAgent.remainingDistance < 0.5f && !isAttacking)
            {
                myState = EnemyState.Idle;
                SetEmissive(idleColor);
            }
            else
                myAgent.Resume();
            if ((Time.time - startSoundPerceived) > checkTime && !isAttacking)
            {
                CheckIfPlayerInSight();
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    void CheckIfPlayerInSight()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < viewRange && player.activeSelf)
        {
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
            else
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

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Sound" && !isAttacking) {
            if (myState == EnemyState.Disactive)
            {
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
        isAttacking = false;
    }

	public void Destroy(){
		gameObject.SetActive(false);
	}

    private void SetEmissive(Color newColor)
    {
        myMaterial.SetColor("_node_217", newColor);
    }
}
