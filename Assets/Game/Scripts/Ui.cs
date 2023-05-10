using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    public static Ui Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

    }


    [SerializeField] Text txtScore;
    [SerializeField] GameObject panelGameOver;


    int score = 0;
    public void AddScore()
    {
        score += 5;

        txtScore.text = score.ToString();
    }

    void Start()
    {
        
    }

   public void GameOverScreen()
    {

        panelGameOver.SetActive(true);
    }

    public void BtnRestart()
    {
        SceneManager.LoadScene(0);

    }
}
