using UnityEngine;
using System.Collections;

public class EndLevelObj : MonoBehaviour {

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
            GameController.instance.EndLevel();
        }
    }
}
