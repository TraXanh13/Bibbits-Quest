using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlockSpawner : MonoBehaviour
{  
    private float levelStartTime;
    public GameObject[] randBlocks;

    void Start() {
        levelStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
