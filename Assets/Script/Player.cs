using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private GameObject projectileFront;
    [SerializeField] private GameObject projectileLeft;
    [SerializeField] private GameObject projectileRight;

    [SerializeField] private GameObject frontLaunchingOrigin;
    [SerializeField] private GameObject leftLaunchingOrigin;
    [SerializeField] private GameObject rightLaunchingOrigin;

    [SerializeField] private float m_VerticalSpeed = 6f;
    [SerializeField] private float m_HorizotalSpeed = 6f;

    private float fireRate = 700f;
    private int fireMode = 1;

    private Vector3 screenPos;
    private Stopwatch stopWatch;

    // Start is called before the first frame updates
    void Start()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();
    }


    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    public void PlaySoundFront()
    {
        frontLaunchingOrigin.GetComponentInChildren<AudioSource>().Play();
    }

    public void PlaySoundLeft()
    {
        leftLaunchingOrigin.GetComponentInChildren<AudioSource>().Play();
    }

    public void PlaySoundRight()
    {
        rightLaunchingOrigin.GetComponentInChildren<AudioSource>().Play();
    }


    void PlayerControl()
    {
        screenPos = m_MainCamera.WorldToScreenPoint(transform.position);

        // UpArrow 
        if (Input.GetKey(KeyCode.UpArrow) && screenPos.y <= Screen.height + 20)
            transform.Translate(-Vector3.forward * m_VerticalSpeed * Time.deltaTime);

        // DownArrow 
        if (Input.GetKey(KeyCode.DownArrow) && screenPos.y > 400)
            transform.Translate(-Vector3.back * m_VerticalSpeed * Time.deltaTime);

        // LeftArrow
        if (Input.GetKey(KeyCode.LeftArrow) && screenPos.x > 150)
            transform.Translate(-Vector3.left * m_HorizotalSpeed * Time.deltaTime);

        // RightArrow
        if (Input.GetKey(KeyCode.RightArrow) && screenPos.x <= Screen.width + 20)
            transform.Translate(-Vector3.right * m_HorizotalSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fireMode == 1)
            {
                fireMode = 2;
            }
            else
            {
                fireMode = 1;
            }
        }

        if (fireMode == 1)
            FireBulletMode1();

        if (fireMode == 2)
            FireBulletMode2();
    }

    void FireBulletMode1()
    {
        

        // Fire Front
        if (Input.GetKey(KeyCode.Z) && stopWatch.ElapsedMilliseconds > fireRate)
        {
            Instantiate(
                projectileFront,
                frontLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundFront();

            stopWatch.Restart();
        }

        // Fire Left
        if (Input.GetKey(KeyCode.Q) && stopWatch.ElapsedMilliseconds > fireRate)
        {
            Instantiate(
                projectileLeft,
                leftLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundLeft();

            stopWatch.Restart();
        }

        // Fire Right
        if (Input.GetKey(KeyCode.D) && stopWatch.ElapsedMilliseconds > fireRate)
        {
            Instantiate(
                projectileRight,
                rightLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundRight();

            stopWatch.Restart();
        }
    }

    void FireBulletMode2()
    {
        if (Input.GetKey(KeyCode.Z) && stopWatch.ElapsedMilliseconds > fireRate)
        {
            Instantiate(
                projectileFront,
                frontLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundFront();

            Instantiate(
               projectileLeft,
               leftLaunchingOrigin.transform.position,
               Quaternion.identity);
            PlaySoundLeft();

            Instantiate(
                projectileRight,
                rightLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundRight();

            stopWatch.Restart();
        }

        if (Input.GetKey(KeyCode.Q) && stopWatch.ElapsedMilliseconds > fireRate)
        {
            Instantiate(
                projectileFront,
                frontLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundFront();

            Instantiate(
               projectileLeft,
               leftLaunchingOrigin.transform.position,
               Quaternion.identity);
            PlaySoundLeft();

            Instantiate(
                projectileRight,
                rightLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundRight();

            stopWatch.Restart();
        }

        if (Input.GetKey(KeyCode.D) && stopWatch.ElapsedMilliseconds > fireRate)
        {
            Instantiate(
                projectileFront,
                frontLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundFront();

            Instantiate(
               projectileLeft,
               leftLaunchingOrigin.transform.position,
               Quaternion.identity);
            PlaySoundLeft();

            Instantiate(
                projectileRight,
                rightLaunchingOrigin.transform.position,
                Quaternion.identity);
            PlaySoundRight();

            stopWatch.Restart();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Boss")
        {
            GameManager.instance.TakeDamage(5);
        }

        if (other.tag == "Shooting Enemy")
        {
            Destroy(other);
            GameManager.instance.TakeDamage(5);
        }

        if (other.tag == "Enemy")
        {
            Destroy(other);
            GameManager.instance.TakeDamage(3);
        }

        if (other.tag == "LittleEnemy")
        {
            Destroy(other);
            GameManager.instance.TakeDamage(1);
        }

        if (other.tag == "SceneDecoration")
        {
            Destroy(other);
            GameManager.instance.TakeDamage(2);
        }
    }

}
