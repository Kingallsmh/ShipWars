using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript Instance;
    public Transform sceneCam;
    public bool isPlayer = true;
    public List<NetworkPlayer> playerList;
    public List<EnemySpawner> playerSpawnList;
    public List<EnemySpawner> enemySpawnList;

    public List<ShipEntity> shipList;
    public GameObject viewerObj;

    private void Awake()
    {
        Instance = this;
    }

    public void UnattachCamera(){
        Camera.main.transform.parent = null;
    }

    public void ClickShip(){
        isPlayer = true;
    }

    public void ClickViewer(){
        isPlayer = false;
    }

    public void SpawnPlayer(NetworkPlayer player, int spot){
        player.ship.transform.position = playerSpawnList[spot].transform.position;
        player.ship.transform.rotation = playerSpawnList[spot].transform.rotation;
    }

    public void SpawnViewer(){
        
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
        newPlayer.AttachCameraTo(newPlayer.ship.gameObject, newPlayer.ship.camPoint);
        SpawnPlayer(newPlayer, playerList.IndexOf(newPlayer));
    }

	public void GivePlayerShip(ShipEntity ship, int numOfPlayer){
        //Change to selection of ship
        playerList[numOfPlayer].ship = Instantiate(ship);
    }

    public void GivePlayerShip(ShipEntity ship, NetworkPlayer player){
        //Change to selection of ship
        player.ship = Instantiate(ship);
    }
}
