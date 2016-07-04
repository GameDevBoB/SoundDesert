using UnityEngine;
using System.Collections;

public class SoundAffected : MonoBehaviour {
	public GameObject soundSpherePrefab;
    //public AudioClip soundAudio;

    protected GameObject soundObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void MakeSound(Vector3 origin)
	{
		soundObj = Instantiate (soundSpherePrefab, origin, Quaternion.identity) as GameObject;
		soundObj.transform.position = origin;
        //GameController.instance.PlayAudio(soundAudio);
		//soundObj.SetActive(true);
		//soundObj.SendMessage("Expand");
	}
}
