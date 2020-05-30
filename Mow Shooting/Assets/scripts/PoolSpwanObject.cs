using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpwanObject : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject player;
    public int poolAmount = 20;
    List<GameObject> enemies;
    public int counterOfEnemies;
    public GameObject[] spawnpoints;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            int type = Random.Range(0, enemy.Length);
            GameObject obj = (GameObject)Instantiate(enemy[type]);
            obj.SetActive(false);
            enemies.Add(obj);
        }
        enemySpawn();
    }
    void enemySpawn()
    {
        StartCoroutine(waitToGenerateEnemy());
    }
    IEnumerator waitToGenerateEnemy()
    {
        
        for (int i = 0; i < poolAmount; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                if (counterOfEnemies < poolAmount)
                {
                    int ran = Random.Range(0, 4);
                    float dist = Vector3.Distance(player.transform.position, spawnpoints[ran].transform.position);
                    print(dist);
                    Vector3 newpos = spawnpoints[ran].transform.position;
                    enemies[i].transform.position = newpos;
                    enemies[i].transform.rotation = Quaternion.identity;
                    enemies[i].SetActive(true);
                    counterOfEnemies++;
                    yield return new WaitForSeconds(Random.Range(1, 2));
                }
            }
        }
       
        yield return null;

    }

}
