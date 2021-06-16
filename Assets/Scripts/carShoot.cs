using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carShoot : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletSpawn;

    public float speedShoot = 50;
    public float TimeShots = 0.33f;
    private float m_timeStamp = 0f;
    void FixedUpdate()
    {
        if ((Time.time >= m_timeStamp) && (Input.GetKey(KeyCode.Mouse0)))
        {
            Fire();
            m_timeStamp = Time.time + TimeShots;
        }
    }
    void Fire()
    {
        var bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speedShoot;

        Destroy(bullet, 2.0f);
    }
}
