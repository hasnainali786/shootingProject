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


    void FixedUpdate()
    {
        aim();
    }
    void aim()
    {
        Transform nearest = null;
        Collider[] cols = Physics.OverlapSphere(transform.position, 5);
        foreach (Collider hitt in cols)
        {
            if (hitt.tag == "Enemy")
            {
                float dist = Vector3.Distance(transform.position, hitt.transform.position);
                if (dist <= 5)
                { 
                    nearest = hitt.transform;
                    isInRange = true;
                    Debug.Log(" in range");
                    Vector3 playerToMouse = nearest.transform.position - transform.position;
                    playerToMouse.y = 0f;
                    playerToMouse.Normalize();
                    float targetAngle = Mathf.Atan2(playerToMouse.x, playerToMouse.z) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }
                else
                {
                    isInRange=false;
                }
            }
        }

    }
}
