using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject EnemyPrefab;
    public int NumberOfEnemies;

    public override void OnStartServer()
    {
        for (int i = 0; i  < NumberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.0f,8.0f), 0.0f, Random.Range(-8.0f,8.0f));
            Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 180.0f), 0);

            GameObject enemy = (GameObject) Instantiate(EnemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }







}
