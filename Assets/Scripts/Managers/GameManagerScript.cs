using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour {
    public Camera mainCam;

    public static GameManagerScript Instance;
    public List<NetworkPlayerScript> playerList;
    public List<EnemySpawner> playerSpawnList;
    public List<EnemySpawner> enemySpawnList;

    public List<ShipEntity> shipList;
    public GameObject viewerObj;
    public Text textObj;
    public Text debugText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetGUIIP();
        PutMessageInDebug("Started GameManager");
    }

    public void SpawnPlayer(NetworkPlayerScript player){
        int spot = playerList.IndexOf(player);
        Debug.Log(spot);
        player.ship.transform.position = playerSpawnList[spot].transform.position;
        player.ship.transform.rotation = playerSpawnList[spot].transform.rotation;
    }

    public void AddPlayer(NetworkPlayerScript newPlayer){
        if(playerList == null){
            playerList = new List<NetworkPlayerScript>();
        }
        if(!playerList.Contains(newPlayer)){
            playerList.Add(newPlayer);
        }
    }

    public void InitPlayer(NetworkPlayerScript newPlayer){
        GivePlayerShip(shipList[0], newPlayer);
    }

	public void GivePlayerShip(ShipEntity ship, int numOfPlayer){
        //Change to selection of ship
        playerList[numOfPlayer].ship = Instantiate(ship);
    }

    public void GivePlayerShip(ShipEntity ship, NetworkPlayerScript player){
        //Change to selection of ship
        player.ship = Instantiate(ship);
    }

    public GameObject GetAShip(){
        return shipList[0].gameObject;
    }

    public void SetGUIIP()
    {
        string ip = Network.player.ipAddress;
        textObj.text = "IP: " + ip;
        Debug.Log(ip);
    }

    public void PutMessageInDebug(string msg)
    {
        debugText.text = msg;
    }
}
