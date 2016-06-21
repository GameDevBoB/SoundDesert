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
        enemiesOnMe.Add(col.gameObject);
    }

    void OnCollisionExit(Collision col)
    {
        enemiesOnMe.Remove(col.gameObject);
    }

    public void DestroyEnemy()
    {
        foreach (GameObject enemy in enemiesOnMe)
            enemy.SendMessage("Fall");
    }
}
