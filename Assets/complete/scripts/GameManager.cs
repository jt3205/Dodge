using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Text scoreText;
    private float time;
    private bool isOver = false;
	
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    void Start (){
        time = 0f;
	}
	
	void Update (){
        if (isOver)
            return;
        time += Time.deltaTime;
        scoreText.text = "생존시간 : " + time.ToString("F2");
	}

    public void SetGameOver()
    {
        isOver = true;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
