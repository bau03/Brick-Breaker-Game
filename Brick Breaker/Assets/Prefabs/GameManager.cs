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
        livesText.text = "Can: " + lives;
        scoreText.text = "Skor: " + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changeInLives) {
        lives += changeInLives; 
        if (lives<=0){
            lives = 0;
            GameOver();
        }
        livesText.text = "Can: " + lives;
    }

    public void UpdateScore(int points)
    {
        score += points; 
        scoreText.text = "Skor: " + score;
    }
    public void UpdateNumberOfBricks() {
        numberOfBricks--;
        if (numberOfBricks <= 0) { 
            if (currentLevelIndex >= levels.Length-1) { 
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
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length; 
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }
    void GameOver(){
        gameOver = true;
        gameOverPanel.SetActive(true); 
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score>highScore){
            PlayerPrefs.SetInt("HIGHSCORE",score);
            highScoreText.text = "Yeni Yüksek Skor! " + "\n"+"Adınızı Giriniz.";
            highScoreInput.gameObject.SetActive(true);
        }
        else
        {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s" + " Yüksek Puan " + highScore + "\n" + "Geçe bilirmisin?";
        }
    }

    public void NewhighScore() {
        string highScoreName=highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text ="Tebrikler "+ highScoreName+"\n"+"Yeni Yüksek Puanınız " + score;
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene"); 
    }
    public void Quit() {
        SceneManager.LoadScene("Start Menu"); 
    }
}
