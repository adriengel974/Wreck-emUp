using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float launchVelocity = 100f;

    private Stopwatch stopWatch;

    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
        LifeTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Player")
        {
            GameManager.instance.TakeDamage(2);
            Destroy(projectile);
        }

        if (other.tag == "Destroy")
        {
            Destroy(projectile);
        }
    }

    // Move the bullet
    void MoveBullet()
    {
        stopWatch.Start();
        if (projectile.tag == "frontEnemy")
            transform.Translate(-Vector3.forward * launchVelocity * Time.deltaTime);

        if (projectile.tag == "leftEnemy")
            transform.Translate(-Vector3.left * launchVelocity * Time.deltaTime);

        if (projectile.tag == "rightEnemy")
            transform.Translate(-Vector3.right * launchVelocity * Time.deltaTime);
    }

    void LifeTime()
    {
        if (stopWatch.ElapsedMilliseconds > 5000f)
        {
            Destroy(gameObject);
            stopWatch.Stop();
        }
    }
}