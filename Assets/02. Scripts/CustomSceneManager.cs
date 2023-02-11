using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : MonoBehaviour
{
    [SerializeField] float animTime = 2f;
    [SerializeField] Image fadeImage;

    private float start = 1f;
    private float end = 0f;
    private float time = 0f;
    private bool isFadeOut = false;

    [SerializeField] bool stopIn = false;
    [SerializeField] bool stopOut = true;

    GameManager gameManager;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        if(stopIn == false && time <= 2)
        {
            PlayFadeIn();
        }

        if (stopOut == false && time <= 2)
        {
            PlayFadeOut();
            Time.timeScale = 1f;
        }

        if(time >= 2 && stopIn == false)
        {
            stopIn = true;
            time = 0;
        }

        if (time >= 2 && stopOut == false)
        {
            stopIn = false;
            stopOut = true;
            time = 0;
        }
    }

    private void PlayFadeIn()
    {
        time += Time.deltaTime / animTime;
        Color color = fadeImage.color;
        color.a = Mathf.Lerp(start, end, time);
        fadeImage.color = color;
    }

    private void PlayFadeOut()
    {
        time += Time.deltaTime / animTime;
        Color color = fadeImage.color;
        color.a = Mathf.Lerp(end, start, time);
        fadeImage.color = color;

        isFadeOut = true;

        if(isFadeOut)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }
}
