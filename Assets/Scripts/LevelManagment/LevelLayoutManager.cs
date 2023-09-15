using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutManager : MonoBehaviour
{
    public int maximumTiles;
    public float distanceFromPlayerToDissapear;
    private int numTillNextClear = 0;
    private int currentTileAmount;

    public GameObject shopGround;
    public List<GameObject> treasureGrounds;
    public List<GameObject> clearGrounds;
    public GameObject endGround;
    public GameObject startGround;
    public GameObject forest;

    public GameObject endWalls;
    public GameObject straight;
    public List<GameObject> straightSingle;
    public GameObject corner;
    public GameObject middle;

    private GameObject grouping, groundGrouping, forestGrouping, wallGrouping;

    private List<Transform> tilesInPreviousRow = new List<Transform>();
    private List<GameObject> totalTiles = new List<GameObject>();

    private Vector3 spawnPoint;

    public List<GameObject> meleeEnemyTypes;
    public GameObject rangedEnemyType;

    // Start is called before the first frame update
    void Start()
    {
        grouping = GameObject.Find("Grouping");
        groundGrouping = grouping.transform.Find("Ground").gameObject;
        forestGrouping = grouping.transform.Find("Forest").gameObject;
        wallGrouping = grouping.transform.Find("Walls").gameObject;

        spawnPoint = transform.position;

        FillOutGroundLists();

        for(int r = 0; currentTileAmount < maximumTiles; r++)
        {
            createTileRows(spawnPoint.x, r);

            int index = Random.Range(0, tilesInPreviousRow.Count);
            float zPos = tilesInPreviousRow[index].position.z;

            spawnPoint = new Vector3(spawnPoint.x + 50, spawnPoint.y, zPos);
        }

        GameObject endGroundTile = Instantiate(endGround, new Vector3(totalTiles[maximumTiles - 1].transform.position.x + 50, totalTiles[maximumTiles - 1].transform.position.y, totalTiles[maximumTiles - 1].transform.position.z), totalTiles[maximumTiles - 1].transform.rotation);
        endGroundTile.transform.SetParent(groundGrouping.transform);
        TileShapeConversion();
    }

    public void FillOutGroundLists()
    {
        int tgtAmount = treasureGrounds.Count;
        int tgfillAmount = maximumTiles / tgtAmount;

        for(int i = 0; i < tgtAmount; i++)
        {
            for(int e = 0; e < tgfillAmount; e++)
            {
                treasureGrounds.Add(treasureGrounds[i]);
            }
        }

        print(treasureGrounds.Count);

        int cgAmount = clearGrounds.Count;
        int cgFillAmount = maximumTiles / 4;

        for (int c = 0; c < cgAmount; c++)
        {
            for (int g = 0; g < cgFillAmount; g++)
            {
                treasureGrounds.Add(clearGrounds[c]);
            }
        }

    }

    public void createTileRows(float xPos, int r)
    {
        int numberOfTilesInRow;
        if (currentTileAmount > maximumTiles - 3)
        {
            numberOfTilesInRow = Random.Range(1, (maximumTiles - currentTileAmount) + 1);
        }
        else
        {
            numberOfTilesInRow = Random.Range(3, 5);
        }

        List<Transform> tilesInRow = new List<Transform>();
        int direction = Random.Range(1, 3);

        for (int t = 0; t < numberOfTilesInRow ; t++)
        {
            GameObject currentTile = Instantiate(RandomGround(numberOfTilesInRow, t, r), spawnPoint, transform.rotation);
            currentTile.transform.SetParent(groundGrouping.transform);

            if(direction == 1)
            {
                spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + 50);
            }
            else if (direction == 2)
            {
                spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z - 50);
            }

            tilesInRow.Add(currentTile.transform);
            totalTiles.Add(currentTile);

            currentTileAmount++;

            if(currentTile.TryGetComponent(out TileManager tileManager))
            {
                tileManager.tileNumber = currentTileAmount;
            }
        }

        tilesInPreviousRow = tilesInRow;
    }

    public GameObject RandomGround(int numberOfTilesInRow, int currentTileInRow, int currentRow)
    {
        GameObject randomGround = null;

        if (currentRow == 0 && currentTileInRow == 0)
        {
            randomGround = startGround;
        }
        else if (currentTileAmount >= maximumTiles / 2 && shopGround != null)
        {
            int tryForShop = Random.Range(1, 4);

            if (currentTileAmount == 19)
            {
                randomGround = shopGround;
                shopGround = null;
            }
            else if (tryForShop == 1)
            {
                randomGround = shopGround;
                shopGround = null;
            }
            else
            {
                int index = Random.Range(0, treasureGrounds.Count);

                randomGround = treasureGrounds[index];
                treasureGrounds.RemoveAt(index);

                numTillNextClear = numTillNextClear - 1;
            }
        }
        else if (currentTileInRow == numberOfTilesInRow - 1)
        {
            int index = Random.Range(0, treasureGrounds.Count);

            randomGround = treasureGrounds[index];
            treasureGrounds.RemoveAt(index);

            numTillNextClear = numTillNextClear - 1;
        }
        else if(clearGrounds.Count > 0 && numTillNextClear <= 0)
        {
            int index = Random.Range(0, clearGrounds.Count);

            randomGround = clearGrounds[index];
            clearGrounds.RemoveAt(index);

            numTillNextClear = Random.Range(2, 6);
        }
        else
        {
            int index = Random.Range(0, treasureGrounds.Count);

            randomGround = treasureGrounds[index];
            treasureGrounds.RemoveAt(index);

            numTillNextClear = numTillNextClear - 1;
        }

        return randomGround;
    }

    public void TileShapeConversion()
    {
        for(int i = 1; i < totalTiles.Count; i++)
        {
            bool zn = false, zp = false, xn = false, xp = false;
            bool znf = false, zpf = false, xnf = false, xpf = false;

            RaycastHit hit;

            if (Physics.Raycast(totalTiles[i].transform.position, Vector3.back, out hit, 60))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    zn = true;
                }
                else if (hit.collider.gameObject.layer == 9)
                {
                    znf = true;
                }
            }
            if (Physics.Raycast(totalTiles[i].transform.position, Vector3.forward, out hit, 60))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    zp = true;
                }
                else if (hit.collider.gameObject.layer == 9)
                {
                    zpf = true;
                }
            }
            if (Physics.Raycast(totalTiles[i].transform.position, Vector3.left, out hit, 60))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    xn = true;
                }
                else if (hit.collider.gameObject.layer == 9)
                {
                    xnf = true;
                }
            }
            if (Physics.Raycast(totalTiles[i].transform.position, Vector3.right, out hit, 60))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    xp = true;
                }
                else if (hit.collider.gameObject.layer == 9)
                {
                    xpf = true;
                }
            }

            GameObject currentForest;
            if(!zn && !znf)
            {
                currentForest = Instantiate(forest, new Vector3(totalTiles[i].transform.position.x, forest.transform.position.y, totalTiles[i].transform.position.z - 50), totalTiles[i].transform.rotation);
                currentForest.transform.SetParent(forestGrouping.transform);
            }
            if(!zp && !zpf)
            {
                currentForest = Instantiate(forest, new Vector3(totalTiles[i].transform.position.x, forest.transform.position.y, totalTiles[i].transform.position.z + 50), totalTiles[i].transform.rotation);
                currentForest.transform.SetParent(forestGrouping.transform);
            }
            if (!xn && !xnf)
            {
                currentForest = Instantiate(forest, new Vector3(totalTiles[i].transform.position.x - 50, forest.transform.position.y, totalTiles[i].transform.position.z), totalTiles[i].transform.rotation);
                currentForest.transform.SetParent(forestGrouping.transform);
            }
            if (!xp && !xpf)
            {
                currentForest = Instantiate(forest, new Vector3(totalTiles[i].transform.position.x + 50, forest.transform.position.y, totalTiles[i].transform.position.z), totalTiles[i].transform.rotation);
                currentForest.transform.SetParent(forestGrouping.transform);
            }

            if (xp && xn && zp && zn)
            {
                Instantiate(middle, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y, totalTiles[i].transform.rotation.z));
            }
            else if (xp && xn)
            {
                StraightSpawn(zn, zp, xn, xp, true, i);
            }
            else if (zp && zn)
            {
                StraightSpawn(zn, zp, xn, xp, false, i);
            }
            else if (xp && zn)
            {
                CornerSpawn(1, i);
            }
            else if (xp && zp)
            {
                CornerSpawn(2, i);
            }
            else if (xn && zn)
            {
                CornerSpawn(3, i);
            }
            else if (xn && zp)
            {
                CornerSpawn(4, i);
            }
            else
            {
                EndSpawn(zn, zp, xn, xp, i);
            }
        }
    }

    public void StraightSpawn(bool zn, bool zp, bool xn, bool xp, bool x, int i)
    {
        GameObject currentWall;

        GameObject randomStraight = straightSingle[Random.Range(0, straightSingle.Count)];

        if (x)
        {
            if (zp)
            {
                 currentWall = Instantiate(randomStraight, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y, totalTiles[i].transform.rotation.z));
                currentWall.transform.SetParent(wallGrouping.transform);
            }
            else if (zn)
            {
                currentWall = Instantiate(randomStraight, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y + 180, totalTiles[i].transform.rotation.z));
                currentWall.transform.SetParent(wallGrouping.transform);
            }
            else
            {
                currentWall = Instantiate(straight, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y, totalTiles[i].transform.rotation.z));
                currentWall.transform.SetParent(wallGrouping.transform);
            }
        }
        else
        {
            if (xp)
            {
                currentWall = Instantiate(randomStraight, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y + 90, totalTiles[i].transform.rotation.z));
                currentWall.transform.SetParent(wallGrouping.transform);
            }
            else if (xn)
            {
                currentWall = Instantiate(randomStraight, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y - 90, totalTiles[i].transform.rotation.z));
                currentWall.transform.SetParent(wallGrouping.transform);
            }
            else
            {
                currentWall = Instantiate(straight, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y + 90, totalTiles[i].transform.rotation.z));
                currentWall.transform.SetParent(wallGrouping.transform);
            }
        }
    }

    public void CornerSpawn(int num, int i)
    {
        GameObject currentWall;

        if (num == 1)
        {
            currentWall = Instantiate(corner, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y + 90, totalTiles[i].transform.rotation.z));
            currentWall.transform.SetParent(wallGrouping.transform);
        }
        else if (num == 2)
        {
            currentWall = Instantiate(corner, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y, totalTiles[i].transform.rotation.z));
            currentWall.transform.SetParent(wallGrouping.transform);
        }
        else if (num == 3)
        {
            currentWall = Instantiate(corner, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y + 180, totalTiles[i].transform.rotation.z));
            currentWall.transform.SetParent(wallGrouping.transform);
        }
        else if (num == 4)
        {
            currentWall = Instantiate(corner, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y - 90, totalTiles[i].transform.rotation.z));
            currentWall.transform.SetParent(wallGrouping.transform);
        }
    }

    public void EndSpawn(bool zn, bool zp, bool xn, bool xp, int i)
    {
        GameObject currentWall;

        if (xp)
        {
            currentWall = Instantiate(endWalls, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y, totalTiles[i].transform.rotation.z));
            currentWall.transform.SetParent(wallGrouping.transform);
        }
        else if (xn)
        {
            currentWall = Instantiate(endWalls, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y + 180, totalTiles[i].transform.rotation.z));
            currentWall.transform.SetParent(wallGrouping.transform);
        }
        else if (zp)
        {
            currentWall = Instantiate(endWalls, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y - 90, totalTiles[i].transform.rotation.z));
            currentWall.transform.SetParent(wallGrouping.transform);
        }
        else if (zn)
        {
            currentWall = Instantiate(endWalls, totalTiles[i].transform.position, Quaternion.Euler(totalTiles[i].transform.rotation.x, totalTiles[i].transform.rotation.y + 90, totalTiles[i].transform.rotation.z));
            currentWall.transform.SetParent(wallGrouping.transform);
        }
    }
}