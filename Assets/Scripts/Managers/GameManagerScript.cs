using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour {

    public static GameManagerScript Instance;
    public List<NetworkPlayer> playerList;
    public List<EnemySpawner> playerSpawnList;
    public List<EnemySpawner> enemySpawnList;

    public List<ShipEntity> shipList;
    public GameObject viewerObj;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPlayer(NetworkPlayer player){
        int spot = playerList.IndexOf(player);
        Debug.Log(spot);
        player.ship.transform.position = playerSpawnList[spot].transform.position;
        player.ship.transform.rotation = playerSpawnList[spot].transform.rotation;
    }

    public void AddPlayer(NetworkPlayer newPlayer){
        if(playerList == null){
            playerList = new List<NetworkPlayer>();
        }
        if(!playerList.Contains(newPlayer)){
            playerList.Add(newPlayer);
        }
    }

    public void InitPlayer(NetworkPlayer newPlayer){
        GivePlayerShip(shipList[0], newPlayer);
    }

	public void GivePlayerShip(ShipEntity ship, int numOfPlayer){
        //Change to selection of ship
        playerList[numOfPlayer].ship = Instantiate(ship);
    }

    public void GivePlayerShip(ShipEntity ship, NetworkPlayer player){
        //Change to selection of ship
        player.ship = Instantiate(ship);
    }

    public GameObject GetAShip(){
        return shipList[0].gameObject;
    }
}
