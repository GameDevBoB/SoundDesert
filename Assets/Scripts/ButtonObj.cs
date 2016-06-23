using UnityEngine;
using System.Collections;

public class ButtonObj : MonoBehaviour
{

    public GameObject door;

    private bool isPressed;
    private bool isBlocked;
   
    void Start()
    {
        door.SendMessage("AddRequiredButton");
        isPressed = false;
        isBlocked = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Cube" || col.gameObject.name == "PushCube" && !isPressed)
        {
            door.SendMessage("Open");
            if (col.gameObject.tag == "Cube")
            {
                col.gameObject.transform.position = new Vector3(transform.position.x, col.gameObject.transform.position.y, transform.position.z);
                col.SendMessage("Block");
                isBlocked = true;
            }
            isPressed = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (!isBlocked)
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "Cube" || col.gameObject.name == "PushCube")
            {
                door.SendMessage("Close");
            }
        }
    }

}
