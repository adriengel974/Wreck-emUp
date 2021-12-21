using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float launchVelocity = 100f;

    private Stopwatch lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = new Stopwatch();
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

        if(other.tag != "Player")
            Destroy(projectile);
    }

    // Move the bullet
    void MoveBullet()
    {
        lifeTime.Start();
        if(gameObject.tag == "front")
            transform.Translate(Vector3.forward * launchVelocity * Time.deltaTime);

        if(gameObject.tag == "left")
            transform.Translate(Vector3.left * launchVelocity * Time.deltaTime);

        if(gameObject.tag == "right")
            transform.Translate(Vector3.right * launchVelocity * Time.deltaTime);
    }

    void LifeTime()
    {
        if (lifeTime.ElapsedMilliseconds > 5000f)
        {
            Destroy(projectile);
            lifeTime.Stop();
        }
    }
}