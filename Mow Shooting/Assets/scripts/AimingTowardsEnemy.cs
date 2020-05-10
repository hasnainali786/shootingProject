using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingTowardsEnemy : MonoBehaviour
{
    public static AimingTowardsEnemy instance;
    public bool isInRange = false;
    Rigidbody playerRigidbody;

    void Awake()
    {
        
        instance = this;
        playerRigidbody = GetComponent<Rigidbody>();
       
        
    }
    // Update is called once per frame
    void Update()
    {
        aim();
    }
    void aim()
    {
        
        float minDist = Mathf.Infinity;

        Transform nearest = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, 8);
       // Physics.SyncTransforms();
        foreach (Collider hitt in cols)
        {
            if (hitt.tag == "Enemy")
            {
                float dist = Vector3.Distance(transform.position, hitt.transform.position);
                
                if (dist <= 7)
                {
                    Debug.Log(" in range");
                    minDist = dist;
                    nearest = hitt.transform;
                    isInRange = true;
                    Vector3 playerToMouse = nearest.transform.position - transform.position;
                    playerToMouse.y = 0f;
                    playerToMouse.Normalize();
                    Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.15f);
                    //playerRigidbody.MoveRotation(newRotation);
                }
                else
                {
                    isInRange=false;
       

                }
            }
            else
            {
                print("not detecting");
                //return;
            }
        }

    }
}
