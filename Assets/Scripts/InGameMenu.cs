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

        //GameObject optionMenu = Resources.Load("Assets/Resources/Prefabs/UI Root") as GameObject;
        //optionMenu = Instantiate(optionMenu, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GUIController.AtAwake();

    }

	void Update () {
        //GUIController.Volume();

        if (GameController.instance.joyPad == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menu == false)
                {
                    GUIController.OpenMenu();
                    menu = true;
                }
                else
                {
                    GUIController.CloseMenu();
                    menu = false;
                }
            }
        }
        if (GameController.instance.joyPad == true)
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (menu == false)
                {
                    GUIController.OpenMenu();
                }
                else
                {
                    GUIController.CloseMenu();
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
}
