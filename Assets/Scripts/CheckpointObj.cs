using UnityEngine;
using System.Collections;

public class CheckpointObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
        }
    }
}
