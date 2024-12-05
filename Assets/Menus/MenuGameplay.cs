using Game.Levels;
using UnityEngine;

public class MenuGameplay : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI m_TextClock;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_TextLives;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_TextScore;

    private void OnEnable()
    {
        RefreshLives(FindObjectOfType<Level>().Lives);
        RefreshScore(FindObjectOfType<Score>().Points);
    }

    public void RefreshClock(float remaining) {
        int minute = Mathf.FloorToInt(remaining / 60.0f);
        int second = Mathf.FloorToInt(remaining % 60.0f);
        m_TextClock.text = $"{minute:D2}:{second:D2}";
    }
    public void RefreshLives(int lives)
    {
        m_TextLives.text = lives.ToString();
    }
    public void RefreshScore(long score)
    {
        m_TextScore.text = score.ToString();
    }
}
