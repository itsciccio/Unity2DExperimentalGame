using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public bool disableGun = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o") && disableGun == false)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
