using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy1 : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            gameManager.ScoreCount();
            gameManager.Particle();
        }
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            gameManager.SetActiveTrue();
        }
    }
}
