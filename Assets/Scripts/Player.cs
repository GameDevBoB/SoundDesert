using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
    public GameObject soundObj;
    public float coolDown;

    private Rigidbody rb;
    private float startTimeShoot;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.W))
            Move(transform.forward);
        if (Input.GetKey(KeyCode.S))
            Move(-transform.forward);
        if (Input.GetKey(KeyCode.D))
            Move(transform.right);
        if (Input.GetKey(KeyCode.A))
            Move(-transform.right);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((Time.time - startTimeShoot) > coolDown || startTimeShoot == 0)
            {
                MakeSound();
                startTimeShoot = Time.time;
            }
        }
    }

    void Move(Vector3 direction)
    {
        rb.MovePosition(transform.position + direction*speed);
    }

    void MakeSound()
    {
        soundObj.SetActive(true);
        soundObj.SendMessage("Expand");
    }
}
