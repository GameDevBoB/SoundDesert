using UnityEngine;
using System.Collections;
using SoundDesertLibrary;

public class InGameMenu : MonoBehaviour {

    private bool menu;
    public static InGameMenu instance;
    public AudioSource[] efxSource;
    public static float volumes;
    

   
    void Awake()
    {
        Cursor.visible = false;
        volumes = PlayerPrefs.GetFloat("Audio Volume", 1);
        if (InGameMenu.instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < efxSource.Length; i++)
        {
            efxSource[i].volume = volumes;
        }

        //optionMenu = Instantiate(optionMenu, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        //GUIController.AtAwake();

    }

	void LateUpdate () {
        Debug.Log("JOYPAD = " + GameController.instance.joyPad);
        GUIController.AtUpdate();
        GUIController.MouseSensitivity();
        //GUIController.Volume();
        
        if (GameController.instance.joyPad == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menu == false)
                {
                    //GameObject.Find("Player").SetActive(false);
                    Physics.queriesHitTriggers = true;
                    GUIController.OpenMenu();
                    menu = true;
                    Cursor.visible = true;
                }
                else
                {
                    Physics.queriesHitTriggers = false;
                    GUIController.CloseMenu();
                    menu = false;
                    Cursor.visible = false;
                }
            }
        }
        if (GameController.instance.joyPad == true)
        {
            if (Input.GetButtonDown("Pause"))
            {
                Physics.queriesHitTriggers = true;
                if (menu == false)
                {
                    GUIController.OpenMenu();
                    menu = true;
                    Cursor.visible = true;
                }
                else
                {
                    Physics.queriesHitTriggers = false;
                    GUIController.CloseMenu();
                    menu = false;
                    Cursor.visible = false;
                }
            }
        }
        GUIController.AtUpdate();
    }

    public void Quit()
    {
        GUIController.Quit();

    }

    public void QuitToMenu()
    {
        Application.LoadLevel("MainMenu");
        Destroy(gameObject);
    }

    public void OptionMenu()
    {
        Debug.Log("ssss");
        GUIController.Options();
        GUIController.MousePage();
        
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
        for(int i = 0; i < efxSource.Length; i++)
        {
            efxSource[i].volume = volumes;
        }
    }

    public void Resume()
    {
        Physics.queriesHitTriggers = false;
        GUIController.CloseMenu();
        menu = false;
        Cursor.visible = false;
    }

    /*public void Joypad()
    {
        
        GUIController.JoypadCheckBox();

        Debug.Log("Joypad cane = " + GameController.instance.joyPad);
    }*/

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
