using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //skor ve can kontrolleri
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public InputField highScoreInput;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length; //tugla sayısı
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changeInLives) {
        lives += changeInLives; //can güncelemesi
        //hiç can kalmadığını kontrol et ve oyunu bitir.
        if (lives<=0){
            lives = 0;
            GameOver();
        }
        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int points)
    {
        score += points; //score güncelle
        scoreText.text = "Score: " + score;
    }
    public void UpdateNumberOfBricks() {
        numberOfBricks--;
        if (numberOfBricks <= 0) { //tugla biterse
            if (currentLevelIndex >= levels.Length-1) { //level atlama
                GameOver();
            }else {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Level" + (currentLevelIndex+2);
                gameOver = true;
                Invoke("LoadLevel", 3f);
             }
            
        }
    }

    void LoadLevel() {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length; //yeni tugla sayısı
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }
    void GameOver(){
        gameOver = true;
        gameOverPanel.SetActive(true); // game over panelini göster.
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score>highScore){
            PlayerPrefs.SetInt("HIGHSCORE",score);
            highScoreText.text = "New High Score! " + "\n"+"Enter Your Name Bellow.";
            highScoreInput.gameObject.SetActive(true);
        }
        else
        {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s" + " High Score was " + highScore + "\n" + "Can you beat it?";
        }
    }

    public void NewhighScore() {
        string highScoreName=highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text ="Congratulations "+ highScoreName+"\n"+"Your New High Score is" + score;
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene"); //oyun u tekrar başlat
    }
    public void Quit() {
        SceneManager.LoadScene("Start Menu"); //oyun u tekrar başlat
    }
}
