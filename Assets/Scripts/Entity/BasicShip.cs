using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BasicShip : ShipEntity {

    public float rollFactor = 1, pitchFactor = 1;
    public GameObject bullet;
    public Transform bulletSpawn;
    public float Fire1CD = 1f;
    public bool Fire1OK = true;

    public List<Collider> ignoreList;

	private void Start()
	{
        Init();
	}

	public override void ControlLoop()
	{
        base.ControlLoop();
        GatherInput();
	}

	public override void FixedControlLoop()
	{
        base.FixedControlLoop();
        Movement();
	}

	public void GatherInput(){
        control.GetInput();

        //Controls
        rotation = new Vector3(control.StickInput.y, 0, -control.StickInput.x);
        rotation.z = rotation.z * rollFactor;
        if(rotation.x > 0){
            rotation.x = rotation.x * (pitchFactor / 1.5f);
        }
        else{
            rotation.x = rotation.x * (pitchFactor);
        }

        //Shoot
        if(control.Btn1 && Fire1OK){
            PrimaryActionFire();
            StartCoroutine(Fire1Cooldown());
        }
    }

    public IEnumerator Fire1Cooldown(){
        Fire1OK = false;
        yield return new WaitForSeconds(Fire1CD);
        Fire1OK = true;
    }

	public override void Movement()
	{
        //Push ship forward in current facing direction at current maxSpeed
        rb.velocity = transform.forward * maxSpeed * 100 * Time.deltaTime;
        //Rotate local transform
        transform.localRotation *= Quaternion.Euler(rotation);
	}

	public override void PrimaryActionFire()
	{
        CmdFireBullet(bulletSpawn.position, bulletSpawn.rotation);
        //GameManagerScript.Instance.PutMessageInDebug("Pos: " + bulletSpawn.position + " Rot: " + bulletSpawn.rotation);
	}

    [Command]
    public void CmdFireBullet(Vector3 clientSpawnPos, Quaternion clientSpawnRot)
    {
        //Debug.Log("Pos: " + clientSpawnPos + " Rot: " + clientSpawnRot);
        var b = (GameObject)Instantiate(
            bullet,
            clientSpawnPos,
            clientSpawnRot);

        // Add velocity to the bullet
        float bulletVel = b.GetComponent<BulletScript>().speed;
        b.GetComponent<BulletScript>().SetIgnoreList(ignoreList);
        b.GetComponent<Rigidbody>().velocity = (b.transform.forward * bulletVel) + rb.velocity;

        NetworkServer.Spawn(b);
        RpcFireBullet(b);
        // Destroy the bullet after 2 seconds
        Destroy(b, 5f);
    }

    [ClientRpc]
    public void RpcFireBullet(GameObject obj)
    {
        obj.GetComponent<BulletScript>().SetIgnoreList(ignoreList);
    }
}
