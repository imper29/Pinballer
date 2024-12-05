using Game.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameover : MonoBehaviour
{
    private static long m_Highscore;
    [SerializeField]
    private Score m_Score;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_TextScore;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_TextHighscore;

    public void Quit()
    {
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene("Level");
    }

    private void OnEnable()
    {
        var points = m_Score.Points;
        if (m_Highscore < points)
            m_Highscore = points;
        m_TextScore.text = $"{points}";
        m_TextHighscore.text = $"{m_Highscore}";
    }
}
