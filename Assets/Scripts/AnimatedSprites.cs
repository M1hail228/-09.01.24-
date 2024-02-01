using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;          // ������ �������� ��� ��������
    private SpriteRenderer spriteRenderer;  // ������ �� ��������� SpriteRenderer ��� ���������� ��������
    private int frame;                 // ������� ���� ��������

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // �������� ��������� SpriteRenderer ��� ������ �������
    }

    private void OnEnable()
    {
        // �������� ����� Animate � ��������� 0 ������ ��� ������� �������� ��� ��������� �������
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        // �������� ��� ������ ������ Animate ��� ���������� �������
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;  // ����������� ����� �������� �����

        // ���������, ���� ����� �������� ����� ��������� ���������� �������� � �������, ���������� ���
        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        // ���������, ��������� �� ����� �������� ����� � ���������� ��������� ������� ��������
        if (frame >= 0 && frame < sprites.Length)
        {
            // ������������� ������ �������� �����
            spriteRenderer.sprite = sprites[frame];
        }

        // ��������� ����� Animate ����� � ����������, ��������� �� �������� ���� �� GameManager
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}