using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform player;

    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position + offset;
	}


}
