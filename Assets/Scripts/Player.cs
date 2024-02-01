using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Контроллер персонажа
    private CharacterController character;

    // Направление движения
    private Vector3 direction;

    // Флаг уклонения
    private bool isCrouching = false;

    // Исходная высота персонажа
    private float originalHeight;

    // Исходная позиция персонажа
    private Vector3 originalPosition;

    // Длительность уклонения
    private float crouchDuration = 1f;

    // Таймер уклонения
    private float crouchTimer;

    // Исходная сила уклона
    private float originalSlopeForce;

    // Сила прыжка
    public float jumpForce = 8f;

    // Значение гравитации
    public float gravity = 9.81f * 2f;

    // Сила уклона вниз
    public float slopeForce = 2f;

    // Множитель высоты при уклонении
    private const float crouchHeightMultiplier = 0.1f;

    private int score = 0;
    public TextMeshProUGUI scoreText;  // Ссылка на объект Text


    private int coinsCollected = 0;
    private bool isImmortal = false;
    private float immortalDuration = 0.2f;
    private float immortalCooldown = 5f;
    private float immortalTimer;

    // Инициализация при старте объекта
    private void Awake()
    {
        character = GetComponent<CharacterController>();
        originalHeight = character.height;
        originalPosition = transform.position;
        originalSlopeForce = slopeForce;
    }

    // Вызывается при включении объекта
    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    // Обновление каждый кадр
    private void Update()
    {
        // Вычисление движения персонажа
        CalculateMovement();

        // Начало уклонения при нажатии кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            StartCrouch();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // Проверка наличия бессмертия
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


        // Обработка уклона
        if (isCrouching)
        {
            // Уменьшение таймера уклона
            crouchTimer -= Time.deltaTime;

            // Остановка уклона при истечении таймера
            if (crouchTimer <= 0f)
            {
                StopCrouch();
            }
        }

        // Применение движения к персонажу
        character.Move(direction * Time.deltaTime);
    }

    // Вычисление движения персонажа
    private void CalculateMovement()
    {
        // Добавление гравитации
        direction += gravity * Time.deltaTime * Vector3.down;

        // Применение уклона вниз, если персонаж на земле
        if (character.isGrounded)
        {
            direction += Vector3.down * (isCrouching ? slopeForce * 5f : slopeForce);

            // Вызов прыжка при нажатии кнопки "Jump"
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
    }

    // Произведение прыжка
    private void Jump()
    {
        direction = Vector3.up * jumpForce;
    }

    // Начало уклонения
    private void StartCrouch()
    {
        if (!isCrouching)
        {
            // Сохранение исходной высоты и позиции перед уклонением
            originalHeight = character.height;
            originalPosition = transform.position;

            // Применение уклона с учетом множителя
            character.height = originalHeight * crouchHeightMultiplier;

            // Увеличение силы уклона вниз
            slopeForce *= 5f;

            // Установка флага уклонения
            isCrouching = true;

            // Установка таймера уклона
            crouchTimer = crouchDuration;
        }
    }

    // Остановка уклона
    private void StopCrouch()
    {
        if (isCrouching)
        {
            // Возвращение к обычной высоте
            character.height = originalHeight;

            // Возвращение на исходное место
            transform.position = originalPosition;

            // Восстановление исходной силы уклона
            slopeForce = originalSlopeForce;

            // Сброс флага уклонения
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
                // Обнуление счетчика монет при столкновении с препятствием
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
            // Предложение использовать бессмертие после набора 2 монет
            Debug.Log("Press Right Mouse Button to use Immortality");
        }
    }

    private void StartImmortality()
    {
        // Проверка наличия бессмертия и прошедшего времени после предыдущего использования
        if (!isImmortal && immortalTimer <= 0f)
        {
            // Начало бессмертия
            isImmortal = true;
            immortalTimer = immortalCooldown + immortalDuration; // Общее время, включая длительность и период ожидания
            Debug.Log("Immortality Activated");

            // Обнуление счетчика монет только при использовании бессмертия
            coinsCollected = 0;
            scoreText.text = coinsCollected.ToString();
            score = 0;
            // Ваш код, связанный с визуальным отображением активации бессмертия
        }
    }

    private void StopImmortality()
    {
        // Окончание бессмертия
        isImmortal = false;
        Debug.Log("Immortality Deactivated");
    }
}