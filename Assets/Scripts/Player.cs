using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // ���������� ���������
    private CharacterController character;

    // ����������� ��������
    private Vector3 direction;

    // ���� ���������
    private bool isCrouching = false;

    // �������� ������ ���������
    private float originalHeight;

    // �������� ������� ���������
    private Vector3 originalPosition;

    // ������������ ���������
    private float crouchDuration = 1f;

    // ������ ���������
    private float crouchTimer;

    // �������� ���� ������
    private float originalSlopeForce;

    // ���� ������
    public float jumpForce = 8f;

    // �������� ����������
    public float gravity = 9.81f * 2f;

    // ���� ������ ����
    public float slopeForce = 2f;

    // ��������� ������ ��� ���������
    private const float crouchHeightMultiplier = 0.1f;

    private int score = 0;
    public TextMeshProUGUI scoreText;  // ������ �� ������ Text


    private int coinsCollected = 0;
    private bool isImmortal = false;
    private float immortalDuration = 0.2f;
    private float immortalCooldown = 5f;
    private float immortalTimer;

    // ������������� ��� ������ �������
    private void Awake()
    {
        character = GetComponent<CharacterController>();
        originalHeight = character.height;
        originalPosition = transform.position;
        originalSlopeForce = slopeForce;
    }

    // ���������� ��� ��������� �������
    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    // ���������� ������ ����
    private void Update()
    {
        // ���������� �������� ���������
        CalculateMovement();

        // ������ ��������� ��� ������� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            StartCrouch();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // �������� ������� ����������
            if (coinsCollected >= 5 && !isImmortal)
            {
                StartImmortality();
            }
        }


        if (isImmortal)
        {
            immortalTimer -= Time.deltaTime;

            if (immortalTimer <= 0f)
            {
                StopImmortality();
            }
        }


        // ��������� ������
        if (isCrouching)
        {
            // ���������� ������� ������
            crouchTimer -= Time.deltaTime;

            // ��������� ������ ��� ��������� �������
            if (crouchTimer <= 0f)
            {
                StopCrouch();
            }
        }

        // ���������� �������� � ���������
        character.Move(direction * Time.deltaTime);
    }

    // ���������� �������� ���������
    private void CalculateMovement()
    {
        // ���������� ����������
        direction += gravity * Time.deltaTime * Vector3.down;

        // ���������� ������ ����, ���� �������� �� �����
        if (character.isGrounded)
        {
            direction += Vector3.down * (isCrouching ? slopeForce * 5f : slopeForce);

            // ����� ������ ��� ������� ������ "Jump"
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
    }

    // ������������ ������
    private void Jump()
    {
        direction = Vector3.up * jumpForce;
    }

    // ������ ���������
    private void StartCrouch()
    {
        if (!isCrouching)
        {
            // ���������� �������� ������ � ������� ����� ����������
            originalHeight = character.height;
            originalPosition = transform.position;

            // ���������� ������ � ������ ���������
            character.height = originalHeight * crouchHeightMultiplier;

            // ���������� ���� ������ ����
            slopeForce *= 5f;

            // ��������� ����� ���������
            isCrouching = true;

            // ��������� ������� ������
            crouchTimer = crouchDuration;
        }
    }

    // ��������� ������
    private void StopCrouch()
    {
        if (isCrouching)
        {
            // ����������� � ������� ������
            character.height = originalHeight;

            // ����������� �� �������� �����
            transform.position = originalPosition;

            // �������������� �������� ���� ������
            slopeForce = originalSlopeForce;

            // ����� ����� ���������
            isCrouching = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (!isImmortal)
            {
                FindObjectOfType<GameManager>().GameOver();
                // ��������� �������� ����� ��� ������������ � ������������
                coinsCollected = 0;
                scoreText.text = coinsCollected.ToString();
                score = 0;
            }
        }
        else
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        score++;
        scoreText.text = score.ToString();

        coinsCollected++;

        if (coinsCollected >= 5 && !isImmortal)
        {
            // ����������� ������������ ���������� ����� ������ 2 �����
            Debug.Log("Press Right Mouse Button to use Immortality");
        }
    }

    private void StartImmortality()
    {
        // �������� ������� ���������� � ���������� ������� ����� ����������� �������������
        if (!isImmortal && immortalTimer <= 0f)
        {
            // ������ ����������
            isImmortal = true;
            immortalTimer = immortalCooldown + immortalDuration; // ����� �����, ������� ������������ � ������ ��������
            Debug.Log("Immortality Activated");

            // ��������� �������� ����� ������ ��� ������������� ����������
            coinsCollected = 0;
            scoreText.text = coinsCollected.ToString();
            score = 0;
            // ��� ���, ��������� � ���������� ������������ ��������� ����������
        }
    }

    private void StopImmortality()
    {
        // ��������� ����������
        isImmortal = false;
        Debug.Log("Immortality Deactivated");
    }
}