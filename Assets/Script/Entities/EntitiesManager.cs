using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyObjects;
    [SerializeField] private GameObject[] entitiesObjects;
    [SerializeField] private GameObject[] entitiesBoss;
    [SerializeField] private GameObject sandObject;

    private int enemySpawnAmount = 2;
    private int lastEnemy = 0;

    private IEnumerator EnemySpawner;

    public List<Vector3> enemyPositions;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner = SpawnEnemies();
        StartCoroutine(SpawnEntitiesLeft());
        StartCoroutine(SpawnEntitiesRight());
        StartCoroutine(EnemySpawner);
    }

    // Update is called once per frame
    void Update()
    {
        CheckWave();
    }

    private void CheckWave()
    {
        if(GameManager.instance.GetEnemyKilled() - lastEnemy == 10)
        {
            SpawnBoss();
            lastEnemy = GameManager.instance.GetEnemyKilled();
        }
    }


    // -------------------- POSITION GENERATOR --------------------

    private void EnemyPositionGenerator(int nbPosition)
    {
        enemyPositions.Clear();

        enemyPositions.Add(new Vector3(0f, 0f, 0f));

        while (enemyPositions.Count < nbPosition)
        {
            if (CheckPosition())
                enemyPositions.Add(new Vector3(Random.Range(-100, 100), 0f, 0f));
        }
    }

    private bool CheckPosition()
    {
        float delta = 30f;

        for (int i = 0; i < enemyPositions.Count; i++)
        {
            for (int j = 0; j < enemyPositions.Count; j++)
            {
                if (i != j)
                {
                    if (enemyPositions[i].x - enemyPositions[j].x < delta)
                    {
                        enemyPositions.Remove(enemyPositions[i]);
                        return false;
                    }

                }
            }
        }
        return true;
    }

    private void StartWave()
    {
        EnemyPositionGenerator(enemySpawnAmount);

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy(i);
        }
    }

    private void StartSpecialWave()
    {
        SpawnBossMinions();
    }

    private void SpawnEnemy(int i)
    {
        GameObject enemyObject = Instantiate(enemyObjects[Random.Range(0, enemyObjects.Length)],
                    transform.position + enemyPositions[i],
                    Quaternion.identity);
    }

    private void SpawnBossMinions()
    {
        Instantiate(enemyObjects[Random.Range(0, enemyObjects.Length)],
                    transform.position + new Vector3(95f,0f,0f),
                    Quaternion.identity);

        Instantiate(enemyObjects[Random.Range(0, enemyObjects.Length)],
                    transform.position + new Vector3(-95f, 0f, 0f),
                    Quaternion.identity);
    }

    private void SpawnBoss()
    {
        GameObject bossObject = Instantiate(entitiesBoss[Random.Range(0, entitiesBoss.Length)],
            transform.position + new Vector3(-40f, 0f, 20f),
            Quaternion.identity);
    }

    IEnumerator SpawnEnemies()
    {
        while (Application.isPlaying)
        {
            if(GameManager.instance.boss)
            {
                StartSpecialWave();
            } else
            {
                StartWave();
            }
            yield return new WaitForSeconds(Random.Range(4f, 6f));
        }
    }

    IEnumerator SpawnEntitiesLeft()
    {
        while (Application.isPlaying)
        {
            GameObject leftEntity = Instantiate(entitiesObjects[Random.Range(0, entitiesObjects.Length)],
            transform.position + new Vector3(-185, -5f, 100f),
            Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(4f, 10f));
        }
    }

    IEnumerator SpawnEntitiesRight()
    {
        while (Application.isPlaying)
        {
            GameObject rightEntity = Instantiate(entitiesObjects[Random.Range(0, entitiesObjects.Length)],
            transform.position + new Vector3(185, -5f, 100f),
            Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(4f, 10f));
        }
    }


    //Never used but could be useful in other version
    IEnumerator BackGroundInstatiation()
    {
        while (Application.isPlaying)
        {

            GameObject sand = Instantiate(sandObject,
                transform.position + new Vector3(0, -50, 0),
                transform.rotation);
            sand.transform.localScale = Vector3.one * 70;

            yield return new WaitForSeconds(20f);
        }
    }
}