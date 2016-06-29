using UnityEngine;
using System.Collections;

public class MainHUBController : MonoBehaviour {
    public Transform[] spawnPoints;
    public GameObject[] myBasReliefs;

    private Material[] myBasReliefsMaterials;
    private Color myBasReliefColorActive;
    private Color myBasReliefColorDisactive;
    private GameObject player;

    void Awake()
    {
        myBasReliefsMaterials = new Material[myBasReliefs.Length];
        for (int i = 0; i < myBasReliefs.Length; i++)
        {
            myBasReliefsMaterials[i] = myBasReliefs[i].GetComponent<MeshRenderer>().material;
        }
        myBasReliefColorActive = myBasReliefsMaterials[0].GetColor("_EmissionColor");
        myBasReliefColorDisactive = myBasReliefsMaterials[0].GetColor("_EmissionColor") * 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start () {
	    for(int i =0; i < myBasReliefsMaterials.Length; i++)
        {
            if(i < (PlayerPrefs.GetInt("CompletedLevel") - Application.loadedLevel))
                myBasReliefsMaterials[i].SetColor("_EmissionColor", myBasReliefColorActive);
            else
                myBasReliefsMaterials[i].SetColor("_EmissionColor", myBasReliefColorDisactive);
        }
        player.transform.position = spawnPoints[PlayerPrefs.GetInt("ExitLevel") - Application.loadedLevel].position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
