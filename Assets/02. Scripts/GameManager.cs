using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject feverItem;
    [SerializeField] private GameObject normalPlayer;
    [SerializeField] private GameObject feverPlayer;
    [SerializeField] private TextMeshProUGUI deathTxt;
    [SerializeField] private Button reStart;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private Image panel;
    [SerializeField] private TextMeshProUGUI feverTimeTxt;
    [SerializeField] private Image feverPanel;
    [SerializeField] private GameObject feverEffect;
    [SerializeField] private Button resumeButton;

    private List<GameObject> gameObject = new List<GameObject>();

    // [SerializeField] private ParticleSystem deathParticle;
    private int score = 0;
    private bool feverTxtSetActive = false;

    private Enemy1 enemyScript;
    private PlayerMove playerMove;

    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        StartCoroutine(StartGameDelay());
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;

        float posX = basePosition.x + Random.Range(-2.5f, 2.5f);
        float posY = basePosition.x + Random.Range(4.5f, 4.5f); 

        Vector2 spawnPos = new Vector2(posX, posY);
        return spawnPos;
    }

    public void CreateEnemy()
    {
        int selection = Random.Range(0, enemy.Length);
        GameObject selectedEnemy = enemy[selection];

        Vector2 spawnPos = GetRandomPosition();

        GameObject instance = Instantiate(selectedEnemy, spawnPos, Quaternion.identity);
        gameObject.Add(instance);
    }

    public void CreateFeverItem()
    {
        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 0f));
        position.z = 0.0f;
        Instantiate(feverItem, position, Quaternion.identity);
    }

    IEnumerator CreateEnemyRoutine()
    {
        while(true)
        {
            CreateEnemy();
            yield return new WaitForSeconds(0.8f);
        }
    }

    IEnumerator CreateFeverItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(26f);
            CreateFeverItem();
        }
    }

    public void SetActiveTrue()
    {
        deathTxt.gameObject.SetActive(true);
        reStart.gameObject.SetActive(true);
    }

    public void ReStart()
    {
        Time.timeScale = 1;
        deathTxt.gameObject.SetActive(false);
        reStart.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ScoreCount()
    {
        score++;
        scoreTxt.text = "score : " + score;
    }

    public void PauseButton()
    {
        panel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        panel.gameObject.SetActive(false);
        deathTxt.gameObject.SetActive(false);
        reStart.gameObject.SetActive(false);

        if(playerMove.isGameOver == true)
        {
            resumeButton.GetComponent<Button>().interactable = false;
        }
        Time.timeScale = 1f;
    }
    
    public void BackToStageScene()
    {
        panel.gameObject.SetActive(false);
        SceneManager.LoadScene("Stage");
    }

    public void BackToMainScene()
    {
        panel.gameObject.SetActive(false);
        SceneManager.LoadScene("Intro");
    }

    private void GameStart()
    {
        Time.timeScale = 1.0f;
    }

    public void FeverTime()
    {
        StartCoroutine(FeverTxtDelay());
    }

    public void Particle()
    {
        StartCoroutine(DeathParticle());
    }

    IEnumerator DeathParticle()
    {
        yield return new WaitForSeconds(0.6f);
    }

    IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(4f);
        GameStart();
        enemyScript = FindObjectOfType<Enemy1>();
        StartCoroutine(CreateEnemyRoutine());
        StartCoroutine(CreateFeverItemRoutine());
    }

    IEnumerator FeverTxtDelay()
    {
        feverPlayer.transform.position = normalPlayer.transform.position;

        normalPlayer.gameObject.SetActive(false);
        feverPlayer.gameObject.SetActive(true);
        Time.timeScale = 0f;
        feverTimeTxt.gameObject.SetActive(true);
        feverPanel.gameObject.SetActive(true);
        feverTxtSetActive = true;
        feverEffect.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);

        feverTimeTxt.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(7.0f);

        if (feverTxtSetActive)
        {
            feverEffect.gameObject.SetActive(false);
            feverPanel.gameObject.SetActive(false);
        }
        normalPlayer.gameObject.SetActive(true);
        feverPlayer.gameObject.SetActive(false);

        normalPlayer.transform.position = feverPlayer.transform.position;
        yield return null;
    }
}