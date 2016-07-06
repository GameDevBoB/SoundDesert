﻿using UnityEngine;
using System.Collections;
using System;
using SoundDesertLibrary;

public enum EnemyState
{
    Disactive,
    SoundCheck,
    PlayerFollow,
    Attack,
    Repair,
    Idle
};

public class Enemy : SoundAffected {
    public AudioClip activationSound;
    public AudioClip attackSound;
    //public AudioClip moveSound;
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
    public ParticleSystem myAttackParticle1;
    public ParticleSystem myAttackParticle2;
    public ParticleSystem myAttackParticle3;
    public GameObject myRepairParticle;
    public Transform attackPosition;

    public AudioSource mySource;
    private NavMeshAgent myAgent;
    private SphereCollider soundTrigger;
    public EnemyState myState;
    private Material myMaterial;
    private Material[] bodyMaterials;
    // public Transform destination;
    // public float hearRange;
    private GameObject player;
    private Rigidbody playerRB;
    private Player playerScript;
    private float startSoundPerceived;
    private float startAttack;
    private RaycastHit hit;
    private bool isAttacking;
    private bool isRepairing;
    private GameObject soundEmitObj;
    private Animator anim;

    //private bool isActive;





    // Use this for initialization
    void Awake () {
        //generalSource = GameObject.Find("general source").GetComponent<AudioSource>();
        //mySource = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        soundTrigger = GetComponent<SphereCollider>();
        myAgent = GetComponent<NavMeshAgent>();
        myMaterial = GetComponent<MeshRenderer>().material;
        bodyMaterials = new Material[ transform.GetChild(0).childCount];
        for(int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            bodyMaterials[i] = transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>().material;
        }
        //Debug.Log(myMaterial.name);
        player = GameObject.FindWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
        playerScript = player.GetComponent<Player>();
        Physics.queriesHitTriggers = true;
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
        myAttackParticle1.gameObject.transform.position = attackPosition.position;
    }
	
