using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour {

    public string TagToNotHit = "Nothing";
    public float speed = 20;
    public List<Collider> ignoreList = new List<Collider>();

    public void SetIgnoreList(List<Collider> list)
    {
        ignoreList = list;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("What? Bullet was fired?");
        if (other.gameObject.tag != TagToNotHit || !ignoreList.Contains(other))
        {
            if(other.GetComponent<StatusScript>()){
                other.GetComponent<StatusScript>().TakeDmg(1);
            }
            Destroy(gameObject);
        }
    }
}
