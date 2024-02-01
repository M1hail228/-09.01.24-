using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    private float leftEdge;

    // ���������� ��� ������ �������
    private void Start()
    {
        // ����������� ����� ������� ������ � ������� �����������
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    // ���������� ������ ����
    private void Update()
    {
        // ����������� ������� ����� � ������ �������� ���� � ������� ���������� � ����������� �����
        transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;


        // ����������� �������, ���� �� ������� �� ����� ������� ������
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
