using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : " + Score.totalScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Score.totalScore += 1;
            Debug.Log(Score.totalScore);
            gameObject.SetActive(false);
            scoreText.text = "Score : " + Score.totalScore;
        }
    }
}
