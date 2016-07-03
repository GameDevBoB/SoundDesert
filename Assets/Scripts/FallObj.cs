using UnityEngine;
using System.Collections;

public class FallObj : SoundAffected
{
    public float timeToRebuild = 2;

    private Rigidbody rb;
    private bool hasFallen;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Use this for initialization
    void Start()
    {
        hasFallen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log (rb.velocity);

    }

    void OnCollisionEnter(Collision col)
    {

        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }


        if (col.gameObject.tag == "SoundWave" && rb.useGravity && col.gameObject != soundObj)
        {
            MakeSound(col.transform.position);

        }

        if (col.gameObject.tag == "Desert" && !hasFallen)
        {
            // MakeSound(col.transform.position);
            /*Vector3 posSound = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            MakeSound(posSound);*/
            Vector3 posSound = col.contacts[0].point;
            MakeSound(posSound);
            hasFallen = true;
            gameObject.layer = LayerMask.NameToLayer("Repairable");
        }

        if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player") && rb.velocity.magnitude > 0.5f && !hasFallen)
        {

            col.gameObject.SendMessage("GetDamage");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Sound" || col.gameObject.tag == "SoundWave") && col.gameObject != soundObj)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        if (col.gameObject.tag == "SoundWave" && rb.useGravity && col.gameObject != soundObj)
            MakeSound(col.transform.position);

        if (col.gameObject.tag == "Desert" && !hasFallen)
        {
            // MakeSound(col.transform.position);
            /*Vector3 posSound = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            MakeSound(posSound);*/
            Vector3 posSound = col.transform.position;
            MakeSound(posSound);
            hasFallen = true;
            gameObject.layer = LayerMask.NameToLayer("Repairable");
        }
        //if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player") && rb.velocity.magnitude > 0.5f)
        //{

            //col.gameObject.SendMessage("GetDamage");
        //}
    }

    public void Repair()
    {
        StartCoroutine("BackAtStart");
        Debug.Log("posizione fallobj " + transform.position);
    }

    IEnumerator BackAtStart()
    {
        float lerpValue = 0;
        float time = 0;
        Vector3 actualPosition = transform.position;
        Quaternion actualRotation = transform.rotation;
        rb.useGravity = false;
        rb.isKinematic = true;
        while (lerpValue < 1)
        {
            time += 0.01f;
            lerpValue = time / timeToRebuild;
            transform.position = Vector3.Lerp(actualPosition, startPosition, lerpValue);
            transform.rotation = Quaternion.Lerp(actualRotation, startRotation, lerpValue);
            yield return new WaitForSeconds(0.01f);
        }
        hasFallen = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }


}
