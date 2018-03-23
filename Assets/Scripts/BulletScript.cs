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
        if (other.gameObject.tag != TagToNotHit || !ignoreList.Contains(other))
        {
            if(other.GetComponent<StatusScript>()){
                other.GetComponent<StatusScript>().CmdTakeDamage(1);
            }
            else if(other.transform.parent && other.transform.parent.GetComponent<StatusScript>()){
                other.transform.parent.GetComponent<StatusScript>().CmdTakeDamage(1);
            }
            else if(other.GetComponent<DamageRedirect>()){
                other.GetComponent<DamageRedirect>().statusToDirect.CmdTakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
