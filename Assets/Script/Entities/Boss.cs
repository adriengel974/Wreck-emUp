using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject projectileFront;
    [SerializeField] private GameObject projectileLeft;
    [SerializeField] private GameObject projectileRight;

    [SerializeField] private GameObject frontLaunchingOrigin;
    [SerializeField] private GameObject leftLaunchingOrigin;
    [SerializeField] private GameObject rightLaunchingOrigin;

    private Stopwatch FireRate;
    private Stopwatch MovingRate;
    private Stopwatch MoveForward;

    private float moveForward = 4000f;
    private float fireRate = 1500f;
    private float movingRate = 6500f;

    //private bool changeDirection = true;
    private float velocity = -23f;

    public int health = 9;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.BossSpawed();

        FireRate = new Stopwatch();
        MovingRate = new Stopwatch();
        MoveForward = new Stopwatch();

        FireRate.Start();
        MovingRate.Start();
        MoveForward.Start();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    // -------------------- Move the enemy --------------------
    private void MoveEnemy()
    {
        if(MoveForward.ElapsedMilliseconds < moveForward)
        {
            transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        } else
        {
            MoveForward.Stop();
        }

        if (MovingRate.ElapsedMilliseconds < movingRate / 2)
        {
            transform.Translate(Vector3.left * velocity * Time.deltaTime);

        }
        else if (MovingRate.ElapsedMilliseconds > movingRate / 2 && MovingRate.ElapsedMilliseconds < movingRate)
        {
            transform.Translate(Vector3.right * velocity * Time.deltaTime);
        }
        else if (MovingRate.ElapsedMilliseconds > movingRate)
        {
            MovingRate.Restart();
        }

        SpecialEnemy();
    }

    void SpecialEnemy()
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



    // -------------------- OnDestroy && OnCollision part --------------------

    public void OnDestroy()
    {
        if (gameObject.tag == "Boss")
        {
            GameManager.instance.AddScore(100);
            GameManager.instance.BossKilled();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Enemy" ||
            other.tag == "Shooting Enemy" ||
            other.tag == "LittleEnemy" ||
            other.tag == "SceneDecoration")
        {
            Destroy(other);
        }


        if (other.tag == "right" ||
            other.tag == "left" ||
            other.tag == "front")
        {

            if (health == 0)
            {
                GameManager.instance.AddScore(100);
                GameManager.instance.BossKilled();
                Destroy(gameObject);
                GameManager.instance.BossUnSpawn();
            }

            Destroy(other);
            health -= 1;
        }
    }
}