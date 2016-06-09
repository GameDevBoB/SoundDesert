using UnityEngine;
using System.Collections;

public class Abyss : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("GetDamage");
        }
    }
}
