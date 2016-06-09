using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

namespace SoundDesertLibrary
{
    public class GUIController : MonoBehaviour
    {
        private UISlider mouseSpeed;
        private UISprite keyboardAndMouse;
        private UISprite controller;
        private UISlider masterVolume;
        private UISlider musicVolume;
        private UISlider efxVolume;
        private AudioSource musicSource;
        private AudioSource efxSource;

        void AtAwake()
        {
            mouseSpeed = GameObject.Find("Mouse Sensitivity").GetComponent<UISlider>();
            keyboardAndMouse = Resources.Load("Keyboard Map") as UISprite;
            controller = Resources.Load("Controller Map") as UISprite;
            keyboardAndMouse.gameObject.SetActive(false);
            controller.gameObject.SetActive(false);
            masterVolume = GameObject.Find("Master Volume").GetComponent<UISlider>();
            musicVolume = GameObject.Find("Music Volume").GetComponent<UISlider>();
            efxVolume = GameObject.Find("Efx Volume").GetComponent<UISlider>();
            musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
            efxSource = GameObject.Find("Music").GetComponent<AudioSource>();
            
        }

        void Quit()
        {
            Application.Quit();

        }

        void MouseSensitivity()
        {
            RPG_Camera.mouseSpeed = mouseSpeed.sliderValue;

        }

        void KeyBinding()
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

        void Volume()
        {
            musicSource.volume = musicVolume.value * masterVolume.value;
            efxSource.volume = efxVolume.value * masterVolume.value;

        }
        
    }
}
