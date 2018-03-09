using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShip : MonoBehaviour {

    Rigidbody rb;
    public float rollAdjust = 1, pitchAdjust = 1;
    public float speed = 1;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("r")){
            transform.position = new Vector3(0, 80, -900);
            transform.rotation = Quaternion.identity;
        }
	}

    private void FixedUpdate()
    {
        Vector2 rotInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rotInput.x *= rollAdjust;
        rotInput.y *= pitchAdjust;

        transform.localRotation *= Quaternion.Euler(rotInput.y, 0, -rotInput.x);
        rb.velocity = transform.forward * 100 * Time.deltaTime * speed;
    }
}
