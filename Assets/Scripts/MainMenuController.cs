using UnityEngine;
using System.Collections;
using SoundDesertLibrary;


public class MainMenuController : MonoBehaviour {



    void Awake()
    {
        GUIController.AtAwake();

    }

	void Start () {

    }

	void Update () {
        //Volume();
	}

    public void Quit()
    {
        GUIController.Quit();
    }

    public void NewGame()
    {
        GameController.instance.DeleteSaving();
        Application.LoadLevel("Tutorial");
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
        GUIController.OptionBack();

    }

    public void Continue()
    {
        GUIController.Continue();
    }
}
