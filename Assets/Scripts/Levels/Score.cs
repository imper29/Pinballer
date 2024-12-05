using Game.Logic.Inputs;
using UnityEngine;

namespace Game.Levels
{
    [AddComponentMenu("Game/Levels/Score"), RequireComponent(typeof(Level))]
    public class Score : MonoBehaviour
    {
        [SerializeField]
        private long m_Points;
        [SerializeField]
        private float m_Multiplier;
        [SerializeField]
        private float m_MultiplierTimeoutDelay;

        private float m_MultiplierTimeout;

        private void Update()
        {
            if (m_Multiplier != 1f && Time.time >= m_MultiplierTimeout)
                ResetMultiplier();
        }

        /// <summary>
        /// Determines the score.
        /// </summary>
        public long Points
        {
            get => m_Points;
        }
        /// <summary>
        /// Determines the score multiplier;
        /// </summary>
        public long Multiplier
        {
            get => Mathf.FloorToInt(m_Multiplier);
        }

        /// <summary>
        /// Adds an amount to the score.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void Add(long amount)
        {
            long offset = amount * Multiplier;
            if (offset != 0)
            {
                m_Points = m_Points + offset;
                if (m_Points < 0)
                    m_Points = 0;
                foreach (var listener in FindObjectsOfType<InputScore>())
                {
                    listener.m_Score.Invoke(m_Points);
                    listener.m_ScoreDelta.Invoke(offset);
                }
            }
        }
        /// <summary>
        /// Removes an amount from the score.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void Remove(long amount)
        {
            Add(-amount);
        }

        /// <summary>
        /// Resets the score multiplier.
        /// </summary>
        public void ResetMultiplier()
        {
            AddMultiplier(1f - m_Multiplier);
            m_MultiplierTimeout = Time.time;
        }
        /// <summary>
        /// Adds an amount to the score multiplier.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void AddMultiplier(float amount)
        {
            var multiplierNext = Mathf.Max(m_Multiplier + amount, 1f);
            if (m_Multiplier != multiplierNext)
            {
                m_MultiplierTimeout = Time.time + m_MultiplierTimeoutDelay;
                var multiplierRoundedPrevious = Multiplier;
                m_Multiplier = multiplierNext;
                var multiplierRounded = Multiplier;
                if (multiplierRounded != multiplierRoundedPrevious)
                {
                    var multiplierRoundedDelta = multiplierRounded - multiplierRoundedPrevious;
                    foreach (var listener in FindObjectsOfType<InputScoreMultiplier>())
                    {
                        listener.m_ScoreMultiplier.Invoke(multiplierRounded);
                        listener.m_ScoreMultiplierDelta.Invoke(multiplierRoundedDelta);
                    }
                }
            }
        }
        /// <summary>
        /// Removes an amount from the score multiplier.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void RemoveMultiplier(float amount)
        {
            AddMultiplier(-amount);
        }
    }
}
