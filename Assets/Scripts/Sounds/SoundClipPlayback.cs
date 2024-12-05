using UnityEngine;

namespace Game.Sounds
{
    [AddComponentMenu("Game/Sounds/SoundClipPlayback")]
    public class SoundClipPlayback : MonoBehaviour
    {
        [SerializeField]
        internal SoundClip m_Clip;

        private void Start()
        {
            var source = GetComponent<AudioSource>();
            source.clip = m_Clip.m_Source;
            source.volume = m_Clip.m_Volume + Random.Range(0f, m_Clip.m_VolumeRange);
            source.pitch = m_Clip.m_Pitch + Random.Range(0f, m_Clip.m_PitchRange);
            source.priority = m_Clip.m_Priority;
            source.time = m_Clip.m_Start;
            source.SetScheduledEndTime(m_Clip.m_Stop);
            source.Play();
            Destroy(gameObject, m_Clip.m_Stop - m_Clip.m_Start);
        }
    }
}
