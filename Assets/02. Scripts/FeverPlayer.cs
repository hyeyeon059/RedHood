using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverPlayer : MonoBehaviour
{
    [SerializeField] private float snapOffset = 30;
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private Transform playerPre;
    public bool canMoving = true;
    public bool isGameOver = false;

    PlayerMove playerMove;
    GameManager gameManager;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>(true);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        ScreenCheck();
    }

    void OnMouseDrag()
    {
        if (isGameOver) canMoving = false;
        if (!canMoving) return;
        transform.position = GetMousePos();
    }

    private void OnMouseDown()
    {
        if (!isGameOver)
        {
            if (Vector3.Distance(playerPosition.transform.position, transform.position) < snapOffset)
            {
                transform.SetParent(playerPosition.transform);
                transform.localPosition = Vector3.zero;
            }
        }
        canMoving = true;
        if (!canMoving) return;
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void ScreenCheck()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(transform.position);
        if (point.x < 0.1f) point.x = 0.1f;
        if (point.x > 0.9f) point.x = 0.9f;
        if (point.y < 0.1f) point.y = 0.1f;
        if (point.y > 0.9f) point.y = 0.9f;
        transform.position = Camera.main.ViewportToWorldPoint(point);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            gameManager.ScoreCount();
            gameManager.Particle();
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(collision.gameObject);
            gameManager.ScoreCount();
            gameManager.Particle();
        }
    }
}
