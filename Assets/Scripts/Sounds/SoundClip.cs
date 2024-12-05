using UnityEngine;

namespace Game.Sounds
{
    [CreateAssetMenu(menuName = "Game/SoundClip")]
    public class SoundClip : ScriptableObject
    {
        [SerializeField, Range(0, 1)]
        internal float m_Volume = 0.5f;
        [SerializeField, Range(0, 1)]
        internal float m_VolumeRange = 0f;

        [SerializeField, Range(-3, 3)]
        internal float m_Pitch = 0f;
        [SerializeField, Range(0, 6)]
        internal float m_PitchRange = 0f;
        
        [SerializeField, Range(0, 256)]
        internal int m_Priority = 128;

        [SerializeField, Min(0)]
        internal float m_Start;
        [SerializeField, Min(0)]
        internal float m_Stop;
        [SerializeField]
        internal AudioClip m_Source;
        [SerializeField]
        private SoundClipPlayback m_PlaybackPrefab;

        /// <summary>
        /// Plays sound.
        /// </summary>
        /// <param name="container">The sound container.</param>
        public void Play(Transform container)
        {
            SoundClipPlayback playback = Instantiate(m_PlaybackPrefab, container.position, container.rotation, container);
            playback.m_Clip = this;
        }
    }
}
