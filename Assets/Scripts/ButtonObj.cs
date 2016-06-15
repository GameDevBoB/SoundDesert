using UnityEngine;
using System.Collections;

public class ButtonObj : MonoBehaviour
{

    public GameObject door;
   


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Cube" || col.gameObject.name == "PushCube")
        {
            door.SendMessage("Open");
            if (col.gameObject.tag == "Cube")
            {
                col.gameObject.transform.position = new Vector3(transform.position.x, col.gameObject.transform.position.y, transform.position.z);
            }
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Cube"  || col.gameObject.name == "PushCube")
        {
            door.SendMessage("Close");
           
            
        }
    }

}
