using UnityEngine;

namespace Game.Logic.Behaviours
{
    public class BehaviourMusicPlayer : MonoBehaviour
    {
        private static bool m_Initialized;
        [SerializeField]
        private AudioSource m_AudioSource;
        [SerializeField]
        private AudioClip[] m_Music;

        private void Awake()
        {
            if (m_Initialized)
            {
                Destroy(gameObject);
            }
            else
            {
                m_Initialized = true;
                DontDestroyOnLoad(gameObject);
            }
        }
        private void Update()
        {
            if (!m_AudioSource.isPlaying)
                m_AudioSource.PlayOneShot(m_Music[Random.Range(0, m_Music.Length)]);
        }
    }
}
