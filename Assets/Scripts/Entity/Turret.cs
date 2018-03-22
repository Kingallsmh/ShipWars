using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Turret : NetworkBehaviour {
    
    public GameObject bullet;
    public Transform turretHead, bulletSpawn;
    public GameObject target;
    public List<Collider> ignoreList;
    public float aimSpeed;
    public float maxRange = 500;
    bool fireAtWill = true;
    public float cooldownTime = 1;

    //TEMP
    public float bulletSpeed = 20;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            FindTarget(target);
        }        
    }

    IEnumerator FireCooldown()
    {
        fireAtWill = false;
        yield return new WaitForSeconds(cooldownTime);
        fireAtWill = true;
    }

    private void FireBullet()
    {
        if (fireAtWill) {
            Debug.Log("FIRE");
            StartCoroutine(FireCooldown());
            // Create the Bullet from the Bullet Prefab
            var b = (GameObject)Instantiate(
                bullet,
                bulletSpawn.position,
                bulletSpawn.rotation);

            // Add velocity to the bullet
            b.GetComponent<BulletScript>().SetIgnoreList(ignoreList);
            b.GetComponent<Rigidbody>().velocity = b.transform.forward * bulletSpeed * 10;
            

            // Destroy the bullet after 2 seconds
            //TODO make a function that destroys bullet after distance
            Destroy(b, 5f);
        }
    }

    private void FireAtTarget(GameObject obj)
    {
        RaycastHit hit;
        Debug.DrawRay(bulletSpawn.position, bulletSpawn.forward * 100);
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward * maxRange, out hit, maxRange))
        {
            if (hit.transform == obj.transform)
            {
                FireBullet();
            }
        }
        //RaycastHit[] hits;
        //hits = Physics.RaycastAll(turretHead.position, transform.forward, maxRange);
        
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    if (hits[i].transform == obj.transform)
        //    {
        //        FireBullet();
        //    }
        //}
    }

    private void AimAtTarget(GameObject obj)
    {
        //LIMITED Rotation caused by Movement of Reticle
        Quaternion limitedRotation = new Quaternion(0F, 0F, 0F, 0F);
        Quaternion rotation = Quaternion.LookRotation(obj.transform.position - bulletSpawn.position);
        //lock rotation, yaw only
        //rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);

        //rotates the Ship to Reticle with fixed speed
        limitedRotation = Quaternion.RotateTowards(turretHead.rotation, rotation, aimSpeed * Time.deltaTime);

        turretHead.rotation = limitedRotation;
        FireAtTarget(obj);
    }

    public void FindTarget(GameObject obj)
    {

        if (Vector3.Distance(turretHead.position, obj.transform.position) < maxRange)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(turretHead.position, (obj.transform.position - turretHead.position), maxRange);

            List<RaycastHit> list = SortedHitArray(hits);

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].transform == obj.transform)
                {
                    AimAtTarget(obj);
                }
                bool ignore = false;
                for (int j = 0; j < ignoreList.Count; j++)
                {
                    if (list[i].collider == ignoreList[j])
                    {
                        ignore = true;
                    }
                }
                if (!ignore)
                {
                    break;
                }
            }
        }        
    }

    public List<RaycastHit> SortedHitArray(RaycastHit[] array)
    {
        List<RaycastHit> newList = new List<RaycastHit>();
        List<RaycastHit> oldList = new List<RaycastHit>();
        for (int a = 0; a < array.Length; a++) {
            oldList.Add(array[a]);
        }

        for(int i = 0; i < array.Length; i++)
        {
            RaycastHit shortest = oldList[0];
            for(int j = 0; j < oldList.Count; j++)
            {
                if(shortest.distance > oldList[j].distance)
                {
                    shortest = oldList[j];
                }
            }
            newList.Add(shortest);
            oldList.Remove(shortest);
        }

        //Debug.Log("List refresh here - - - - + + + +");
        //for(int i = 0; i < newList.Count; i++)
        //{
        //    Debug.Log("#" + i + " : " + newList[i].distance);
        //}

        return newList;
    }

    public void SetTarget(GameObject _target){
        target = _target;
    }
}
