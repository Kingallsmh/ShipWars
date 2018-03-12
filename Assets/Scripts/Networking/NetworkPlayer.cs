using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    public NetworkClient client;
    public ShipEntity ship;
    ShipEntity myship;
    public Camera playerCam;

	// Use this for initialization
	void Start () {
        //Put player in list for GameManager to keep track
        //GameManagerScript.Instance.AddPlayer(this);
        //myship = GameManagerScript.Instance.GetAShip();
        if(isLocalPlayer){
            //GameManagerScript.Instance.InitPlayer(this);
            SpawnShip();
            Camera.main.gameObject.SetActive(false);
            playerCam.gameObject.SetActive(true);
            
            CmdNetworkSpawnShip();
            Debug.Log(ship);
        }
	}
	
	void Update () {
        if(isLocalPlayer){
            if (ship){
                ship.ControlLoop();
            }
        }
	}

	private void FixedUpdate() {
        if (isLocalPlayer){
            if (ship){
                ship.FixedControlLoop();
            }
        }
	}

    public void AttachCameraTo(GameObject camAttach){
        playerCam.transform.parent = camAttach.transform;
        playerCam.transform.position = Vector3.zero;
        playerCam.transform.rotation = Quaternion.identity;
    }

    public void AttachCameraTo(GameObject camAttach, Transform suggestedPos)
    {
        playerCam.transform.parent = camAttach.transform;
        playerCam.transform.position = suggestedPos.position;
        playerCam.transform.rotation = suggestedPos.rotation;
    }

    [Command]
    public void CmdNetworkSpawnShip(){
        Debug.Log("Get Ship on Server");
        GameObject s = Instantiate(GameManagerScript.Instance.GetAShip());
        NetworkServer.Spawn(s);
        RpcSpawnShip(s.gameObject, netId);
    }

    [ClientRpc]
    public void RpcSpawnShip(GameObject _ship, NetworkInstanceId id)
    {
        if (NetworkServer.FindLocalObject(id))
        {
            Debug.Log("Attach a ship to player: " + id);
            ship = _ship.GetComponent<ShipEntity>();
            AttachCameraTo(ship.gameObject, ship.camPoint);
        }
        
    }

    public void SpawnShip()
    {
        //ship = Instantiate(selectShip);
    }
}
