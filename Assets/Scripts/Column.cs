using UnityEngine;
using System.Collections;

public class Column : SoundAffected {
    //public Vector3 fallRotation;
    
    public GameObject mymesh;
	public GameObject columnChild;
	public float fallTime=2;
	public float fallDistance=1;
	public bool isBridge;

    //private Material elementMat;
    private float hiddenColumn = 0.2f;
    private float visibleColumn = 1f;
	private float lerpTime;
	//private Color prevColor;
    private bool hasFallen;
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
    }

	// Use this for initialization
	void Start () {
		hasFallen = false;
		isFalling=false;
		isRebuilding=false;
		initialRot=transform.GetChild (1).eulerAngles;
		initialPos=transform.GetChild (1).position;
	}
	void Update(){

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
	/*	if(isRebuilding==true){

			Rebuild();

		}*/

	}

    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
            if (!hasFallen)
            {
				isFalling=true;
                //transform.RotateAround(transform.position, transform.right, -90);
			//	Quaternion.Slerp(transform.rotation,Quaternion.Euler(-90,transform.rotation.y,transform.rotation.z),1);
                MakeSound(col.transform.position);
                hasFallen = true;

            }
            else if (col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);
            
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
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
        {
            if (!hasFallen)
            {
				isFalling=true;
			
                MakeSound(col.transform.position);
                
            }
            else if (col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);

        }
		if (col.gameObject.tag == "Enemy" && rb.velocity!=Vector3.zero) {
            col.gameObject.SendMessage("GetDamage");

        }

    }
	void Fall(){

		if(isRebuilding==false){

			if (lerpTime < 1  )
			{
				lerpTime += Time.deltaTime/fallTime;

				columnChild.transform.eulerAngles= Vector3.Lerp( initialRot , new Vector3(initialRot.x-90,initialRot.y,initialRot.z),(lerpTime));
				columnChild.transform.position= Vector3.Lerp (initialPos , new Vector3( initialPos.x+fallDistance,initialPos.y,initialPos.z),(lerpTime));
				if(!isBridge){
				columnChild.transform.position= Vector3.Lerp (initialPos , new Vector3( initialPos.x+fallDistance,initialPos.y+1,initialPos.z),(lerpTime));
				}
			}
			if(lerpTime>1){
				isFalling=false;
				hasFallen = true;
				rb.useGravity=true;
				lerpTime=0;

			}
		}
			

	}
	public void Rebuild(){
			if(isFalling==false){
				if(hasFallen==true){
					Debug.Log ("sono dentro");
					
					
					if (lerpTime < 1  )
					{
						lerpTime += Time.deltaTime/fallTime;//((Time.time - startLerpTime) / lerptime);
						
						columnChild.transform.eulerAngles= Vector3.Lerp( new Vector3(initialRot.x-90,initialRot.y,initialRot.z) , initialRot,(lerpTime));
					columnChild.transform.position= Vector3.Lerp (new Vector3( initialPos.x+fallDistance,initialPos.y,initialPos.z) ,initialPos ,(lerpTime));
						columnChild.transform.position= Vector3.Lerp (new Vector3( initialPos.x+fallDistance,initialPos.y+1f,initialPos.z) ,initialPos ,(lerpTime));
						//Quaternion.Lerp(transform.rotation,Quaternion.Euler(-90,transform.rotation.y,transform.rotation.z),(exitTime));
					}
					if(lerpTime>1){

						isRebuilding=false;

						hasFallen=false;

						rb.useGravity=false;
						lerpTime=0;
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
