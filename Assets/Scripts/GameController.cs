using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Text scoreText;
    public Text Bestscoretext;
    public Text gameOverScoretext;
    public Text Inputtext;
    public Text YouWintext;
    public GameObject gameOverText;
    public GameObject victory;
   // public Image ScoreImg;
    public bool isWin = false;

    public bool isGameOver = false;

    public int score = 0;
    public int BestScore = 0;

    // dang dung ky thuat sigleton

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance = this)
        {
            // trang bi trung lap neu ton tai roi xoa
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Bestscoretext.text = ("" + PlayerPrefs.GetInt("BestScore"));
        this.BestScore = PlayerPrefs.GetInt("BestScore", 0);
    }
    public void Addscore(int score)
    {
        this.score += score;
        scoreText.text = " X   " +   this.score;

    }
    
    public void GameOver()
    {
        gameOverScoretext.text = " " + this.score;

        if (PlayerPrefs.HasKey("score"))
        {
            Scene ActiveScreen = SceneManager.GetActiveScene();
            if (ActiveScreen.buildIndex == 0)
            {
                PlayerPrefs.DeleteKey("score");
                score = 0;
            }
            else
            {
                score = PlayerPrefs.GetInt("score");
            }
        }

        gameOverText.SetActive(true);
        isGameOver = true;
        
    }
    public void YouWin()
    {
        YouWintext.text = " VICTORY ";
        victory.SetActive(true);
        isWin = true;
        
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
