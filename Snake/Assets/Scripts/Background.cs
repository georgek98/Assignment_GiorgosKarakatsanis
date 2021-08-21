using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{

    public Text pointsText;
    public Text scoreText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }
    public void Score_update(int score)
    {
        scoreText.text = score.ToString() + " POINTS";
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenuButton()
    {
       SceneManager.LoadScene("Main_menu");
    }
}
