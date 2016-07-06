using UnityEngine;
using System.Collections;
using SoundDesertLibrary;

public class InGameMenu : MonoBehaviour {

    private bool menu;
    public static InGameMenu instance;
   
    void Awake()
    {
        if(InGameMenu.instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
        //optionMenu = Instantiate(optionMenu, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        //GUIController.AtAwake();

    }

	void LateUpdate () {
        GUIController.AtUpdate();
        GUIController.MouseSensitivity();
        //GUIController.Volume();
        GUIController.AtUpdate();
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
                }
                else
                {
                    Physics.queriesHitTriggers = false;
                    GUIController.CloseMenu();
                    menu = false;
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
                }
                else
                {
                    Physics.queriesHitTriggers = false;
                    GUIController.CloseMenu();
                    menu = false;
                }
            }
        }
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
        GUIController.AudioPage();
        
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

    public void Resume()
    {
        Physics.queriesHitTriggers = false;
        GUIController.CloseMenu();
        menu = false;
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
