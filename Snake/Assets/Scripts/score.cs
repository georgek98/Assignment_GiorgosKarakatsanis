using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class score : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        string read_txt = File.ReadAllText("Assets/Top_score.txt");
        scoreText.text = read_txt + " POINTS";
    }


}
