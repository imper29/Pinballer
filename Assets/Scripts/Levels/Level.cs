using UnityEngine;
using UnityEngine.Events;

namespace Game.Levels
{
    [AddComponentMenu("Game/Levels/Level")]
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private int m_Lives;
        [SerializeField]
        private UnityEvent m_Gameover;
        [SerializeField]
        private UnityEvent<int> m_LivesChanged;

        public int Lives
        {
            get => m_Lives;
        }

        private void Start()
        {
            if (!FindObjectOfType<Ball>())
                SpawnBallOrGameover();
        }

        public void SpawnBallOrGameover()
        {
            if (m_Lives == 0)
            {
                gameObject.SetActive(false);
                m_Gameover.Invoke();
            }
            else
            {
                --m_Lives;
                m_LivesChanged.Invoke(m_Lives);
                var spawns = FindObjectsOfType<Spawn>();
                var spawn = spawns[Random.Range(0, spawns.Length)];
                Instantiate(spawn.m_Prefab, spawn.transform.position, spawn.transform.rotation, spawn.transform);
            }
        }
        public void Gameover() {
            m_Lives = 0;
            m_LivesChanged.Invoke(m_Lives);
            gameObject.SetActive(false);
            m_Gameover.Invoke();
        }
    }
}
