using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BossScript : MonoBehaviour {

    //Turn this into an abstract script that gets inherited by a more specialized script

    public GameObject bossBall;
    public Transform spawnPoint;
    public PlayerController playerTarget;
    Rigidbody rb;
    Vector3 direction;
    public float maxSpeed = 20;
    public float waitPotentialMove = 10;
    public float waitPotentialShoot = 5;

    public NetworkEntityInterpret NEI;

    public Vector2 minBounds, maxBounds;

    public void Init(){
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ChangeMoveOnTimer());
        if (bossBall)
        {
            Debug.Log("IsLocal? " + NEI.AreWeLocalClient());
            StartCoroutine(FireOnTimer());
        }
    }

    public void FloatAround(){
        isWithinBounds();
    }

    IEnumerator ChangeMoveOnTimer(){
        float seconds = 0;
        while(gameObject){
            StartDirection();
            seconds = Random.Range(0, waitPotentialMove);
            yield return new WaitForSeconds(seconds);
        }
    }

    IEnumerator FireOnTimer(){
        float seconds = 0;
        while (gameObject)
        {
            seconds = Random.Range(0, waitPotentialShoot);
            yield return new WaitForSeconds(seconds);
            Fire();
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bossBall,
            spawnPoint.position,
            spawnPoint.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 15;

        //NetworkServer.Spawn(bullet);
        NEI.CmdFirBullet(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    void isWithinBounds(){
        if (transform.position.x < maxBounds.x && transform.position.x > minBounds.x && 
            transform.position.y < maxBounds.y && transform.position.y > minBounds.y){
            rb.velocity = direction;
        }
        else{
            SetDirection();
        }
    }

    public void SetDirection(){
        float x = direction.x;
        float y = direction.y;
        if (transform.position.x > maxBounds.x)
        {
            x = Random.Range(-maxSpeed, 0);
        }
        else if (transform.position.x < minBounds.x)
        {
            x = Random.Range(0, maxSpeed);
        }

        if (transform.position.y > maxBounds.y)
        {
            y = Random.Range(-maxSpeed, 0);
        }
        else if (transform.position.y < minBounds.y)
        {
            y = Random.Range(0, maxSpeed);
        }
        direction = new Vector3(x, y, 0);
        rb.velocity = direction;
    }

    public void StartDirection(){
        float x = Random.Range(-maxSpeed, maxSpeed);
        float y = Random.Range(-maxSpeed, maxSpeed);
        direction = new Vector3(x, y, 0);
    }
}
