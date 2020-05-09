using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float fireTime = 0.05f;
    public GameObject bullet;
    public Transform gunBarrel;
    public int poolAmount = 20;
    public ParticleSystem muzzleflash;
    public int spreadAmount = 10;
    List<GameObject> bullets;
    

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }
        //InvokeRepeating("Fire", fireTime, fireTime);
    }
    public void Update()
	{
        if (AimingTowardsEnemy.instance.isInRange)
        {
            Invoke("Fire", fireTime);
        }
        else
        {
            CancelInvoke();
        }
    }

    void Fire()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                Vector3 rotation = gunBarrel.rotation.eulerAngles;
                bullets[i].transform.position = gunBarrel.position;
                bullets[i].transform.rotation = Quaternion.Euler(rotation);
                muzzleflash.Play();
                bullets[i].SetActive(true);
                return;
            }
        }
    }


    void SpawnBulletSpread()
    {
        
        for (int i = 0; i < bullets.Count; i++)
        {
            int max = spreadAmount / 2;
            int min = -max;
            if (!bullets[i].activeInHierarchy)
            {
                Vector3 rotation = gunBarrel.rotation.eulerAngles;
                Vector3 tempRot = rotation;
                for (int x = min; x < max; x++)
                {
                    tempRot.x = (rotation.x + 3 * x) % 360;

                    for (int y = min; y < max; y++)
                    {
                        tempRot.y = (rotation.y + 3 * y) % 360;
                        bullets[i].transform.position = gunBarrel.position;
                        bullets[i].transform.rotation = Quaternion.Euler(tempRot);
                        muzzleflash.Play();
                        bullets[i].SetActive(true);
                    }
                }
            }
        }
    }

}
