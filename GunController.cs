using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;
    public PlayerController playerController;

    public GameObject[] bulletPool = new GameObject[20];

    int newPoolIndex = 0;

    private void Start()
    {
        playerController.OnFireButtonPress += Shoot;
    }


    void Shoot()
    {

        if(bulletPool[newPoolIndex] == null)
        {
            bulletPool[newPoolIndex] = Instantiate(bulletPrefab);

        }
        bulletPool[newPoolIndex].transform.position = firePoint.position;
        bulletPool[newPoolIndex].transform.rotation = firePoint.rotation;
        Rigidbody rb = bulletPool[newPoolIndex].GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce((firePoint.position-transform.position)*3000);
        newPoolIndex++;
        if (newPoolIndex > 19)
        {
            newPoolIndex = 0;
        }


    }

}
