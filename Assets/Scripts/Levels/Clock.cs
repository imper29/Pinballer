using UnityEngine;

namespace Game.Levels {
    public class Clock : MonoBehaviour {
        [SerializeField]
        private Level m_Level;
        [SerializeField]
        private MenuGameplay m_Menu;

        [SerializeField]
        private float m_Duration;
        private float m_Time;

        private void Awake() {
            m_Time = 0.0f;
        }
        private void Update() {
            m_Time += Time.deltaTime;
            if (m_Time > m_Duration) {
                m_Level.Gameover();
            }
            m_Menu.RefreshClock(Mathf.Max(m_Duration - m_Time, 0.0f));
        }
    }
}
