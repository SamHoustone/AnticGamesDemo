using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public AudioSource bgm;

    public GameObject menuObject,endScreenObject,instructionObject;
    public TextMeshProUGUI aiScoreText ,playerscoreText, powerText, noOfMovesText,remarkText,remarkScoreText;
    public Button musicOn, musicOff, menu, exit, Resume, Replay,startGame;
    public GameManager gameManager;
    public bool muteGame, inMenu;

    private void Start()
    {
        Time.timeScale = 0;
        menu.onClick.AddListener(Menu);
        Resume.onClick.AddListener(Menu);
        musicOn.onClick.AddListener(Music);
        musicOff.onClick.AddListener(Music);
        exit.onClick.AddListener(Exit);
        Replay.onClick.AddListener(RestartGame);
        startGame.onClick.AddListener(StartGame);
    }
    private void StartGame()
    {
        instructionObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void UpdateMoves(int num)
    {
        noOfMovesText.text = "MOVES LEFT : " +  num.ToString();
    }
    public void UpdateEndScreen()
    {
        endScreenObject.SetActive(true);

        if (gameManager.PlayerScore > gameManager.AiScore)
        {
            remarkText.text = " YOU WON ";

        }
        else
        {
            remarkText.text = " YOU LOST ";
        }
        remarkScoreText.text = "YOU SCORE " + gameManager.PlayerScore + " AGAISNT THE AI AT " + gameManager.AiScore;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void Music()
    {
        if(muteGame)
        {
            musicOn.gameObject.SetActive(true);
            musicOff.gameObject.SetActive(false);
            bgm.mute = false;

            muteGame = false;
        }
        else
        {
            musicOn.gameObject.SetActive(false);
            musicOff.gameObject.SetActive(true);

            musicOff.enabled = true;
            bgm.mute = true;

            muteGame = true;
        }
    }

    public void Menu()
    {
        if (!inMenu)
        {
            menuObject.SetActive(true);
            inMenu = true;
            Time.timeScale = 0;
        }
        else
        {
            menuObject.SetActive(false);
            inMenu = false;
            Time.timeScale = 1;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void UpdatePlayerScore(int scoreToAdd)
    {
        gameManager.PlayerScore += scoreToAdd;
        playerscoreText.text = "POINTS : " + gameManager.PlayerScore.ToString();
    }
    public void UpdateAiScore(int scoreToAdd)
    {
        gameManager.AiScore += scoreToAdd;
        aiScoreText.text = "POINTS : " + gameManager.AiScore.ToString();
    }

    public void UpdatePower(float power)
    {
        powerText.text = "POWER : " + Mathf.Round(power).ToString();
    }


}
