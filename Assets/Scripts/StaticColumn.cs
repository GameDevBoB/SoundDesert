using UnityEngine;
using System.Collections;

public class StaticColumn : SoundAffected {

	void OnCollisionEnter(Collision col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj) {

			MakeSound(col.transform.position);
			
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
		
		{
				MakeSound(col.transform.position);

		}
	}

}
