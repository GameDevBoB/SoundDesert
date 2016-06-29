using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreakableBridgeObj : MonoBehaviour {
    private List<GameObject> enemiesOnMe;
    private bool hasFallen;
	// Use this for initialization
	void Start () {
        enemiesOnMe = new List<GameObject>();
        hasFallen = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(enemiesOnMe.Count);

    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Enemy")
        {
            if (hasFallen)
                col.gameObject.SendMessage("Fall");
            else
                enemiesOnMe.Add(col.gameObject);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (hasFallen)
        {
            foreach (GameObject enemy in enemiesOnMe)
                enemy.SendMessage("Fall");
        }
    }

    void OnTriggerExit(Collider col)
    {
        //Debug.Log(col.gameObject.tag + " esce");
        if (col.gameObject.tag == "Enemy" && !hasFallen)
            enemiesOnMe.Remove(col.gameObject);
    }

    public void FallEnemy()
    {
        hasFallen = true;
        //Debug.Log("Faccio cadere i nemici " + enemiesOnMe.Count);
        foreach (GameObject enemy in enemiesOnMe)
        {
            enemy.SendMessage("Fall");
        }
        enemiesOnMe.Clear();
    }
}
