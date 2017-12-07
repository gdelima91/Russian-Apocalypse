using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyToSpawn;
    public float timeBetweenSpawns;
    public GameObject player;
    float timer;

	// Use this for initialization
	void Start () {
       // SpawnEnemy();
        GetComponent<MeshRenderer>().enabled = false;
        timer = timeBetweenSpawns;
	}

    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            GameObject enemy1 = Instantiate(enemyToSpawn, transform.position, transform.rotation) as GameObject;
           
            timer = timeBetweenSpawns;
        }
    }

    //void SpawnEnemy () {
    //    GameObject enemy1 = Instantiate(enemyToSpawn, transform.position, transform.rotation) as GameObject;
    //    //enemy1.GetComponent<Enemy1_StateMachine>().currentTFTarget = player.transform;
    //    StartCoroutine("SpawnTimer");
    //}

    //IEnumerator SpawnTimer () {
    //    yield return new WaitForSeconds(timeBetweenSpawns);
    //    SpawnEnemy();
    //}
}
