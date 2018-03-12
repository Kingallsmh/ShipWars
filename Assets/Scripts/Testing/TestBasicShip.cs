using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestBasicShip : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    Rigidbody rb;
    public float speed = 2;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            
            return;
        }
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
	}

    public override void OnStartLocalPlayer()
    {
        transform.Rotate(0, 180, 0);
    }

    [Command]
    public void CmdFire()
    {
        var b = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        //float bulletVel = b.GetComponent<BulletScript>().speed;
        b.GetComponent<Rigidbody>().velocity = (b.transform.forward * 30) + rb.velocity;

        NetworkServer.Spawn(b);

        // Destroy the bullet after 2 seconds
        Destroy(b, 2f);
    }
}
