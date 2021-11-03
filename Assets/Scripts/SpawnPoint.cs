using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public bool enemy1Destroyed = false;
    public bool enemy2Destroyed = false;
    public bool enemy3Destroyed = false;

    public bool enemy1Spawned = false;
    public bool enemy2Spawned = false;
    public bool enemy3Spawned = false;

    public bool message1 = false;
    public bool message2 = false;
    public bool message3 = false;

    public float countdownTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn in the first enemy
        Instantiate(enemy1, transform.position, Quaternion.identity);
        enemy1Spawned = true;

    }

    // Update is called once per frame
    void Update()
    {
        // Once the first enemy is destroyed after having spawned mark it as true
        if (GameObject.FindWithTag("Enemy 1") == null && enemy1Spawned)
        {
            enemy1Destroyed = true;
        }

        // After the first enemey is destroyed
        if (enemy1Destroyed && !message1)
        {
            Debug.Log("Number 2 arrives in " + countdownTime + " seconds!");

            Invoke("SpawnEnemy2", countdownTime);

            message1 = true;
        }

        if (enemy2Spawned && GameObject.FindWithTag("Enemy 2") == null && message1)
        {
            enemy2Destroyed = true;
        }
        

        if (enemy2Destroyed && enemy2Spawned && !message2)
        {
            Debug.Log("Can you handle number 3!?");

            Invoke("SpawnEnemy3", countdownTime);

            message2 = true;

        }
        
    }

    void SpawnEnemy2()
    {
        Instantiate(enemy2, transform.position, Quaternion.identity);
        enemy2Spawned = true;
    }

    void SpawnEnemy3()
    {
        Instantiate(enemy3, transform.position, Quaternion.identity);
        enemy3Spawned = true;
    }

}
