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
        GameManagerScript.Instance.AddPlayer(this);
        myship = GameManagerScript.Instance.GetAShip();
        if(isLocalPlayer){
            //GameManagerScript.Instance.InitPlayer(this);
            Camera.main.gameObject.SetActive(false);
            playerCam.gameObject.SetActive(true);
            CmdNetworkSpawnShip();
            AttachCameraTo(ship.gameObject, ship.camPoint);
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
        ship = Instantiate(myship);
        NetworkServer.Spawn(ship.gameObject);
    }
}
