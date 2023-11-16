using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("ZombieSpawn var")]
    public GameObject ZombiePrefab;
    public Transform ZombieSpawnPosition;
    public GameObject DangerZone1;
    private float repeatCycle = 1f;

    private void OnTriggerEnter(Collider other)
    {
        InvokeRepeating("EnemySpawner", 1f, repeatCycle);
        Destroy(gameObject, 100f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }


    void EnemySpawner()
    {
        Instantiate(ZombiePrefab, ZombieSpawnPosition.position,ZombieSpawnPosition.rotation);
    }

}
