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

    //Just for testing
    public List<Turret> turretList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

	private void OnEnable()
	{
        SetGUIIP();
        CloseLogin();
<<<<<<< HEAD
	}
=======
    }
>>>>>>> 930d15d3a5fecbb0d9130062fd78884f8513747d

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

    public void RemoveFromPlayerList(NetworkPlayerScript player)
    {
        playerList.Remove(player);
    }
    
    public void InitPlayer(GameObject newPlayer){
        AddPlayer(newPlayer.GetComponent<NetworkPlayerScript>());
    }
    
    public int GetPlayerNum(GameObject newPlayer)
    {
        return playerList.IndexOf(newPlayer.GetComponent<NetworkPlayerScript>());
    }

	public void GivePlayerShip(ShipEntity ship, int numOfPlayer){
        //Change to selection of ship
        playerList[numOfPlayer].ship = Instantiate(ship);
    }

    public void GivePlayerShip(ShipEntity ship, NetworkPlayerScript player){
        //Change to selection of ship
        player.ship = Instantiate(ship);
    }

    public GameObject GetAShip(int num){
        return shipList[num].gameObject;
    }

    public Transform GetSpawn(int spotNum)
    {
        return playerSpawnList[spotNum].transform;
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

    [Command]
    public void CmdSetTurretToTargetPlayer(int numOfPlayer){
        for (int i = 0; i < turretList.Count; i++){
            turretList[i].SetTarget(playerList[numOfPlayer].ship.gameObject);
        }
        RpcSetTurretToTargetPlayer(numOfPlayer);
    }

    [ClientRpc]
    public void RpcSetTurretToTargetPlayer(int numOfPlayer){
        for (int i = 0; i < turretList.Count; i++){
            turretList[i].SetTarget(playerList[numOfPlayer].ship.gameObject);
        }
    }

    public void CloseLogin(){
        LoginManager.Instance.obj.SetActive(false);
    }
}
