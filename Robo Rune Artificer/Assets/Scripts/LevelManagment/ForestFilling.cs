using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestFilling : MonoBehaviour
{
    public GameObject forestNoScript;
    
    // Start is called before the first frame update
    void Start()
    {
        bool zn = false, zp = false, xn = false, xp = false;
        bool znf = false, zpf = false, xnf = false, xpf = false;

        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Vector3.back, out hit, 60))
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
        if (Physics.Raycast(this.transform.position, Vector3.forward, out hit, 60))
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
        if (Physics.Raycast(this.transform.position, Vector3.left, out hit, 60))
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
        if (Physics.Raycast(this.transform.position, Vector3.right, out hit, 60))
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

        GameObject newForest;
        Transform grouping = GameObject.Find("Grouping").transform;
        Transform forestGroup = grouping.Find("Forest");

        if (zn)
        {
            if (!xnf && !xn)
            {
                newForest = Instantiate(forestNoScript, new Vector3(transform.position.x - 50, transform.position.y, transform.position.z), this.transform.rotation);
                newForest.transform.SetParent(forestGroup);
            }
            else if (!xpf && !xp)
            {
                newForest = Instantiate(forestNoScript, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), this.transform.rotation);
                newForest.transform.SetParent(forestGroup);
            }
        }
        else if (zp)
        {
            if (!xnf && !xn)
            {
                newForest = Instantiate(forestNoScript, new Vector3(transform.position.x - 50, transform.position.y, transform.position.z), this.transform.rotation);
                newForest.transform.SetParent(forestGroup);
            }
            else if (!xpf && !xp)
            {
                newForest = Instantiate(forestNoScript, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), this.transform.rotation);
                newForest.transform.SetParent(forestGroup);
            }
        }
        
        if (xn)
        {
            if (!znf && !zn)
            {
                newForest = Instantiate(forestNoScript, new Vector3(transform.position.x, transform.position.y, transform.position.z - 50), this.transform.rotation);
                newForest.transform.SetParent(forestGroup);
            }
            else if (!zpf && !zp)
            {
                newForest = Instantiate(forestNoScript, new Vector3(transform.position.x, transform.position.y, transform.position.z + 50), this.transform.rotation);
                newForest.transform.SetParent(forestGroup);
            }
        }
        else if (xp)
        {
            if (!znf && !zn)
            {
                newForest = Instantiate(forestNoScript, new Vector3(transform.position.x, transform.position.y, transform.position.z - 50), this.transform.rotation);
                newForest.transform.SetParent(forestGroup);
            }
            else if (!zpf && !zp)
            {
                newForest = Instantiate(forestNoScript, new Vector3(transform.position.x, transform.position.y, transform.position.z + 50), this.transform.rotation);
                newForest.transform.SetParent(forestGroup);
            }
        }
    }
}
