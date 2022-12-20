using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlockSpawner : MonoBehaviour
{  
    private float levelStartTime;
    public GameObject[] randBlocks;

    void Start() {
        levelStartTime = Time.time;
        SpawnRandomBlock(3);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            SpawnRandomBlock(1);
        }
        if(((Time.time - levelStartTime) % 1) <= 0.001){ 
            print("hello");
            SpawnRandomBlock(2);
        }
    }

    public void SpawnRandomBlock(int times) {
        for (int i = 0; i < times; i++) {
            int randBlockIndex = Random.Range(0, randBlocks.Length);
            GameObject randBlock = randBlocks[randBlockIndex];
            Vector2 randomSpawnLocation = new Vector2(Random.Range(-45, -28), 5);
            Instantiate(randBlock, randomSpawnLocation, Quaternion.identity);
        }
    }
}
