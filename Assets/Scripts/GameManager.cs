using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    // ����������� ��������� GameManager ��� ������� �� ������ �������
    public static GameManager Instance { get; private set; }

    // ��������� �������� ����
    public float initialGameSpeed = 5f;

    // ���������� �������� ���� � �������� �������
    public float gameSpeedIncrease = 0.1f;

    // ������� �������� ����
    public float gameSpeed { get; private set; }

    // UI-��������
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public Button MainMenuButton;
    //// ������ �� ������� Player � Spawner
    private Player player;
    private Spawner spawner;

    // ������� ���� � ����
    private float score;

    // �������� ��� ��������� �������� �������� ����� �� ������ �������
    public float Score => score;


    public TextMeshProUGUI[] topScoreTexts;

    // ���������� ��� ������ ����
    private void Awake()
    {
        // ��������� ���������� GameManager, ���� �� ��� �� ����������
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // ���������� ��� ����������� ������� GameManager
    private void OnDestroy()
    {
        // ��� �����������, ���������, �������� �� ������� ��������� GameManager
        // ������������� �����������, �, ���� ���, �������� ���
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // ���������� ��� ������ ����
    private void Start()
    {
        //// ����� �������� Player � Spawner � �����
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        // ������ ����� ����
        NewGame();
    }

    // ����� ��� ������ ����� ����
    public void NewGame()
    {
        // �������� ���� ����������� �� �����
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // ����� ����� � �������� ����
        score = 0f;
        gameSpeed = initialGameSpeed;

        // ��������� GameManager ��� ���������� �������� � �����
        enabled = true;

        // ��������� ������ � ��������, ���������� ������ "Game Over" � ������ "Retry"
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        MainMenuButton.gameObject.SetActive(false);

        // ���������� ������� �����
        UpdateHiscore();
    }

    // ����� ��� ���������� ����
    public void GameOver()
    {
        // ��������� ������� �������� � ���������� GameManager
        gameSpeed = 0f;
        enabled = false;

        // ���������� ������ � ��������, ��������� ������ "Game Over" � ������ "Retry"
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        MainMenuButton.gameObject.SetActive(true);

        // ���������� ������� �����
        UpdateHiscore();
    }

    // ���������� �� ������ �����
    private void Update()
    {
        // ���������� �������� ���� � ���������� �����
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;

        // ����������� ����� � UI
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        CheckForCollision();

    }

    // ���������� ������� �����
    private void UpdateHiscore()
    {
        float[] topScores = new float[5];

        // ��������� ������� ���-5 ������� �� PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            topScores[i] = PlayerPrefs.GetFloat("hiscore" + (i + 1), 0);
        }

        // ���������, ��������� �� ������� ���� � ��������� ���-5 ��� �������������
        for (int i = 0; i < 5; i++)
        {
            if (score > topScores[i])
            {
                float temp = topScores[i];
                topScores[i] = score;
                score = temp;
            }
        }

        // ��������� ����������� ���-5 ������� � PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat("hiscore" + (i + 1), topScores[i]);
        }

        // ���������� ���-5 �������� � UI
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