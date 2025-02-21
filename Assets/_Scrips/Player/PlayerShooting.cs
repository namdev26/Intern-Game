using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shootPoint;
    //void Update()
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        this.Shoot();
    //    }
    //}

    public void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
    }
}
