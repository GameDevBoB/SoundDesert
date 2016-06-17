using UnityEngine;
using System.Collections;


public class RockBehaviour : SoundAffected {
	private Rigidbody rb;

	public bool onTheGround;
	public bool isFallObj;
	public bool isColumn;



	// Use this for initialization
	void Start () {
		rb= GetComponent<Rigidbody>();
		rb.isKinematic=true;
		onTheGround=false;



	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate(){
		Debug.Log (rb.velocity);
	}

	void OnTriggerEnter(Collider trig){
		if(trig.gameObject.tag=="Sound" || trig.gameObject.tag == "SoundWave"){
			rb.isKinematic=false;
		}
	}

	void OnCollisionEnter(Collision col){
		if(isFallObj==true){
			if(col.gameObject.tag=="Player" && onTheGround!=true ){
					col.gameObject.SetActive (false);			
			}
			if(col.gameObject.tag=="Enemy" && onTheGround!=true ){
				col.gameObject.SetActive (false);			
			}

		}
		if(isColumn==true){
			if(col.gameObject.tag=="Player" && rb.velocity.y<-1){
				col.gameObject.SetActive (false);			
			}
			if(col.gameObject.tag=="Enemy" && rb.velocity.y<-1){
				col.gameObject.SetActive (false);			
			}
		}
		if(col.gameObject.tag=="Desert"){
			onTheGround=true;


			MakeSound (col.transform.position);
		}
		else{
			onTheGround=false;
		}

	}


}
