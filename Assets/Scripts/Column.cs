using UnityEngine;
using System.Collections;

public class Column : SoundAffected {
    //public Vector3 fallRotation;
    
    public GameObject mymesh;

    //private Material elementMat;
    private float hiddenColumn = 0.2f;
    private float visibleColumn = 1f;
    //private Color prevColor;
    private bool hasFallen;
	private Rigidbody rb;

    void Awake()
    {
        //elementMat = mymesh.GetComponent<MeshRenderer>().material;
        //prevColor = elementMat.color;
		rb = GetComponent<Rigidbody> ();
    }

	// Use this for initialization
	void Start () {
		hasFallen = false;
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {
            if (!hasFallen)
            {
                transform.RotateAround(transform.position, transform.right, -90);
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
                transform.RotateAround(transform.position, transform.right, -90);
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
