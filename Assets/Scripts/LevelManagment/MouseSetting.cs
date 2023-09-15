using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
