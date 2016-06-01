using UnityEngine;
using System.Collections;

public class ButtonObj : MonoBehaviour
{

    public GameObject door;
   


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Cube")
        {
            door.SendMessage("Open");
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.tag == "Cube")
        {
            door.SendMessage("Close");
        }
    }

}
