using UnityEngine;
using System.Collections;
using SoundDesertLibrary;


public class MainMenuController : MonoBehaviour {



    void Awake()
    {
        GUIController.AtAwake();

        //public delegate soundlibdele
        //GUIController.AtAwake();
    }
	// Use this for initialization
	void Start () {
	}
	
	
	// Update is called once per frame
	void Update () {
        Volume();
	}

    public void Quit()
    {
        GUIController.Quit();
    }

    public void NewGame()
    {
        GameController.instance.DeleteSaving();
    }

    /*public void Load()
    {
        GameController.instance.LoadCheckpoint();
    }*/

    public void Options()
    {
        GUIController.Options();
        GUIController.AudioPage();
    }

    public void Volume()
    {
        GUIController.Volume();
    }

    public void AudioPage()
    {
        GUIController.AudioPage();

    }

    public void MousePage()
    {
        GUIController.MousePage();

    }

    public void ControlsPage()
    {
        GUIController.ControlsPage();

    }

    public void OptionBack()
    {
        

    }
}
