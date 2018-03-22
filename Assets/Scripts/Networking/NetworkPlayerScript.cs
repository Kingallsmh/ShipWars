using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerScript : NetworkBehaviour {

    public NetworkClient client;
    public ShipEntity ship;
    ShipEntity myship;
    public Camera playerCam;
    Camera mainCam;

	// Use this for initialization
	void Start () {
        //Put player in list for GameManager to keep track
        //GameManagerScript.Instance.AddPlayer(this);
        //myship = GameManagerScript.Instance.GetAShip();
        GameManagerScript.Instance.InitPlayer(gameObject);
        if (isLocalPlayer){ 
            mainCam = GameManagerScript.Instance.mainCam;
            mainCam.gameObject.SetActive(false);
            playerCam.gameObject.SetActive(true);
            GameManagerScript.Instance.PutMessageInDebug(Application.platform.ToString());
            CmdNetworkSpawnShip(netId, GameManagerScript.Instance.GetPlayerNum(gameObject), LoginManager.Instance.GetPlayerSelect());
            Debug.Log(GameManagerScript.Instance.GetPlayerNum(gameObject));
        }
	}

    void Update () {
        if(isLocalPlayer){
            if (ship){
                ship.ControlLoop();
                if (Input.GetKeyDown(KeyCode.R))
                {
                    CmdResetPosition(ship.gameObject);
                }
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

    [Command]
    public void CmdResetPosition(GameObject _ship){
        if(ship){
            int spawnNum = GameManagerScript.Instance.GetPlayerNum(gameObject);
            Transform spot = GameManagerScript.Instance.GetSpawn(spawnNum);
            _ship.transform.position = spot.position;
            _ship.transform.rotation = spot.rotation;
        }
    }

    private void OnDestroy()
    {
        GameManagerScript.Instance.RemoveFromPlayerList(this);
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
    public void CmdUnspawnShip() {
        Debug.Log("Get Ship off the Server!");
        //Unspawn is not needed. If it is destroyed on the server, it is destroyed on clients
        //NetworkServer.UnSpawn(ship.gameObject);
        Destroy(ship.gameObject);
    }

    [Command]
    public void CmdNetworkSpawnShip(NetworkInstanceId id, int spawnNum, int numOfShip){
        Debug.Log("Get Ship on Server");
        Debug.Log(isServer);
        GameObject s;
        if(numOfShip == 0){
           s  = Instantiate(GameManagerScript.Instance.GetAShip(numOfShip));
        }
        else{
            s = Instantiate(GameManagerScript.Instance.GetAShip(numOfShip), GameManagerScript.Instance.GetSpawn(spawnNum));
        }
        //Before Network spawn, init() isServer is false. After, it's true.
        NetworkServer.SpawnWithClientAuthority(s, gameObject);
        if(s.GetComponent<StatusScript>()){
            s.GetComponent<StatusScript>().Init();
        }
        RpcSpawnShip(s.gameObject, id);
    }

    [ClientRpc]
    public void RpcSpawnShip(GameObject _ship, NetworkInstanceId id)
    {
        Debug.Log("Attach a ship to player: " + id);
        Debug.Log("NetID: " + netId);
        if (ClientScene.FindLocalObject(id))
        {
            ClientScene.FindLocalObject(id).GetComponent<NetworkPlayerScript>().ship = _ship.GetComponent<ShipEntity>();            
            //AttachCameraTo(ship.gameObject, ship.camPoint);
        }
        if (netId == id && isLocalPlayer)
        {
            //Because all of this only called on the client and not on the server, 
            //the syncVars will not work as they are not updated on the server
            Debug.Log("Player: " + ClientScene.FindLocalObject(id));
            if(_ship.GetComponent<StatusScript>()){
                _ship.GetComponent<StatusScript>().Init(
                ClientScene.FindLocalObject(id).GetComponent<PlayerUI>());
            }
            AttachCameraTo(ship.gameObject, ship.camPoint);
            Debug.Log("ISLocal");
            _ship.GetComponent<ShipEntity>().Init();
        }
    }
}
