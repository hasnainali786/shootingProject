using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float fireTime = 0.05f;
    public GameObject bullet;
    public Transform gunBarrel;
    public AudioSource bulletSound;
    public int poolAmount = 20;
    public ParticleSystem muzzleflash;
    public int spreadAmount = 10;
    List<GameObject> bullets;

    public float timeBetweenBullets = 0.15f;        // The time between each shot.
                       // The distance the gun can fire.

    float timer;


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
    public void FixedUpdate()
	{
        timer += Time.deltaTime;
        if (AimingTowardsEnemy.instance.isInRange && timer >= timeBetweenBullets)
        {
            Fire();
            //SpawnBulletSpread();
        }
        else
        {
            CancelInvoke();
        }
    }

    void Fire()
    {
        timer = 0f;
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                Vector3 rotation = gunBarrel.rotation.eulerAngles;
                bullets[i].transform.position = gunBarrel.position;
                bullets[i].transform.rotation = Quaternion.Euler(rotation);
                muzzleflash.Play();
                bullets[i].SetActive(true);
                if (bulletSound)
                {
                    bulletSound.Play();
                }
                //return;
            }
        }
    }


    void SpawnBulletSpread()
    {
        int max = spreadAmount / 2;
        int min = 0;
        Vector3 rotation = gunBarrel.rotation.eulerAngles;
        Vector3 tempRot = rotation;
        for (int x = min; x < max; x++)
        {
            tempRot.x = (rotation.x + 3 * x) % 360;

            for (int y = min; y < max; y++)
            {
                if (!bullets[y].activeInHierarchy)
                {
                    tempRot.y = (rotation.y + 3 * y) % 360;
                    bullets[y].transform.position = gunBarrel.position;
                    bullets[y].transform.rotation = Quaternion.Euler(tempRot);
                    bullets[y].SetActive(true);
                    if (bulletSound)
                    {
                        bulletSound.Play();
                    }
                }
            }
        }
    }

}
