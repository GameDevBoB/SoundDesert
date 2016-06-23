using UnityEngine;
using System.Collections;

public class CheckpointObj : MonoBehaviour {
    //public GameObject myBasRelief;

    //private Material myBasReliefMaterial;
    //private Color myBasReliefColorActive;
    //private Color myBasReliefColorDisactive;

    void Awake()
    {
        //myBasReliefMaterial = myBasRelief.GetComponent<MeshRenderer>().material;
        //myBasReliefColorActive = myBasReliefMaterial.GetColor("_EmissionColor");
        //myBasReliefColorDisactive = myBasReliefMaterial.GetColor("_EmissionColor") * 0;
    }

	// Use this for initialization
	void Start () {
        //myBasReliefMaterial.SetColor("_EmissionColor", myBasReliefColorDisactive);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameController.instance.SaveCheckpoint(gameObject);
            this.gameObject.SetActive(false);
            //Debug.Log(myBasReliefMaterial.GetColor("_EmissionColor"));
            //myBasReliefMaterial.SetColor("_EmissionColor", myBasReliefColorActive);
        }
    }
}