	// Update is called once per frame
	void Update ()
    {

        
        //Debug.DrawRay(transform.position, -transform.up, Color.red, 100);
        //Debug.Log(Physics.Raycast(transform.position + transform.up * 3, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer("Bridge")));
        if (myAgent.enabled)
        {
            if (myState != EnemyState.Disactive)
            {
                //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Activation"));
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Activation"))
                    myAgent.Resume();
                if ((myAgent.remainingDistance <= (myAgent.stoppingDistance + 1f) && myAgent.pathStatus == NavMeshPathStatus.PathComplete) && myState != EnemyState.Attack)
                {

                    if (soundEmitObj != null && soundEmitObj.layer == LayerMask.NameToLayer("Repairable"))
                    {
                        if (!isRepairing)
                        {
                            myState = EnemyState.Repair;
                            SetEmissive(repairColor);
                            soundEmitObj.SendMessage("Repair");
                            myRepairParticle.SetActive(true);
                            //Debug.Log(myState);
                            isRepairing = true;
                            myAgent.Stop();
                        }
                        //transform.LookAt(new Vector3(soundObj.transform.position.x, transform.position.y, soundObj.transform.position.z));
                        if (soundEmitObj.layer != LayerMask.NameToLayer("Repairable"))
                        {
                            myRepairParticle.SetActive(false);
                            //Debug.Log("spengo il particellare");
                            myAgent.Resume();
                            myState = EnemyState.Idle;
                            SetEmissive(idleColor);
                            isRepairing = false;
                            soundEmitObj = null;
                        }
                    }
                    if (soundEmitObj != null && soundEmitObj.layer != LayerMask.NameToLayer("Repairable"))
                    {
                        myRepairParticle.SetActive(false);
                        //Debug.Log("spengo il particellare");
                        myAgent.Resume();
                        myState = EnemyState.Idle;
                        SetEmissive(idleColor);
                        isRepairing = false;
                        soundEmitObj = null;
                    }
                    else
                    {
                        //myRepairParticle.SetActive(false);
                        myState = EnemyState.Idle;
                        SetEmissive(idleColor);
                    }
                }
                else
                {
                    myAgent.Resume();
                    //generalSource.transform.position = transform.position;
                }
                if ((Time.time - startSoundPerceived) > checkTime && myState != EnemyState.Attack)
                {
                    CheckIfPlayerInSight();
                }
                if (myState == EnemyState.Idle)
                    CheckIfRepairable();
            }
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
                //Debug.Log("Giocatore in vista! Attacco");
                myState = EnemyState.Attack;
                SetEmissive(attackColor);
                myAgent.Stop();
                if (((Time.time - startAttack) > attackDelay || startAttack == 0) && !isAttacking)
                {
                    startAttack = Time.time;
                    StartCoroutine("Attack");
                }
            }
            else if (playerScript.isMoving)
            {
                myAgent.Resume();
                myState = EnemyState.PlayerFollow;
                SetEmissive(playerFollowColor);
                myAgent.SetDestination(player.transform.position);
            }
        }
        else
            myState = EnemyState.Idle;
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
        //Debug.Log("Controllo se posso riparare qualcosa");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, repairRange, 1 << LayerMask.NameToLayer("Repairable"));
        if (hitColliders.Length > 0)
        {
            Vector3 direction = Vector3.zero;
            if (hitColliders[0].gameObject.tag == "Column")
            {
                soundEmitObj = hitColliders[0].transform.parent.gameObject;
                direction = soundEmitObj.transform.position;
            }
            else if (hitColliders[0].gameObject.tag == "Capital")
            {
                soundEmitObj = hitColliders[0].gameObject;
                direction = soundEmitObj.GetComponent<Renderer>().bounds.center;
            }

            //Debug.Log(Physics.Linecast(transform.position, direction, 1 << LayerMask.NameToLayer("Floor")));
            //Debug.DrawLine(direction, transform.position, Color.red, 1);

            if (!Physics.Linecast(transform.position, direction, 1 << LayerMask.NameToLayer("Floor")))
            {
                myState = EnemyState.SoundCheck;
                SetEmissive(soundCheckColor);


                //Debug.Log("vado a riparare");
                //Debug.DrawLine(direction, transform.position, Color.red, 1);

                myAgent.Resume();
                //Debug.Log("posizione soundobj " + soundObj.transform.position + " nome " + soundObj.name);
                myAgent.SetDestination(direction);
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Sound" && !isAttacking)
        {
            if (myState == EnemyState.Disactive)
            {
                //generalSource.gameObject.transform.position = transform.position;
                AudioLib.GeneralSound(activationSound, mySource);
                anim.SetBool("canActivate", true);
                myAgent.enabled = true;
                //GetComponent<Rigidbody>().isKinematic = true;
                soundTrigger.radius = activeSoundPerception / 2;
            }
            if (myAgent.enabled)
            {
                myState = EnemyState.SoundCheck;
                SetEmissive(soundCheckColor);
                startSoundPerceived = Time.time;
                //myAgent.Resume();
                myAgent.SetDestination(col.gameObject.transform.position);
                myAgent.Stop();
            }
            //myAgent.destination = col.gameObject.transform.position;
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("canAttack", true);
        isAttacking = true;
        GetComponent<Rigidbody>().isKinematic = true;
        myAgent.enabled = false;
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
                myAttackParticle1.Play();
                myAttackParticle2.Play();
                myAttackParticle3.Play();
                MakeSound(attackPosition.position);
                //generalSource.gameObject.transform.position = transform.position;
                AudioLib.GeneralSound(attackSound, mySource);
            }
            //Debug.Log("Sto attaccando!" + attackTimer);
            yield return new WaitForSeconds(0.01f);
            anim.SetBool("canAttack", false);
        }
        myState = EnemyState.Idle;
        SetEmissive(idleColor);
        isAttacking = false;
        myAgent.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

	public void GetDamage(){
        anim.SetBool("isDead", true);
		gameObject.SetActive(false);
	}

    public void Fall()
    {
        
        if (Physics.Raycast(transform.position + transform.up * 3, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer("Bridge")))
        {
            
            //Debug.Log("Sto cadendo " + hit.transform.gameObject.name);
            myAgent.enabled = false;

            Invoke("GetDamage", 5);
        }
    }

    private void SetEmissive(Color newColor)
    {
        myMaterial.SetColor("_color_emissive", newColor);
        foreach (Material bodypart in bodyMaterials)
            bodypart.SetColor("_color_emissive", newColor);
    }
}
