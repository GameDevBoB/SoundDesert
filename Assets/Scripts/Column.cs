using UnityEngine;
using System.Collections;

public class Column : SoundAffected {
    //public Vector3 fallRotation;
    
    public GameObject mymesh;

    private Material elementMat;
    private float hiddenColumn = 0.2f;
    private float visibleColumn = 1f;
    private Color prevColor;
    private bool hasFallen;

    void Awake()
    {
        elementMat = mymesh.GetComponent<MeshRenderer>().material;
        prevColor = elementMat.color;
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
                MakeSound(col.transform.position);
                transform.RotateAround(transform.position, transform.right, -90);
                hasFallen = true;
            }
            else if (col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);
            
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
        {
            if (!hasFallen)
            {
                MakeSound(col.transform.position);
                transform.RotateAround(transform.position, transform.right, -90);
                hasFallen = true;
            }
            else if (col.gameObject.tag == "SoundWave")
                MakeSound(col.transform.position);

        }
    }

    public void GetAlpha()
    {
        prevColor.a = hiddenColumn;
        elementMat.color = prevColor;
    }

    public void BackToNormalAlpha()
    {
        prevColor.a = visibleColumn;
        elementMat.color = prevColor;
    }
}
