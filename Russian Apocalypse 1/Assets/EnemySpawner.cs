using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyToSpawn;
    public float timeBetweenSpawns;
    public GameObject player;

	// Use this for initialization
	void Start () {
        SpawnEnemy();
        GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnEnemy () {
        GameObject enemy1 = Instantiate(enemyToSpawn, transform.position, transform.rotation) as GameObject;
        //enemy1.GetComponent<Enemy1_StateMachine>().currentTFTarget = player.transform;
        StartCoroutine("SpawnTimer");
    }

    IEnumerator SpawnTimer () {
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnEnemy();
    }
}
