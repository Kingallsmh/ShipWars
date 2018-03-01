using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StatusScript : NetworkBehaviour {

    public bool canBeHit = true;
    public float invincibleWait = 2f;
    public int maxHealth = 10, health;
    public List<MeshRenderer> affectedByDmgObjects;
    public Material flashMat;

	// Use this for initialization
	void Start () {
        Heal();
	}

    public void Heal(){
        health = maxHealth;
    }

    public void Heal(int amount){
        health += amount;
        if(health > maxHealth){
            health = maxHealth;
        }
    }

    public void TakeDmg(int amount){
        if(!canBeHit){
            return;
        }
        StartCoroutine(InvicibleTime());
        if(affectedByDmgObjects != null){
            StartCoroutine(FlashingDmgIndicator(0.1f));
        }
        health -= amount;
        if(health < 0){
            health = 0;
        }
    }

    IEnumerator InvicibleTime(){
        canBeHit = false;
        yield return new WaitForSeconds(invincibleWait);
        canBeHit = true;
    }

    IEnumerator FlashingDmgIndicator(float flashLength){
        List<Material> matlist = new List<Material>();
        for (int i = 0; i < affectedByDmgObjects.Count; i++){
            matlist.Add(affectedByDmgObjects[i].material);
        }

        while(!canBeHit){
            for (int i = 0; i < affectedByDmgObjects.Count; i++){
                Color c = affectedByDmgObjects[i].material.color;
                affectedByDmgObjects[i].material = flashMat;
            }
            yield return new WaitForSeconds(flashLength);
            for (int i = 0; i < affectedByDmgObjects.Count; i++)
            {
                Color c = affectedByDmgObjects[i].material.color;
                affectedByDmgObjects[i].material = matlist[i];
            }
            yield return new WaitForSeconds(flashLength);
        }
    }
}
