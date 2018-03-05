using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StatusScript : MonoBehaviour {

    public bool canBeHit = true;
    bool flashing = false;
    public float invincibleWait = 2f;
    public int maxHealth = 10;
    public int health;
    public MeshRenderer affectedByDmg;
    public SkinnedMeshRenderer affectedByDmgSkinned;
    public Material flashMat;

    public NetworkEntityInterpret NEI;
    PlayerUI ui;

	// Use this for initialization
	public void Init () {
        ui = GetComponent<PlayerUI>();
        if(ui){
            UIManagerScript.Instance.SetPlayerUI(ui);
            ui.ShowUI(true);
        }
        Heal();
        //TakeDmg(1);
	}

    public void Heal(){
        health = maxHealth;
        if(ui){
            ui.UpdatePlayerHealth(health);
        }
    }

    public void Heal(int amount){
        health += amount;
        if(health > maxHealth){
            health = maxHealth;
        }
        if (ui)
        {
            ui.UpdatePlayerHealth(health);
        }
    }

    //TODO make able to work with varying types of entities
    public void TakeDmg(int amount){
        if(!canBeHit){
            return;
        }
        StartCoroutine(InvicibleTime());

        if(NEI){
            NEI.health -= amount;
        }
        else{
            if (affectedByDmg || affectedByDmgSkinned)
            {
                StartCoroutine(FlashingDmgIndicator(0.1f));
                health -= amount;
                if (ui)
                {
                    ui.UpdatePlayerHealth(health);
                }
            }
        }
    }

    IEnumerator InvicibleTime(){
        canBeHit = false;
        yield return new WaitForSeconds(invincibleWait);
        canBeHit = true;
    }

    public IEnumerator FlashingDmgIndicator(float flashLength){
        Debug.Log("Flash!");

        if (affectedByDmg && !flashing)
        {
            flashing = true;
            Material[] matlist = affectedByDmg.materials;
            Material[] flashlist = new Material[matlist.Length];
            for (int i = 0; i < flashlist.Length; i++)
            {
                flashlist[i] = flashMat;
            }
            while (!canBeHit)
            {
                for (int i = 0; i < affectedByDmg.materials.Length; i++)
                {
                    affectedByDmg.materials = flashlist;
                }
                yield return new WaitForSeconds(flashLength);
                affectedByDmg.materials = matlist;
                yield return new WaitForSeconds(flashLength);
            }
            affectedByDmg.materials = matlist;
        }
        else if (affectedByDmgSkinned && !flashing)
        {
            flashing = true;
            Material[] matlist = affectedByDmgSkinned.materials;
            Material[] flashlist = new Material[matlist.Length];
            for (int i = 0; i < flashlist.Length; i++)
            {
                flashlist[i] = flashMat;
            }
            while (!canBeHit)
            {
                for (int i = 0; i < affectedByDmgSkinned.materials.Length; i++)
                {
                    affectedByDmgSkinned.materials = flashlist;
                }
                yield return new WaitForSeconds(flashLength);
                affectedByDmgSkinned.materials = matlist;
                yield return new WaitForSeconds(flashLength);
            }
            affectedByDmgSkinned.materials = matlist;
        }
        flashing = false;
    }
}
