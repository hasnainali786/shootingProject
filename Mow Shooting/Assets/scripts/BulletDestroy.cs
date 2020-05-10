using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Destroy", 0.5f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        CancelInvoke();
    }
}
