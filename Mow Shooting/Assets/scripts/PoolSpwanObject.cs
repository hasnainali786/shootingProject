using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpwanObject : MonoBehaviour
{
    public GameObject enemy;
    public int poolAmount = 20;
    List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {

        enemies = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemy);
            obj.SetActive(false);
            enemies.Add(obj);
        }
        InvokeRepeating("enemySpawn", 0.1f, 0.1f);
    }
    void enemySpawn()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                enemies[i].transform.position = Camera.main.ViewportToWorldPoint(new Vector3(2.1f, 0.2f, 10.0f));
                enemies[i].SetActive(true);
                return;
            }
        }
    
    }
   
}
