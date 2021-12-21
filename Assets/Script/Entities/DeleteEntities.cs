using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEntities : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Enemy" ||
            other.tag == "LittleEnemy" ||
            other.tag == "Shooting Enemy" ||
            other.tag == "Boss" ||
            other.tag == "SceneDecoration")
        {
            Destroy(other);
        }
    }
}
