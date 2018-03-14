using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StatusScript : NetworkBehaviour {

    public bool canBeHit = true;
    bool flashing = false;
    public float invincibleWait = 2f;
    public int maxHealth = 10;
    [SyncVar(hook = "OnChangeHealth")]
    public int health;
    public MeshRenderer affectedByDmg;
    public SkinnedMeshRenderer affectedByDmgSkinned;
    public Material flashMat;

    public NetworkEntityInterpret NEI;
    public PlayerUI ui;


    public void Init()
    {
        Heal();
    }

	// Use this for initialization
	public void Init (PlayerUI _ui) {
        if (_ui)
        {
            ui = _ui;
            ui.ShowUI(true);
            ui.UpdatePlayerHealth(health);
        }
        //TakeDmg(1);
	}

    public void Heal(){
        Debug.Log("Server? " + isServer);
        if (!isServer)
        {
            return;
        }
        Debug.Log("Heal!");
        health = maxHealth;
    }

    public void Heal(int amount){
        if (!isServer)
        {
            return;
        }
        health += amount;
        if(health > maxHealth){
            health = maxHealth;
        }
        if (ui)
        {
            ui.UpdatePlayerHealth(health);
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isServer || !canBeHit)
        {
            return;
        }
        health -= amount;
        if(health < 0)
        {
            health = 0;
            Debug.Log("FOOOOOOXXXXXXX!!!");
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

    void OnChangeHealth(int health)
    {
        Debug.Log("Health of hit target: " + health);
        if (ui)
        {
            ui.UpdatePlayerHealth(health);
        }
    }
}
