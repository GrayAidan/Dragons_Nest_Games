using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavBaker : MonoBehaviour
{
    private NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {

            surface.BuildNavMesh();
            Destroy(this);

    }
}
