using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;  // ������ �� ��������� MeshRenderer �������

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();  // �������� ��������� MeshRenderer ��� ������ �������
    }

    private void Update()
    {
        // �������� ������� �������� ���� �� GameManager � ��������� � �������� �������� �����
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;

        // ��������� �������� �������� �� ��� X � ������ �������� � �������
        meshRenderer.material.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
    }
}

