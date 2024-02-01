using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    private float leftEdge;

    // Вызывается при старте объекта
    private void Start()
    {
        // Определение левой границы экрана в мировых координатах
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    // Вызывается каждый кадр
    private void Update()
    {
        // Перемещение объекта влево с учетом скорости игры и времени прошедшего с предыдущего кадра
        transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;


        // Уничтожение объекта, если он выходит за левую границу экрана
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
