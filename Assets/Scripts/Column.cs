﻿using UnityEngine;
using System.Collections;
using SoundDesertLibrary;

public class Column : SoundAffected {
    //public Vector3 fallRotation;
    public AudioClip fallSound;
    public AudioClip rebuildingSound;
    public AudioClip fallenSound;   //inteso come suono per quando viene colpita quando è a terra
    public GameObject mymesh;
	public GameObject columnChild;
	public float fallTime=2;
	public float fallDistance;
	public bool isBridge;
    public bool hasFallen;
    public GameObject[] bridgeObstacles;
    public GameObject bridgeCollider;
    public ParticleSystem myParticle;
    public Transform fallPosition;
    public AudioSource mySource;

    //private Material elementMat;
    private float hiddenColumn = 0.2f;
    private float visibleColumn = 1f;
	private float lerpTime;
	//private Color prevColor;
	private bool isFalling;
	private bool isRebuilding;
	private Rigidbody rb;
	private Vector3 initialRot;
	private Vector3 initialPos;
   


    void Awake()
    {
        //elementMat = mymesh.GetComponent<MeshRenderer>().material;
        //prevColor = elementMat.color;
		rb = GetComponent<Rigidbody> ();
        //mySource = gameObject.GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
		//hasFallen = false;
		isFalling=false;
		isRebuilding=false;
		initialRot=transform.GetChild (1).localEulerAngles;
		initialPos=transform.GetChild (1).localPosition;
        if (hasFallen)
        {
            columnChild.transform.localEulerAngles = initialRot - Vector3.right * 90;
            columnChild.transform.localPosition = initialPos - Vector3.forward * fallDistance * 1 / transform.localScale.x - Vector3.up / 2 * 1 / transform.localScale.y;
            if (!isBridge)
            {
                columnChild.transform.localPosition = initialPos - Vector3.forward * fallDistance * 1 / transform.localScale.x + Vector3.up / 2 * 1 / transform.localScale.y;
            }
            columnChild.layer = LayerMask.NameToLayer("Repairable");
            gameObject.layer = LayerMask.NameToLayer("Repairable");
        }
        bridgeCollider.SetActive(false);
        //myParticle.Play();
    }
	void Update(){
        Debug.Log(mySource.volume + " cane");
	/*	Debug.Log ("isRebuilding: "+ isRebuilding);
		Debug.Log ("hasFallen: "+ hasFallen);
		Debug.Log ("lerpTime: "+lerpTime);
	*/
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isFalling==true){
			Fall ();

		}
        if (Input.GetKeyDown(KeyCode.R))
            Repair();
		if(isRebuilding==true){

			Rebuild();

		}

	}

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.name);
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
            if (!hasFallen)
            {
                isFalling = true;
                //Debug.Log("faccio suono");
                //transform.RotateAround(transform.position, transform.right, -90);
                //	Quaternion.Slerp(transform.rotation,Quaternion.Euler(-90,transform.rotation.y,transform.rotation.z),1);
                //MakeSound(col.transform.position);
                hasFallen = true;
                
            }
            else if (col.gameObject.tag == "SoundWave")
            {
                //MakeSound(col.transform.position);
                Debug.Log("faccio suono");
                AudioLib.GeneralSound(fallenSound, mySource);
            }
            
        }
		if (col.gameObject.tag == "Enemy" && rb.velocity!=Vector3.zero) {

            col.gameObject.SendMessage("GetDamage");
        }
	/*	if(col.gameObject.tag=="Player"){
			isRebuilding=true;
			Debug.Log ("ti ho toccato");
		}*/
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.gameObject.name);
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
        {
            if (!hasFallen)
            {
				isFalling=true;

                //Debug.Log("faccio suono");
                //MakeSound(col.transform.position);
                
            }
            else if (col.gameObject.tag == "SoundWave")
            {
                //MakeSound(col.transform.position);
                //Debug.Log("faccio suono");
            }

        }
		if (col.gameObject.tag == "Enemy" && rb.velocity!=Vector3.zero) {
            col.gameObject.SendMessage("GetDamage");

        }

    }
	void Fall(){

		if(isRebuilding==false){
            //myParticle.Stop();
			if (lerpTime < 1  )
			{
				lerpTime += Time.deltaTime/fallTime;
                //Debug.Log("Sto cadendo " + fallDistance);
				columnChild.transform.localEulerAngles= Vector3.Lerp( initialRot , initialRot-Vector3.right * 90,(lerpTime));
				columnChild.transform.localPosition= Vector3.Lerp (initialPos , initialPos - Vector3.forward *fallDistance * 1/transform.localScale.x - Vector3.up/2 * 1 / transform.localScale.y, (lerpTime));
				if(!isBridge){
				columnChild.transform.localPosition= Vector3.Lerp (initialPos , initialPos - Vector3.forward * fallDistance *1/transform.localScale.x + Vector3.up/2 * 1 / transform.localScale.y, (lerpTime));
				}
			}
			if(lerpTime>1){
				isFalling=false;
				hasFallen = true;
				rb.useGravity=true;
                myParticle.Stop();
                columnChild.layer = LayerMask.NameToLayer("Repairable");
                gameObject.layer = LayerMask.NameToLayer("Repairable");
                MakeSound(fallPosition.position);
                foreach (GameObject bridgeObstacle in bridgeObstacles)
                    bridgeObstacle.SetActive(false);
                lerpTime =0;

			}
            AudioLib.GeneralSound(fallSound, mySource);
        }
			

	}

    public void Repair()
    {
        
        isRebuilding = true;
        foreach (GameObject bridgeObstacle in bridgeObstacles)
            bridgeObstacle.SetActive(true);
    }

    public void Rebuild(){
			if(isFalling==false){
				if(hasFallen==true){
					//Debug.Log ("sono dentro");
                

                if (lerpTime < 1  )
					{
						lerpTime += Time.deltaTime/fallTime;//((Time.time - startLerpTime) / lerptime);
						
						columnChild.transform.localEulerAngles= Vector3.Lerp(initialRot - Vector3.right * 90, initialRot,(lerpTime));
					columnChild.transform.localPosition= Vector3.Lerp (initialPos - Vector3.forward * fallDistance * 1 / transform.localScale.x - Vector3.up / 2 * 1 / transform.localScale.y, initialPos ,(lerpTime));
                    if (!isBridge)
                        columnChild.transform.localPosition= Vector3.Lerp (initialPos - Vector3.forward * fallDistance * 1 / transform.localScale.x + Vector3.up / 2 * 1 / transform.localScale.y, initialPos ,(lerpTime));
						//Quaternion.Lerp(transform.rotation,Quaternion.Euler(-90,transform.rotation.y,transform.rotation.z),(exitTime));
					}
					if(lerpTime>1){

						isRebuilding=false;

						hasFallen=false;

						rb.useGravity=false;

                    myParticle.Play();

                    columnChild.layer = LayerMask.NameToLayer("Default");
                    gameObject.layer = LayerMask.NameToLayer("Default");
                    AudioLib.GeneralSound(rebuildingSound, mySource);

                    lerpTime =0;
					}
				}
			}


	}



    /*public void GetAlpha()
    {
        prevColor.a = hiddenColumn;
        elementMat.color = prevColor;
    }

    public void BackToNormalAlpha()
    {
        prevColor.a = visibleColumn;
        elementMat.color = prevColor;
    }*/


}
