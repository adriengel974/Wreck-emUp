using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bestScoreText;

    [SerializeField] GameObject gameOverObject;

    private int maxHealth = 30;
    private int health = 30;
    private int score = 0;
    //private int highScore;

    private int enemyKilled = 0;
    private int bossKilled = 0;
    public bool boss = false;

    // Use this for initialization
    void Awake()
    {
        //getScores();

        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
    }



    // Update is called once per frame
    void Update()
    {
        TakeDamage(0);
        AddScore(0);
        //saveScores();
    }


    public void TakeDamage(int damageValue)
    {
        health -= damageValue;

        if(health <= 0)
        {
            gameOverObject.SetActive(true);
            Time.timeScale = 0f;
        }

        healthBar.value = health / (float)maxHealth;
    }




    public void AddScore(int bonusScore)
    {
        score += bonusScore;
        scoreText.text = score.ToString();
    }

   /* public void saveScores()
    {
        if(score > highScore)
        {
            PlayerPrefs.SetInt("BestScore", highScore);
            bestScoreText.text = score.ToString();
        }
            
    }

    public void getScores()
    {
        highScore = PlayerPrefs.GetInt("BestScore");
    }
   */




    public void EnemyKilled()
    {
        enemyKilled += 1;
    }

    public int GetEnemyKilled()
    {
        return enemyKilled;
    }



    public void BossSpawed()
    {
        boss = true;
    }

    public void BossUnSpawn()
    {
        boss = false;
    }

    public void BossKilled()
    {
        bossKilled += 1;
    }

    public int GetBossKilled()
    {
        return bossKilled;
    }
}
