using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ��������� ������ ��� �������� ���-5 ��������
    private float[] topScores;

    public TextMeshProUGUI[] topScoreTexts;

    // ���������� ��� ������ ����� MainMenu
    private void Start()
    {
        // ��������� ���-5 �������� �� PlayerPrefs
        LoadTopScores();

        // ���������� ���-5 �������� � UI
        UpdateTopScoreUI();
    }

    // ��� ������ Play
    public void PlayGame()
    {
        // ������ ��������� �� ������� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // ��� ������ Exit
    public void ExitGame()
    {
        Application.Quit();
    }

    // ����� ��� �������� ���-5 �������� �� PlayerPrefs
    private void LoadTopScores()
    {
        // ���������� �� �� ������, ��� � � GameManager
        topScores = new float[5];

        // ��������� ������� ���-5 ������� �� PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            topScores[i] = PlayerPrefs.GetFloat("hiscore" + (i + 1), 0);
        }
    }

    // ����� ��� ���������� ������������ ���-5 �������� � UI
    private void UpdateTopScoreUI()
    {

        // ���������� ���-5 �������� � UI
        for (int i = 0; i < topScoreTexts.Length; i++)
        {
            topScoreTexts[i].text = Mathf.FloorToInt(topScores[i]).ToString("D5");
        }

    }
}
