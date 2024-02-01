using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Добавляем массив для хранения топ-5 рекордов
    private float[] topScores;

    public TextMeshProUGUI[] topScoreTexts;

    // Вызывается при старте сцены MainMenu
    private void Start()
    {
        // Загружаем топ-5 рекордов из PlayerPrefs
        LoadTopScores();

        // Отображаем топ-5 рекордов в UI
        UpdateTopScoreUI();
    }

    // Для кнопки Play
    public void PlayGame()
    {
        // Запуск следующей по индексу сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Для кнопки Exit
    public void ExitGame()
    {
        Application.Quit();
    }

    // Метод для загрузки топ-5 рекордов из PlayerPrefs
    private void LoadTopScores()
    {
        // Используем ту же логику, что и в GameManager
        topScores = new float[5];

        // Загружаем текущие топ-5 рекорды из PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            topScores[i] = PlayerPrefs.GetFloat("hiscore" + (i + 1), 0);
        }
    }

    // Метод для обновления отображаемых топ-5 рекордов в UI
    private void UpdateTopScoreUI()
    {

        // Отображаем топ-5 рекордов в UI
        for (int i = 0; i < topScoreTexts.Length; i++)
        {
            topScoreTexts[i].text = Mathf.FloorToInt(topScores[i]).ToString("D5");
        }

    }
}
