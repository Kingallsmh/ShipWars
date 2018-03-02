using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEntityInterpret : NetworkBehaviour {

    public List<BossScript> scriptsToRun;
    public int maxHealth = 10;
    [SyncVar(hook = "OnHealthChange")]
    public int health = 0;

    private void Start()
    {
        if(isServer){
            health = maxHealth;
            for (int i = 0; i < scriptsToRun.Count; i++)
            {
                scriptsToRun[i].Init();
            }
        }
    }

    public bool AreWeLocalClient(){
        return isLocalPlayer;
    }

    // Update is called once per frame
    void Update () {
        if (isServer)
        {
            for (int i = 0; i < scriptsToRun.Count; i++)
            {
                scriptsToRun[i].FloatAround();
            }
        }
	}

    [Command]
    public void CmdFirBullet(GameObject bullet){
        Debug.Log("Fire!");
        NetworkServer.Spawn(bullet);
    }

    [Command]
    public void CmdChangeHealth(int newHealth){
        health = newHealth;
    }

    void OnHealthChange(int newHealth){
        for (int i = 0; i < scriptsToRun.Count; i++){
            StartCoroutine(scriptsToRun[i].GetComponent<StatusScript>().FlashingDmgIndicator(0.1f));
        }
    }
}
