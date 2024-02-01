using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    // Статический экземпляр GameManager для доступа из других классов
    public static GameManager Instance { get; private set; }

    // Начальная скорость игры
    public float initialGameSpeed = 5f;

    // Увеличение скорости игры с течением времени
    public float gameSpeedIncrease = 0.1f;

    // Текущая скорость игры
    public float gameSpeed { get; private set; }

    // UI-элементы
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public Button MainMenuButton;
    //// Ссылки на объекты Player и Spawner
    private Player player;
    private Spawner spawner;

    // Текущий счет в игре
    private float score;

    // Свойство для получения значения текущего счета из других классов
    public float Score => score;


    public TextMeshProUGUI[] topScoreTexts;

    // Вызывается при старте игры
    private void Awake()
    {
        // Установка экземпляра GameManager, если он еще не установлен
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Вызывается при уничтожении объекта GameManager
    private void OnDestroy()
    {
        // При уничтожении, проверяем, является ли текущий экземпляр GameManager
        // установленным экземпляром, и, если так, обнуляем его
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // Вызывается при старте игры
    private void Start()
    {
        //// Поиск объектов Player и Spawner в сцене
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        // Начало новой игры
        NewGame();
    }

    // Метод для начала новой игры
    public void NewGame()
    {
        // Удаление всех препятствий из сцены
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Сброс счета и скорости игры
        score = 0f;
        gameSpeed = initialGameSpeed;

        // Включение GameManager для обновления скорости и счета
        enabled = true;

        // Включение игрока и спаунера, отключение текста "Game Over" и кнопки "Retry"
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        MainMenuButton.gameObject.SetActive(false);

        // Обновление лучшего счета
        UpdateHiscore();
    }

    // Метод для завершения игры
    public void GameOver()
    {
        // Остановка игровой скорости и отключение GameManager
        gameSpeed = 0f;
        enabled = false;

        // Отключение игрока и спаунера, включение текста "Game Over" и кнопки "Retry"
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        MainMenuButton.gameObject.SetActive(true);

        // Обновление лучшего счета
        UpdateHiscore();
    }

    // Вызывается на каждом кадре
    private void Update()
    {
        // Увеличение скорости игры и обновление счета
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;

        // Отображение счета в UI
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        CheckForCollision();

    }

    // Обновление лучшего счета
    private void UpdateHiscore()
    {
        float[] topScores = new float[5];

        // Загружаем текущие топ-5 рекорды из PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            topScores[i] = PlayerPrefs.GetFloat("hiscore" + (i + 1), 0);
        }

        // Проверяем, улучшился ли текущий счет и обновляем топ-5 при необходимости
        for (int i = 0; i < 5; i++)
        {
            if (score > topScores[i])
            {
                float temp = topScores[i];
                topScores[i] = score;
                score = temp;
            }
        }

        // Сохраняем обновленные топ-5 рекорды в PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat("hiscore" + (i + 1), topScores[i]);
        }

        // Отображаем топ-5 рекордов в UI
        for (int i = 0; i < 5; i++)
        {
            topScoreTexts[i].text = Mathf.FloorToInt(topScores[i]).ToString("D5");
        }
    }

    private void CheckForCollision()
    {

        if (player.transform.position.y < -5f)
        {
            GameOver();
        }
    }

    // Method to handle MainMenuButton click
    public void MainMenuButtonClick()
    {
        // Load the "MainMenu" scene
        SceneManager.LoadScene("MainMenu");
    }

}