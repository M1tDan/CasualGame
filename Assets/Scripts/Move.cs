using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Move : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 targetPosition;
    public float speed = 5f;
    private int Score = 0;

    public GameObject Player; //Игрок
    public TMP_Text score, result; //Текст счёта/результат

    void Start()
    {
        score.text = ("Your score: " + Score);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
        }
        if (isMoving)
        {
            move();
        }
    }

    private void SetTargetPosition() //Инициализация места нажатия
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        isMoving = true;
    }

    private void move() //Перемещение игрока
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Score = Score + 1;
            score.text = ("Your score: " + Score);
            Destroy(collision.gameObject);
            if(Score == 5)
            {
                Result();
            }
        }
        else if(collision.gameObject.CompareTag("Spikes"))
        {
            Result();
        }
        else
        {

        }
    }

    public void Result()
    {
        Destroy(Player);
        score.gameObject.SetActive(false);
        result.gameObject.SetActive(true);
        if(Score >= 5)
        {
            result.text = ("You WIN\n" + "Your score: " + Score);
        }
        else
        {
            result.text = ("You LOSE\n" + "Your score: " + Score);
        }
    }
}
