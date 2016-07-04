using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using SoundDesertLibrary;
using UnityEngine.Audio;


namespace SoundDesertLibrary
{
    public  class GUIController : MonoBehaviour
    {
        private static UISlider mouseSpeed;
        private static UISprite keyboardAndMouse;
        private static UISprite controller;
        private static UISlider masterVolume;
        private static UISlider musicVolume;
        private static UISlider efxVolume;
        private static AudioSource musicSource;
        private static AudioSource efxSource;
        private static GameObject optionMenu;
        private static GameObject[] optionMenuPage = new GameObject[3];
        private static GameObject mainMenu;
        private static GameObject menu;
        private static bool controllo;

        public static void AtAwake()
        {
            controllo = false;
            if (Application.loadedLevelName == "MainMenu")
            {
                Debug.Log("Jeppetto");
                mainMenu = GameObject.Find("First Menu");
            }
            else
            {
                menu = GameObject.Find("First Menu");
                Debug.Log(menu.name);
            }
            
            mouseSpeed = GameObject.Find("Mouse Sensitivity").GetComponent<UISlider>();
            /*keyboardAndMouse = Resources.Load("Keyboard Map") as UISprite;
            controller = Resources.Load("Controller Map") as UISprite;*/
            //keyboardAndMouse.gameObject.SetActive(false);
            //controller.gameObject.SetActive(false);
            masterVolume = GameObject.Find("Master Volume").GetComponent<UISlider>();
            musicVolume = GameObject.Find("Music Volume").GetComponent<UISlider>();
            efxVolume = GameObject.Find("Efx Volume").GetComponent<UISlider>();
            //musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
            //efxSource = GameObject.Find("Music").GetComponent<AudioSource>();
            optionMenu = GameObject.Find("Option Menu");
            optionMenuPage[0] = GameObject.Find("Audio Option");
            optionMenuPage[1] = GameObject.Find("Mouse Option");
            optionMenuPage[2] = GameObject.Find("Controls Option");
            optionMenuPage[0].SetActive(true);
            optionMenuPage[1].SetActive(false);
            optionMenuPage[2].SetActive(false);
            optionMenu.SetActive(false);
            if(Application.loadedLevelName != "MainMenu")
            {
                menu.SetActive(false);
            }

            mouseSpeed.value = RPG_Camera.mouseSpeed / 8;
            


        }

        public static void AtUpdate()
        {
            //Debug.Log(controllo);
        }

        public static void Quit()
        {
            Application.Quit();

        }

        public static void MouseSensitivity()
        {
            RPG_Camera.mouseSpeed = (mouseSpeed.value * 8f) + 0.5f;

        }

        public static void KeyBinding()
        {
           if(GameController.instance.joyPad == false)
            {
                keyboardAndMouse.gameObject.SetActive(true);
            }
           else
            {
                controller.gameObject.SetActive(true);
            }

        }

        public static void Volume()
        {
            musicSource.volume = musicVolume.value * masterVolume.value;
            efxSource.volume = efxVolume.value * masterVolume.value;

        }

        public static void Options()
        {
            Debug.Log("GianniGianni");
            optionMenu.SetActive(true);
            Debug.Log("cccc");
            if (mainMenu != null)
            {
                mainMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                menu.SetActive(false);
            }

        }
        
        public static void MousePage()
        {
            optionMenuPage[0].SetActive(false);
            optionMenuPage[1].SetActive(true);
            optionMenuPage[2].SetActive(false);

        }

        public static void AudioPage()
        {
            optionMenuPage[0].SetActive(true);
            optionMenuPage[1].SetActive(false);
            optionMenuPage[2].SetActive(false);

        }

        public static void ControlsPage()
        {
            optionMenuPage[0].SetActive(false);
            optionMenuPage[1].SetActive(false);
            optionMenuPage[2].SetActive(true);

        }

        public static void OptionBack()
        {
            optionMenu.SetActive(false);
            PlayerPrefs.SetFloat("Mouse Sensitivity", RPG_Camera.mouseSpeed);
            if (mainMenu != null)
            {
                mainMenu.SetActive(true);
            }
            else
            {
                menu.SetActive(true);
                
            }

        }

        public static void OpenMenu()
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
        public static void CloseMenu()
        {
            controllo = true;
            Time.timeScale = 1;
            menu.SetActive(false);
            
        }

        public static void JoypadCheckBox()
        {
            if(!GameController.instance.joyPad)
                GameController.instance.joyPad = true;
            else
                GameController.instance.joyPad = false;
        }

        public static void Continue()
        {
            int levelToLoad = PlayerPrefs.GetInt("CompletedLevel");
            if(levelToLoad > 0)
            {
                Application.LoadLevel(levelToLoad);
            }
        }
    }

   
    public class AudioController : MonoBehaviour
    {

        private static AudioSource musicSource;
        private static AudioSource efxSource;
        

        void AtAwake()
        {
            musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
            efxSource = GameObject.Find("Music").GetComponent<AudioSource>();
        }


        public static void MoveSound(AudioClip Sound)
        {
            if(!efxSource.isPlaying )
                efxSource.PlayOneShot(Sound);
        }

        public static void ShootSound(AudioClip Sound)
        {
            efxSource.PlayOneShot(Sound);
        }

        public static void EnemyActivation()
        {
            //efxSource.PlayOneShot()
        }




    }


}
