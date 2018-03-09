using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject entityPrefab;
    public int numberOfEnemies;
    public SpawnType spawner = SpawnType.Minions;
    public enum SpawnType{
        Player, Minions, Boss
    }

    //public override void OnStartServer()
    //{
    //    switch(spawner){
    //        case SpawnType.Minions: SpawnMinions();
    //            break;
    //        case SpawnType.Boss: 
    //            StartCoroutine(SpawnBoss(5f));
    //            break;
    //        case SpawnType.Player:
    //            SpawnPlayer();
    //            break;
    //    }
    //}

    void SpawnPlayer(){
        GameObject player = Instantiate(entityPrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(player);
    }

    void SpawnMinions(){
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-8.0f, 8.0f),
                Random.Range(-8.0f, 8.0f), transform.position.z
                );

            var enemy = (GameObject)Instantiate(entityPrefab, spawnPosition, transform.rotation);
            NetworkServer.Spawn(enemy);
        }
    }

    void SpawnTheBoss(){
        Debug.Log("Spawn Boss!");
        GameObject boss = Instantiate(entityPrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(boss);
    }

    IEnumerator SpawnBoss(float spawnWaitTime){
        yield return new WaitForSeconds(spawnWaitTime);
        Debug.Log("Spawn Boss!");
        GameObject boss = Instantiate(entityPrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(boss);
    }
}
