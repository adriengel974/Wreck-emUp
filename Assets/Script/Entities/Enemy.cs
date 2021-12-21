using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectileFront;
    [SerializeField] private GameObject projectileLeft;
    [SerializeField] private GameObject projectileRight;

    [SerializeField] private GameObject frontLaunchingOrigin;
    [SerializeField] private GameObject leftLaunchingOrigin;
    [SerializeField] private GameObject rightLaunchingOrigin;

    private Stopwatch FireRate;
    private Stopwatch lifeTime;

    private float velocity = -20f;
    private float fireRate = 2000f;

    public int health = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Enemy")
            health = 1;

        if (gameObject.tag == "Shooting Enemy")
            health = 2;

        if (gameObject.tag == "LittleEnemy")
            health = 0;

        if (gameObject.tag == "SceneDecoration")
            health = 10;

        FireRate = new Stopwatch();
        FireRate.Start();

        lifeTime = new Stopwatch();
        lifeTime.Start();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        LifeTime();
    }

    // Move the bullet
    private void MoveEnemy()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        SpecialEnemy();
    }

    void SpecialEnemy()
    {
        if(gameObject.tag == "Shooting Enemy")
        {
            
            if (FireRate.ElapsedMilliseconds > fireRate)
            {
                // Fire Front
                Instantiate(
                    projectileFront,
                    frontLaunchingOrigin.transform.position,
                    Quaternion.identity);

                // Fire Left
                Instantiate(
                    projectileLeft,
                    leftLaunchingOrigin.transform.position,
                    Quaternion.identity);

                // Fire Right
                Instantiate(
                    projectileRight,
                    rightLaunchingOrigin.transform.position,
                    Quaternion.identity);

                FireRate.Restart();
            }
        }
    }

    private void LifeTime()
    {
        if (lifeTime.ElapsedMilliseconds > 15000f)
        {
            Destroy(gameObject);
            lifeTime.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (gameObject.tag == "SceneDecoration" &&
            other.tag == "rightEnemy" ||
            other.tag == "leftEnemy" ||
            other.tag == "frontEnemy" ||
            other.tag == "right" ||
            other.tag == "left" ||
            other.tag == "front")
        {
            Destroy(other);
        }

        if (other.tag == "right" ||
            other.tag == "left" ||
            other.tag == "front" /*&&
            gameObject.tag != "SceneDecoration"*/)
        {

            if (health == 0)
            {
                if(gameObject.tag == "LittleEnemy")
                    GameManager.instance.AddScore(5);

                if (gameObject.tag == "Enemy")
                    GameManager.instance.AddScore(10);

                if (gameObject.tag == "Shooting Enemy")
                    GameManager.instance.AddScore(20);

                Destroy(gameObject);
                GameManager.instance.EnemyKilled();
            }
                

            Destroy(other);
            health -= 1;
        }
    }

}
