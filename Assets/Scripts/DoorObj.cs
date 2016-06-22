using UnityEngine;
using System.Collections;


public class DoorObj : MonoBehaviour
{
    public float lerptimeOpen;
    public float lerptimeClose;
    public float doorTranslation;
    public ParticleSystem myParticle;

    private bool isOpen;
    private float startLerpTime;
    private Vector3 initialPos;
    private float exitTime;
    private int requiredButtonNumber;
    private int pressedButtonsNumber;


    void Awake()
    {
        requiredButtonNumber = 0;
        //lerptime = 1.5f;
        isOpen = false;
        initialPos = transform.position;

    }

    void Start()
    {
        //myParticle.Stop();
        pressedButtonsNumber = 0;
    }

    

    public void Open()
    {
        pressedButtonsNumber++;
        //Debug.Log(pressedButtonsNumber);
        if (!isOpen && (pressedButtonsNumber == requiredButtonNumber) )
        {
            startLerpTime = Time.time;
            StopAllCoroutines();
            StartCoroutine("OpenDoor");
            isOpen = !isOpen;
        }

    }

    public void Close()
    {
        if (isOpen)
        {
            startLerpTime = Time.time;
            StopAllCoroutines();
            StartCoroutine("CloseDoor");
            isOpen = !isOpen;
        }
        pressedButtonsNumber--;
    }

    IEnumerator OpenDoor()
    {
        myParticle.Play();
        while (exitTime < 1  )
        {
           exitTime += ((Time.time - startLerpTime) / lerptimeOpen);
            transform.position = Vector3.Lerp(initialPos, initialPos + Vector3.up * doorTranslation, (exitTime));
            yield return new WaitForEndOfFrame();
        }
        myParticle.Stop();
       
    }

    IEnumerator CloseDoor()
    {
        myParticle.Play();
        while (exitTime > 0)
        {
            exitTime -= ((Time.time - startLerpTime) / lerptimeClose);
            transform.position = Vector3.Lerp(initialPos, initialPos + Vector3.up * doorTranslation, (exitTime ));
            yield return new WaitForEndOfFrame();
        }
        myParticle.Stop();
    }

    public void AddRequiredButton()
    {
        requiredButtonNumber++;
        //Debug.Log(requiredButtonNumber);
    }


}
