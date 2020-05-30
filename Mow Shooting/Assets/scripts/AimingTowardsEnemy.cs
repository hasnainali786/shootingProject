using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingTowardsEnemy : MonoBehaviour
{
    public static AimingTowardsEnemy instance;
    public bool isInRange = false;
    float turnSmoothVelocity;

    void Awake()
    {
        
        instance = this; 
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
                    float targetAngle = Mathf.Atan2(playerToMouse.x, playerToMouse.z) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    //Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                    //transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.15f);
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
