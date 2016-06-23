using UnityEngine;
using System.Collections;

public class EnterLevelObj : MonoBehaviour {
    public int levelToLoadIndex;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel+levelToLoadIndex);
        }
    }
}
