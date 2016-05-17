using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour {
    //public Vector3 fallRotation;
	private bool hasFallen;
	// Use this for initialization
	void Start () {
		hasFallen = false;
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
}
