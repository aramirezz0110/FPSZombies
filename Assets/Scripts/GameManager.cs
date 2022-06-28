using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int enemiesAlive; //enemies in scene
    public int rounds; //rounds played
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    
    [Header("UI")]
    public TMP_Text roundText;
    public TMP_Text roundsSurvivedText;
    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesAlive == 0)
        {
            rounds++;
            roundText.text = "Round: " + rounds;
            NextWave(rounds);
        }
    }
    public void NextWave(int round)
    {
        int randomPos;
        GameObject spawnPoint;
        for (int i=0; i < round; i++)
        {
            randomPos = Random.Range(0, spawnPoints.Length);
            spawnPoint = spawnPoints[randomPos];

            Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemiesAlive++;
        }
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        roundsSurvivedText.text = rounds.ToString();

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneRef.GameScene);
        Time.timeScale = 1;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneRef.MainMenuScene);
    }
}
public static class SceneRef
{
    public static string MainMenuScene = "MainMenu";
    public static string GameScene = "Game";    
}
