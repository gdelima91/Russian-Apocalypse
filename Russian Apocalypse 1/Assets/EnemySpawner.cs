using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V;

public class EnemySpawner : MonoBehaviour {


    public GameObject enemyToSpawn;
    public float timeBetweenSpawns;
    public GameObject player;
    public PlayerBarkManager playerBM;
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
            if (enemy1.GetComponent<Player_Bark_Call>() != null) {
                enemy1.GetComponent<Player_Bark_Call>().pBM = playerBM;
            }

            //enemy1.GetComponent<Enemy1_StateMachine>().gun = transform.Find("m4").gameObject as V.Gun;
                //gameObject.transform.FindChild("m4").gameObject;
           
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
