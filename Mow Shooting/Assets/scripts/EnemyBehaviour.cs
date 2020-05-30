using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    public int deathcount;
    bool hitBool = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            if (!hitBool)
            {
                agent.destination = player.transform.position;
                GetComponent<Animation>().Play("run");
                agent.isStopped = false;
            }
        }
        if(deathcount<=0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Bullet")
        {
            StartCoroutine(hit());
            print(other.gameObject);
        }
    }
    IEnumerator hit()
    {
        deathcount -= 1;
        GetComponent<Animation>().Play("hit");
        agent.isStopped = true;
        hitBool = true;
        yield return new WaitForSeconds(GetComponent<Animation>()["hit"].length);
        hitBool = false;
    }

    IEnumerator death()
    {
        GetComponent<Animation>().Play("death");
        hitBool = true;
        yield return new WaitForSeconds(GetComponent<Animation>()["death"].length);
    }
}
