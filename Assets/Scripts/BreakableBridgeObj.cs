using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreakableBridgeObj : MonoBehaviour {
    private List<GameObject> enemiesOnMe;
	// Use this for initialization
	void Start () {
        enemiesOnMe = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
            enemiesOnMe.Add(col.gameObject);
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
            enemiesOnMe.Remove(col.gameObject);
    }

    public void FallEnemy()
    {
        foreach (GameObject enemy in enemiesOnMe)
            enemy.SendMessage("Fall");
    }
}
