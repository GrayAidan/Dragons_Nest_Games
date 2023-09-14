using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestDissapear : MonoBehaviour
{
    private LevelLayoutManager _lm;
    private GameObject player;
    public GameObject grouping;
    
    // Start is called before the first frame update
    void Start()
    {
        _lm = GameObject.Find("GameManager").GetComponent<LevelLayoutManager>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
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
