using UnityEngine;
using System.Collections;
using System.Diagnostics;
public class DecorationEntities : MonoBehaviour
{

    private float velocity = -20f;
    private Stopwatch stopWatch;

    // Use this for initialization
    void Start()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);

        if(gameObject.tag == "Sand")
            LifeTime();
    }


    void LifeTime()
    {
        if (stopWatch.ElapsedMilliseconds > 40000f)
        {
            Destroy(gameObject);
            stopWatch.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Destroy")
        {
            Destroy(gameObject);
            stopWatch.Stop();
        }

        if (gameObject.tag == "SceneDecoration" &&
            other.tag == "right" ||
            other.tag == "left" ||
            other.tag == "front")
        {
            Destroy(other);
        }
    }
}
