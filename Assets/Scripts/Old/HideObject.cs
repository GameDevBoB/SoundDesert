using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObject : MonoBehaviour {

	public Transform player;
	public List <Transform> hiddenObjs;
	public LayerMask myMask;

	private Vector3 direction;
	private float distance;
    private Transform currentHit;
	private Transform myCamera;
    private Transform wasHidden;

	void Awake()
	{
		myCamera = transform;
	}

    // Use this for initialization
    void Start () {
		hiddenObjs = new List<Transform> ();

	
	}
	
	// Update is called once per frame
	void Update () {

		FindPlayerDistance ();
		StoreObjs ();
	
	}

	void FindPlayerDistance()
	{

		distance = Vector3.Distance (player.position, myCamera.position);
		direction = player.position - myCamera.position;
		

	}

	void StoreObjs()
	{
		RaycastHit[] hits = Physics.RaycastAll(myCamera.position, direction, distance, myMask);
		for (int i = 0; i < hits.Length; i++)
		{

			currentHit = hits[i].transform;
			
			if (!hiddenObjs.Contains(currentHit))
			{
				hiddenObjs.Add(currentHit);
                currentHit.gameObject.SendMessage("GetAlpha");
            }

		}
		for (int i = 0; i < hiddenObjs.Count; i++)
		{
			bool isHit = false;
			for (int j = 0; j < hits.Length; j++)
			{
				if (hits[j].transform == hiddenObjs[i])
				{
					isHit = true;
					break;
				}
			}
			if (!isHit)
			{
				wasHidden = hiddenObjs[i];
                wasHidden.gameObject.SendMessage("BackToNormalAlpha");
                hiddenObjs.RemoveAt(i);
				i--;
			}
		}

	}


}
