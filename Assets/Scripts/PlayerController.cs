using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletObject;
    public Transform bulletSpawn;
    public GameObject visualAnim; //For this object spin effect

	// Update is called once per frame
	void Update () {

        //Animation of spinning looking ship thing :D
        visualAnim.transform.Rotate(0, 0, 10f);

        if(isLocalPlayer){
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 10f;
            float y = Input.GetAxis("Vertical") * Time.deltaTime * 10f;

            transform.Translate(x, y, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdFire();
            }
        }

	}

    //Good place to init cameras and input
    public override void OnStartLocalPlayer()
    {
        visualAnim.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletObject,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 15;

        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}
