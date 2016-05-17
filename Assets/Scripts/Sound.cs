using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
    public float range;
    public float duration;

	private Vector3 startScale;

	// Use this for initialization
	void Start () {
        startScale = transform.localScale;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.magnitude < (startScale * range).magnitude)
        {
            transform.localScale = new Vector3(transform.localScale.x + range / duration, transform.localScale.y + range / duration, transform.localScale.z + range / duration);
        }
        else
            gameObject.SetActive(false);
	
	}

    public void Expand()
    {
		transform.position = transform.parent.position;
        transform.localScale = startScale;
    }
}
