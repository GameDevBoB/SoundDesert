using UnityEngine;
using System.Collections;

public class Amplificator : SoundAffected {
    public AudioSource mySource;
    public AudioClip myClip;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "SoundWave")

        {
            MakeSound(col.transform.position);
            mySource.PlayOneShot(myClip);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "SoundWave")

        {
            MakeSound(col.transform.position);
            mySource.PlayOneShot(myClip);

        }
    }
}
