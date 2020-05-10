using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpwanObject : MonoBehaviour
{
    public GameObject enemy;
    public int poolAmount = 20;
    List<GameObject> enemies;
    Vector3 center;
    public int numberOfEnemiesInLevel;
    public int counterOfEnemies;
    public GameObject playerCenter;
    // Start is called before the first frame update
    void Start()
    {
        center = playerCenter.transform.position;
        enemies = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemy);
            obj.SetActive(false);
            enemies.Add(obj);
        }
        // InvokeRepeating("enemySpawn", 0.1f, 0.1f);
        enemySpawn();
    }
    void enemySpawn()
    {

        StartCoroutine(waitToGenerateEnemy());


    }
    IEnumerator waitToGenerateEnemy()
    {
        
        for (int i = 0; i < numberOfEnemiesInLevel; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                if (counterOfEnemies < numberOfEnemiesInLevel)
                {
                    int a = i * 30;
                    center = playerCenter.transform.position;
                    Vector3 newpos = RandomCircle(center, 30f, a);
                    enemies[i].transform.position = newpos;
                    enemies[i].transform.rotation = Quaternion.identity;
                    //Camera.main.ViewportToWorldPoint(new Vector3(2.1f, 0.2f, 10.0f));
                    enemies[i].SetActive(true);

                    // GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), newPos, Quaternion.identity);

                    counterOfEnemies++;
                    yield return new WaitForSeconds(Random.Range(2, 5));
                }
            }
        }
       
        yield return null;

    }
    Vector3 RandomCircle(Vector3 center, float radius, float a)
    {
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = 1 ;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

}
