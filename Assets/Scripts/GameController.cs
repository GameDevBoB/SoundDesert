using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    [HideInInspector]
    public static GameController instance;
    public int levelIndex;
    public List<GameObject> checkpoints;
    [HideInInspector]
    public bool joyPad;

    private AudioSource myAudio;
    private GameObject player;

    void Awake()
    {
        Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.Locked;
        instance = this;
        player = GameObject.FindWithTag("Player");
        //Debug.Log("Livelli completati " + PlayerPrefs.GetInt("CompletedLevel"));
        if (PlayerPrefs.GetInt("CheckpointLevel") == levelIndex && checkpoints.Count != 0)
        {
            LoadCheckpoint();
        }
        myAudio = GetComponent<AudioSource>();
        SoundDesertLibrary.GUIController.AtAwake();
    }



	// Use this for initialization
	void Start () {
        levelIndex = Application.loadedLevel;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteSaving();
        }
	}

    public void SaveCheckpoint(GameObject checkpoint)
    {
        PlayerPrefs.SetInt("CheckpointIndex", checkpoints.IndexOf(checkpoint));
        PlayerPrefs.SetInt("CheckpointLevel", levelIndex);
        PlayerPrefs.Save();
        Debug.Log("Checkpoint salvato!");
    }

    public void ClearCheckpoint()
    {
        PlayerPrefs.DeleteKey("CheckpointIndex");
        PlayerPrefs.DeleteKey("CheckpointLevel");
    }

    public void LoadCheckpoint()
    {
        int index = PlayerPrefs.GetInt("CheckpointIndex");
        player.transform.position = checkpoints[index].transform.position;
        for(int i = 0; i <= index; i++)
        {
            checkpoints[i].SetActive(false);
        }
    }

    public void EndLevel()
    {
        if (PlayerPrefs.GetInt("CompletedLevel") < levelIndex)
        {
            PlayerPrefs.SetInt("CompletedLevel", levelIndex);
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetInt("ExitLevel", levelIndex);
        ClearCheckpoint();
        Debug.Log("Bravo hai finito il livello!");
        Time.timeScale = 0;
        Application.LoadLevel(1);
    }

    public void DeleteSaving()
    {
        Debug.Log("Salvataggi cancellati");
        ClearCheckpoint();
        PlayerPrefs.DeleteKey("CompletedLevel");
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }

    public void PlayAudio(AudioClip audio)
    {
        myAudio.PlayOneShot(audio);
    }
}
