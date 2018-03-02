using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    //TODO try turning this into just a thing to read player input

    public GameObject bulletObject;
    public Transform bulletSpawn;
    Rigidbody rb;
    public GameObject visualAnim; //For this object spin effect

    public float speed = 10;

    public Vector2 minBounds, maxBounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(isLocalPlayer){
            Camera.main.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update () {

        //Animation of spinning looking ship thing :D
        visualAnim.transform.Rotate(0, 0, 10f);

        if(isLocalPlayer){
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 10f * speed;
            float y = Input.GetAxis("Vertical") * Time.deltaTime * 10f * speed;

            LimitToBorders(x, y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdFire();
            }
        }

	}

    public void LimitToBorders(float x, float y){
        float xPos = (x * Time.deltaTime) + transform.position.x;
        float yPos = (y * Time.deltaTime) + transform.position.y;
        if(xPos < minBounds.x){
            float dif = minBounds.x + 0.1f - transform.position.x;
            transform.position += new Vector3(dif, 0, 0);
            x = 0;
        } 
        else if(xPos > maxBounds.x){
            float dif = maxBounds.x - 0.1f - transform.position.x;
            transform.position += new Vector3(dif, 0, 0);
            x = 0;
        }
        if (yPos < minBounds.y){
            float dif = minBounds.y + 0.1f - transform.position.y;
            transform.position += new Vector3(0, dif, 0);
            y = 0;
        }
            else if(yPos > maxBounds.y)
        {
            float dif = maxBounds.y - 0.1f - transform.position.y;
            transform.position += new Vector3(0, dif, 0);
            y = 0;
        }
        rb.velocity = new Vector3(x, y, 0);
        Debug.Log("Velocity: " + rb.velocity);
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
