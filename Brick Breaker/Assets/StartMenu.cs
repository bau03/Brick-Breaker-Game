using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    public Text highscoreText;

    void Start() {
        if (PlayerPrefs.GetString("HIGHSCORENAME")!="")
        {
            highscoreText.text = "Yüksek Puan Sahibi " + PlayerPrefs.GetString("HIGHSCORENAME") + " " + PlayerPrefs.GetInt("HIGHSCORE");
        }
      
    }
    public void QuitGame() {
        Application.Quit();
        Debug.Log("Cıkış yaptı.");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
