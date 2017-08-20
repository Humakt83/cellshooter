using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    private int score;
    private bool gameOver;
    private bool restart;

    void Start() {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update() {
        if (restart && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int score) {
        this.score += score;
        UpdateScore();
    }

    public void GameOver() {
        gameOverText.text = "Game Over";
        gameOver = true;
        restartText.text = "Press 'R' for Restart";
        restart = true;
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while (!gameOver) {
            for (int i = 0; i < hazardCount; i++) {
                SpawnAsteroid();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    private void SpawnAsteroid() {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(hazards[Random.Range(0, hazards.Length)], spawnPosition, spawnRotation);
    }

    private void UpdateScore() {        
        scoreText.text = "Score: " + score;
    }

}
