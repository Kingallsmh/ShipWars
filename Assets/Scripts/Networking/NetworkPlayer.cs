using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    public NetworkClient client;
    public ShipEntity ship;
    public Camera playerCam;

	// Use this for initialization
	void Start () {
        if(isLocalPlayer){
            Debug.Log("Network client: " + NetworkManager.singleton.client.connection);
            client = NetworkManager.singleton.client;
            GameManagerScript.Instance.InitPlayer(this);
        }
        if(isClient){
            GameManagerScript.Instance.AddPlayer(this);
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
}
