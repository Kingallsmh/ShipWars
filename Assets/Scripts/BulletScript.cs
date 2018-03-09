using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour {

    public string TagToNotHit = "Nothing";
    public float speed = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != TagToNotHit)
        {
            if(other.GetComponent<StatusScript>()){
                other.GetComponent<StatusScript>().TakeDmg(1);
            }
            Destroy(gameObject);
        }
    }
}
