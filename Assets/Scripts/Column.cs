using UnityEngine;
using System.Collections;

public class Column : SoundAffected {
    //public Vector3 fallRotation;
    
    public GameObject mymesh;
	public GameObject columnChild;
	public float fallTime=2;

    //private Material elementMat;
    private float hiddenColumn = 0.2f;
    private float visibleColumn = 1f;
	private float lerpTime;
	//private Color prevColor;
    private bool hasFallen;
	private bool isFalling;
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
		initialRot=transform.GetChild (1).eulerAngles;
		initialPos=transform.GetChild (1).position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isFalling==true){
			
			if (lerpTime < 1  )
			{
				lerpTime += Time.deltaTime/fallTime;//((Time.time - startLerpTime) / lerptime);

				columnChild.transform.eulerAngles= Vector3.Lerp( initialRot , new Vector3(initialRot.x-90,initialRot.y,initialRot.z),(lerpTime));
				columnChild.transform.position= Vector3.Lerp (initialPos , new Vector3( initialPos.x,initialPos.y+1f,initialPos.z),(lerpTime));
				//Quaternion.Lerp(transform.rotation,Quaternion.Euler(-90,transform.rotation.y,transform.rotation.z),(exitTime));
			}
			if(lerpTime>1){
				isFalling=false;
				rb.useGravity=true;
			}

		}

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
		
			col.gameObject.SendMessage("Destroy");
		}
    }

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
        {
            if (!hasFallen)
            {
				isFalling=true;
               // transform.RotateAround(transform.position, transform.right, -90);

			/*	startLerpTime = Time.time;
				
				while (exitTime < 1  )
				{
					exitTime += ((Time.time - startLerpTime) / lerptime);
					transform.eulerAngles= Vector3.Lerp( transform.eulerAngles , new Vector3(transform.eulerAngles.x-90,transform.eulerAngles.y,transform.eulerAngles.z),(exitTime));
					//Quaternion.Lerp(transform.rotation,Quaternion.Euler(-90,transform.rotation.y,transform.rotation.z),(exitTime));
				}*/
				//transform.eulerAngles= new Vector3(
				//	Mathf.LerpAngle(transform.eulerAngles.x, transform.eulerAngles.x-90, 1*Time.deltaTime),
				//	transform.eulerAngles.y,
					//Mathf.LerpAngle(transform.eulerAngles.y, transform.eulerAngles.y, Time.deltaTime),
				//	transform.eulerAngles.z);
					//Mathf.LerpAngle(transform.eulerAngles.z, transform.eulerAngles.z, Time.deltaTime));
                MakeSound(col.transform.position);
                hasFallen = true;
            }
            else if (col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);

        }
		if (col.gameObject.tag == "Enemy" && rb.velocity!=Vector3.zero) {
			col.gameObject.SendMessage("Destroy");
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
