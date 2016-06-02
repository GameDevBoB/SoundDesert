﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public GameObject soundObj;
    public GameObject wavePrefab;
    public float speed;
    public float coolDown;
    public float waveVelocity;
    public float waveDuration;
    public Transform camera;
    public Transform cameraPos;
    public Transform lookAt;
    public Transform spawnPointWave;
    public float maxVertical;
    [HideInInspector]
    public float xl;
    [HideInInspector]
    public float yl;
    [HideInInspector]
    public float x;
    [HideInInspector]
    public float y;

    private Rigidbody rb;
    private float startTimeShoot;
    private RaycastHit hit;
    private LineRenderer myAimPreview;
    private bool joyPad;


    void Awake()
    {
        //lookAt.position = transform.position;
        rb = GetComponent<Rigidbody>();
        Physics.queriesHitTriggers = false;
        myAimPreview = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        myAimPreview.SetPosition(0, spawnPointWave.position);
        myAimPreview.SetPosition(1, spawnPointWave.forward * waveVelocity * waveDuration);
    }

    // Update is called once per frame
    void Update()
    {
        // cameraPos.position = camera.position;
        xl = Input.GetAxis("RightAnalX");
        yl = Input.GetAxis("RightAnalY");
        x = Input.GetAxis("LeftAnalX");
        y = Input.GetAxis("LeftAnalY");
        MoveJoyPad();
        //RotateJoyPad();
        CameraRotate();
        if (Input.GetButton("MakeSound"))
        {
            if ((Time.time - startTimeShoot) > coolDown || startTimeShoot == 0)
            {
                ShootSoundWave();
                startTimeShoot = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
            joyPad = true;

        if (Input.GetKeyDown(KeyCode.N))
            joyPad = false;
        if (joyPad == false)
        {
            if (Input.GetKey(KeyCode.W))
                Move(transform.forward);
            if (Input.GetKey(KeyCode.S))
                Move(-transform.forward);
            if (Input.GetKey(KeyCode.D))
                Move(transform.right);
            if (Input.GetKey(KeyCode.A))
                Move(-transform.right);
            if (Input.GetMouseButtonDown(0))
            {
                if ((Time.time - startTimeShoot) > coolDown || startTimeShoot == 0)
                {
                    //MakeSound();
                    ShootSoundWave();
                    startTimeShoot = Time.time;
                }
            }

            /*if (Input.GetKey(KeyCode.Space))
            {
                MakeSound();
            }*/



            //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1 << LayerMask.NameToLayer("Floor"));
            //Vector3 mouse2World = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Vector3.Distance(Camera.main.transform.position, hit.transform.position), Input.mousePosition.z));
            //Debug.DrawLine(transform.position, hit.point, Color.red, 1);

            //transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));

        }
        myAimPreview.SetPosition(0, spawnPointWave.position);
        myAimPreview.SetPosition(1, spawnPointWave.position + spawnPointWave.forward * waveVelocity * waveDuration);

    }

    void Move(Vector3 direction)
    {
        rb.MovePosition(transform.position + direction * speed);
    }

    void ShootSoundWave()
    {
        GameObject wave = Instantiate(wavePrefab, spawnPointWave.position, spawnPointWave.rotation * wavePrefab.transform.rotation) as GameObject;
        wave.SendMessage("Shoot", new float[] { waveVelocity, waveDuration });
    }

    void MakeSound()
    {
        soundObj.SetActive(true);
        soundObj.SendMessage("Expand");
    }

    void MoveJoyPad()
    {

        if (x > 0.1)
        {
            Vector3 s = new Vector3(transform.position.x + (x * (speed + 0.3f)), transform.position.y, transform.position.z - (y * (speed + 0.3f)));
            rb.MovePosition(s);
        }
        if (x < -0.1)
        {
            Vector3 s = new Vector3(transform.position.x + (x * (speed + 0.3f)), transform.position.y, transform.position.z - (y * (speed + 0.3f)));
            rb.MovePosition(s);
        }
        if (y > 0.1)
        {
            Vector3 s = new Vector3(transform.position.x + (x * (speed + 0.3f)), transform.position.y, transform.position.z - (y * (speed + 0.3f)));
            rb.MovePosition(s);
        }
        if (y < -0.1)
        {
            Vector3 s = new Vector3(transform.position.x + (x * (speed + 0.3f)), transform.position.y, transform.position.z - (y * (speed + 0.3f)));

            rb.MovePosition(s);
        }
    }


    /* void RotateJoyPad()
     {

         if (xl > 0.1)
         {
             lookAt.position = new Vector3(transform.position.x + yl, transform.position.y, transform.position.z - yl);
             transform.LookAt(lookAt);
         }
         if (xl < -0.1)
         {
             lookAt.position = new Vector3(transform.position.x + xl, transform.position.y, transform.position.z - yl);
             transform.LookAt(lookAt);
         }
         if (yl > 0.1)
         {
             lookAt.position = new Vector3(transform.position.x + xl, transform.position.y, transform.position.z - yl);
             transform.LookAt(lookAt);
         }
         if (yl < -0.1)
         {
             lookAt.position = new Vector3(transform.position.x + xl, transform.position.y, transform.position.z - yl);
             transform.LookAt(lookAt);
         }
     }*/

    void CameraRotate()
    {

        /*Vector3 pos = new Vector3(cameraPos.position.x - transform.position.x, lookAt.position.y, cameraPos.position.z - transform.position.z);
        
        lookAt.position = new Vector3(-pos.x, pos.y, -pos.z);*/

        lookAt.position = new Vector3(cameraPos.position.x, Mathf.Clamp(cameraPos.position.y, (-maxVertical / 10), maxVertical), cameraPos.position.z);
        transform.LookAt(lookAt);
    }

    public void GetDamage()
    {
        gameObject.SetActive(false);
    }
}
