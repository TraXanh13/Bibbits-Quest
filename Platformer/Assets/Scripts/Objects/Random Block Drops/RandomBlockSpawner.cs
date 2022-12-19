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
        if(Input.GetKeyDown(KeyCode.Q)) {
            SpawnRandomBlock();
        }
    }

    public void SpawnRandomBlock() {
        int randBlockIndex = Random.Range(0, randBlocks.Length);
        GameObject randBlock = randBlocks[randBlockIndex];
        Vector2 randomSpawnLocation = new Vector2(Random.Range(-45, -28), 5);
        Instantiate(randBlock, randomSpawnLocation, Quaternion.identity);
    }
}
