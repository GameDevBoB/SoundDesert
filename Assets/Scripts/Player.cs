using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
    public GameObject soundObj;
    public float coolDown;
    public GameObject wavePrefab;
    public Transform spawnPointWave;
    public float waveVelocity;
    public float waveDuration;
    public Transform lookAt;

    private Rigidbody rb;
    private float startTimeShoot;
    private RaycastHit hit;
	private LineRenderer myAimPreview;

    void Awake()
    {
        lookAt.position = transform.position;
        rb = GetComponent<Rigidbody>();
        Physics.queriesHitTriggers = false;
		myAimPreview = GetComponent<LineRenderer> ();
    }

	// Use this for initialization
	void Start () {
		myAimPreview.SetPosition (0, spawnPointWave.position);
		myAimPreview.SetPosition (1, spawnPointWave.forward * waveVelocity * waveDuration);
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
        if (Input.GetMouseButton(0))
        {
            if ((Time.time - startTimeShoot) > coolDown || startTimeShoot == 0)
            {
                //MakeSound();
                ShootSoundWave();
                startTimeShoot = Time.time;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            MakeSound();
        }

        if (Input.GetButtonDown("MakeSound"))
        {
            if ((Time.time - startTimeShoot) > coolDown || startTimeShoot == 0)
            {
                ShootSoundWave();
                startTimeShoot = Time.time;
            }
        }

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1 << LayerMask.NameToLayer("Floor"));
        //Vector3 mouse2World = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Vector3.Distance(Camera.main.transform.position, hit.transform.position), Input.mousePosition.z));
        //Debug.DrawLine(transform.position, hit.point, Color.red, 1);
        transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
		myAimPreview.SetPosition (0, spawnPointWave.position);
		myAimPreview.SetPosition (1, spawnPointWave.position + spawnPointWave.forward * waveVelocity * waveDuration);
    }

    void Move(Vector3 direction)
    {
        rb.MovePosition(transform.position + direction*speed);
    }

    void ShootSoundWave()
    {
        GameObject wave = Instantiate(wavePrefab, spawnPointWave.position, spawnPointWave.rotation * wavePrefab.transform.rotation) as GameObject;
        wave.SendMessage("Shoot", new float[]{ waveVelocity, waveDuration });
    }

    void MakeSound()
    {
        soundObj.SetActive(true);
        soundObj.SendMessage("Expand");
    }

    void MoveJoyPad()
    {
        float x = Input.GetAxis("LeftAnalX");
        float y = Input.GetAxis("LeftAnalY");
        if (x > 0.1)
        {
            Vector3 s = new Vector3(transform.position.x + (x * speed), transform.position.y, transform.position.z - (y * speed));
            rb.MovePosition(s);
        }
        if (x < -0.1)
        {
            Vector3 s = new Vector3(transform.position.x + (x * speed), transform.position.y, transform.position.z - (y * speed));
            rb.MovePosition(s);
        }
        if (y > 0.1)
        {
            Vector3 s = new Vector3(transform.position.x + (x * speed), transform.position.y, transform.position.z - (y * speed));
            rb.MovePosition(s);
        }
        if (y < -0.1)
        {
            Vector3 s = new Vector3(transform.position.x + (x * speed), transform.position.y, transform.position.z - (y * speed));

            rb.MovePosition(s);
        }
    }


    void RotateJoyPad()
    {
        float xl = Input.GetAxis("RightAnalX");
        float yl = Input.GetAxis("RightAnalY");
        if (xl > 0.1)
        {
            lookAt.position = new Vector3(transform.position.x - xl, transform.position.y, transform.position.z + yl);
            transform.LookAt(lookAt);
        }
        if (xl < -0.1)
        {
            lookAt.position = new Vector3(transform.position.x - xl, transform.position.y, transform.position.z + yl);
            transform.LookAt(lookAt);
        }
        if (yl > 0.1)
        {
            lookAt.position = new Vector3(transform.position.x - xl, transform.position.y, transform.position.z + yl);
            transform.LookAt(lookAt);
        }
        if (yl < -0.1)
        {
            lookAt.position = new Vector3(transform.position.x - xl, transform.position.y, transform.position.z + yl);
            transform.LookAt(lookAt);
        }
    }
}
