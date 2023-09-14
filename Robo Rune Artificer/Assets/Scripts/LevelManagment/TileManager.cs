using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public int tileNumber;
    private int maxTiles;

    private bool enemiesSpawned;

    private GameObject player;
    private GameObject grouping;
    private Transform enemyGrouping;
    private LevelLayoutManager _lm;

    public GameObject circleSpawn;

    public List<Transform> setSpawns;
    public List<Transform> setSpawnsRanged;

    // Start is called before the first frame update
    void Start()
    {
        _lm = GameObject.Find("GameManager").GetComponent<LevelLayoutManager>();
        player = GameObject.Find("Player");
        enemyGrouping = GameObject.Find("Grouping").transform.Find("Enemies");

        maxTiles = _lm.maximumTiles;
        grouping = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemiesSpawned)
        {
            SpawnEnemies(difficultyLevel());
        }
        else 
        {
            ObjectDissapear();
        }
    }

    public int difficultyLevel()
    {
        int difficulty = 0;
        float tileThird = (maxTiles / 3) + 1;

        if (tileNumber <= tileThird)
        {
            difficulty = 1; //lowest
        }
        else if (tileNumber > tileThird && tileNumber <= tileThird * 2)
        {
            difficulty = 2; //medium
        }
        else if(tileNumber > tileThird * 2)
        {
            difficulty = 3; //highest
        }

        return difficulty;
    }

    public void SpawnEnemies(int difficulty)
    {
        int enemyAmount = 0;
        int enemyVariantChance = -1;
        int enemyFormation;

        if (circleSpawn == null && setSpawnsRanged.Count == 0)
        {
            enemyFormation = 1;
        }
        else
        {
            enemyFormation = Random.Range(1, 4);
        }

        switch (enemyFormation)
        {
            case 1:
                break;
            case 2:
                if (circleSpawn == null)
                {
                    enemyFormation = 3;
                }
                break;
            case 3:
                if (setSpawnsRanged.Count == 0)
                {
                    enemyFormation = 2;
                }
                break;
        }

        switch (difficulty)
        {
            case 0:
                print("not set");
                break;
            case 1:
                enemyAmount = Random.Range(1, 3);
                enemyVariantChance = Random.Range(0, 2);
                break;
            case 2:
                enemyAmount = Random.Range(1, 4);
                enemyVariantChance = Random.Range(1, 3);
                break;
            case 3:
                enemyAmount = Random.Range(2, 5);
                enemyVariantChance = Random.Range(1, 5);
                break;
        }

        int enemyVariantAmount = 0;
        GameObject enemyType;

        switch (enemyFormation)
        {
            case 1: //Set Points
                for (int i = 0; i < enemyAmount; i++)
                {
                    if (enemyVariantAmount < enemyVariantChance)
                    {
                        enemyType = _lm.meleeEnemyTypes[Random.Range(1, _lm.meleeEnemyTypes.Count)];
                        enemyVariantAmount++;
                    }
                    else
                    {
                        enemyType = _lm.meleeEnemyTypes[0];
                    }

                    GameObject go = Instantiate(enemyType, setSpawns[i].position, Quaternion.identity);
                    go.transform.SetParent(enemyGrouping);
                }
                break;
            case 2: //Circle Spawn
                    for (int i = 0; i < enemyAmount; i++)
                    {
                        float angle = i * Mathf.PI * 2f / enemyAmount;
                        Vector3 newPos = new Vector3(Mathf.Cos(angle) * 4, 0, Mathf.Sin(angle) * 4) + circleSpawn.transform.position;

                        if (enemyVariantAmount < enemyVariantChance)
                        {
                            enemyType = _lm.meleeEnemyTypes[Random.Range(1, _lm.meleeEnemyTypes.Count)];
                            enemyVariantAmount++;
                        }
                        else
                        {
                            enemyType = _lm.meleeEnemyTypes[0];
                        }

                        GameObject go = Instantiate(enemyType, newPos, Quaternion.identity);
                    go.transform.SetParent(enemyGrouping);
                    }
                break;
            case 3: //RangedSpawn
                int rangedEnemiesSpawned = 0;
                
                for (int i = 0; i < setSpawnsRanged.Count; i++)
                {
                    GameObject go = Instantiate(_lm.rangedEnemyType, setSpawnsRanged[i].position, Quaternion.identity);
                    go.transform.SetParent(enemyGrouping);

                    rangedEnemiesSpawned++;
                }

                for(int i = 0; i < enemyAmount - rangedEnemiesSpawned; i++)
                {
                    if (enemyVariantAmount < enemyVariantChance)
                    {
                        enemyType = _lm.meleeEnemyTypes[Random.Range(1, _lm.meleeEnemyTypes.Count)];
                        enemyVariantAmount++;
                    }
                    else
                    {
                        enemyType = _lm.meleeEnemyTypes[0];
                    }

                    GameObject go = Instantiate(enemyType, setSpawns[i].position, Quaternion.identity);
                    go.transform.SetParent(enemyGrouping);
                }
                break;
        }
        print(tileNumber +" " + enemyAmount + " " + enemyVariantChance + " " + difficulty + " " + enemyFormation);

        enemiesSpawned = true;
    }

    public void ObjectDissapear()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceFromPlayer > _lm.distanceFromPlayerToDissapear)
        {
            grouping.SetActive(false);
        }
        else
        {
            grouping.SetActive(true);
        }
    }
}
