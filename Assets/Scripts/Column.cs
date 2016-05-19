using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour {
    //public Vector3 fallRotation;
	private bool hasFallen;
    private Material elementMat;
   
				
    private float hiddenColumn = 0.2f;
    private float visibleColumn = 1f;
    private Color prevColor;
    
	// Use this for initialization
	void Start () {
		hasFallen = false;
        elementMat = GetComponent<MeshRenderer>().material;
        prevColor = elementMat.color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Sound" && !hasFallen) {
			transform.RotateAround (transform.position, transform.right, -90);
			hasFallen = true;
		}
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Sound" && !hasFallen) {
			//Debug.Log(col.gameObject.transform.position);
			transform.RotateAround (transform.position, transform.right, -90);
			hasFallen = true;
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
