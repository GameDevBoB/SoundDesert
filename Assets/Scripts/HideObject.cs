using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObject : MonoBehaviour {

	public Transform player;
	public Transform myCamera;
	public List <Transform> hiddenObjects;
	public LayerMask myMask;



	private Vector3 direction;
	private float distance;
    private Transform currentHit;
    private Transform wasHidden;

    // Use this for initialization
    void Start () {
		hiddenObjects = new List<Transform> ();

	
	}
	
	// Update is called once per frame
	void Update () {

		FindPlayerDistance ();
		StoreElements ();
	
	}

	void FindPlayerDistance()
	{

		distance = Vector3.Distance (player.position, myCamera.position);
		direction = player.position - myCamera.position;
		

	}

	void StoreElements()
	{
		RaycastHit[] hits = Physics.RaycastAll(myCamera.position, direction, distance, myMask);
		for (int i = 0; i < hits.Length; i++)
		{

			currentHit = hits[i].transform;
			
			if (!hiddenObjects.Contains(currentHit))
			{
				hiddenObjects.Add(currentHit);
                currentHit.gameObject.SendMessage("GetAlpha");
            }

		}
		for (int i = 0; i < hiddenObjects.Count; i++)
		{
			bool isHit = false;
			for (int j = 0; j < hits.Length; j++)
			{
				if (hits[j].transform == hiddenObjects[i])
				{
					isHit = true;
					break;
				}
			}
			if (!isHit)
			{
				wasHidden = hiddenObjects[i];
                wasHidden.gameObject.SendMessage("BackToNormalAlpha");
                hiddenObjects.RemoveAt(i);
				i--;
			}
		}

	}


}
