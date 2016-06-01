using UnityEngine;
using System.Collections;


public class DoorObj : MonoBehaviour
{
    public float lerptime;
    public float doorTranslation;

    private bool isOpen;
    private float startLerpTime;
    private Vector3 initialPos;
    private float exitTime;



    void Awake()
    {

        //lerptime = 1.5f;
        isOpen = false;
        initialPos = transform.position;

    }

    

    public void Open()
    {

        if (!isOpen)
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
    }

    IEnumerator OpenDoor()
    {
        while (exitTime < 1  )
        {
            exitTime += ((Time.time - startLerpTime) / lerptime);
            transform.position = Vector3.Lerp(initialPos, initialPos + Vector3.up * doorTranslation, (exitTime));
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator CloseDoor()
    {
        while (exitTime > 0)
        {
            exitTime -= ((Time.time - startLerpTime) / lerptime);
            transform.position = Vector3.Lerp(initialPos, initialPos + Vector3.up * doorTranslation, (exitTime ));
            yield return new WaitForEndOfFrame();
        }
        
    }
}
