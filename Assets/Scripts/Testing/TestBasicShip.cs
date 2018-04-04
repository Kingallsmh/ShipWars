using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBasicShip : MonoBehaviour {

    protected Vector3 velocity, rotation;
    public float rollFactor = 1, pitchFactor = 1;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    Rigidbody rb;
    public float speed = 2;

    Controller control;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        control = GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        //Controls
        //Debug.Log(control.StickInput.x);
        rotation = new Vector3(control.StickInput.y, 0, -control.StickInput.x);
        rotation.z = rotation.z * rollFactor;
        if (rotation.x > 0)
        {
            rotation.x = rotation.x * (pitchFactor / 1.5f);
        }
        else
        {
            rotation.x = rotation.x * (pitchFactor);
        }

        Movement();
	}

    public void Movement()
    {
        //Push ship forward in current facing direction at current maxSpeed
        rb.velocity = transform.forward * speed * 100 * Time.deltaTime;
        //Rotate local transform
        transform.localRotation = Quaternion.Euler(rotation) * transform.localRotation;
    }
}
