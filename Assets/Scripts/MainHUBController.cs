﻿using UnityEngine;
using System.Collections;

public class MainHUBController : MonoBehaviour {
    public Transform[] spawnPoints;
    public GameObject[] myBasReliefs;
    public GameObject[] doorOpenerCubes;
    public GameObject[] loadingDoors;
    public GameObject[] enterLevels;

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
        //if (PlayerPrefs.GetInt("ExitLevel") < Application.loadedLevel)
            //PlayerPrefs.SetInt("ExitLevel", Application.loadedLevel);
    }

    void Start () {
        Debug.Log("Livelli completati " + PlayerPrefs.GetInt("CompletedLevel"));
	    for(int i =0; i < myBasReliefsMaterials.Length; i++)
        {
            if(i < (PlayerPrefs.GetInt("CompletedLevel") - Application.loadedLevel))
                myBasReliefsMaterials[i].SetColor("_EmissionColor", myBasReliefColorActive);
            else
                myBasReliefsMaterials[i].SetColor("_EmissionColor", myBasReliefColorDisactive);
        }
        //Debug.Log(PlayerPrefs.GetInt("ExitLevel") - Application.loadedLevel);
        if (PlayerPrefs.GetInt("ExitLevel") - Application.loadedLevel >= 0)
            player.transform.position = spawnPoints[PlayerPrefs.GetInt("ExitLevel") - Application.loadedLevel-1].position;
        loadingDoors[0].SendMessage("AddRequiredButton");
        loadingDoors[0].SendMessage("Open");
        /*for (int i = 0; i < (PlayerPrefs.GetInt("ExitLevel") - Application.loadedLevel); i++)
        {
            doorOpenerCubes[i].SetActive(true);
            //if (loadingDoors.Length > (i + 1))
            //{
                //loadingDoors[i + 1].SendMessage("AddRequiredButton");
                //loadingDoors[i + 1].SendMessage("Open");
            //}
            //Debug.Log(i);
        }*/

        int levelcompleted = PlayerPrefs.GetInt("ExitLevel") - Application.loadedLevel;
        if (levelcompleted >= 1)
        {
            loadingDoors[1].SendMessage("AddRequiredButton");
            loadingDoors[1].SendMessage("Open");
            loadingDoors[2].SendMessage("AddRequiredButton");
            loadingDoors[2].SendMessage("Open");
            enterLevels[0].SetActive(false);
        }
        if(levelcompleted >=2)
        {
            loadingDoors[3].SendMessage("AddRequiredButton");
            loadingDoors[3].SendMessage("Open");
            doorOpenerCubes[0].SetActive(false);
            doorOpenerCubes[1].SetActive(true);
            enterLevels[1].SetActive(false);
        }
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
