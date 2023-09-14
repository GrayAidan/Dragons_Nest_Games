using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private bool spawned;

    private int managerMaxTiles;

    void Start()
    {
        managerMaxTiles = GameObject.Find("GameManager").GetComponent<LevelLayoutManager>().maximumTiles;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            SpawnEnemies();
            spawned = true;
        }
    }

    public void SpawnEnemies()
    {

    }
}
