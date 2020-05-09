using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=5;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
