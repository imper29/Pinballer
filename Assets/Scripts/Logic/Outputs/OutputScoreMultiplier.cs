using Game.Levels;
using UnityEngine;

namespace Game.Logic.Outputs
{
    public class OutputScoreMultiplier : MonoBehaviour
    {
        [SerializeField]
        private float m_Multiplier;

        public void Execute()
        {
            var score = FindObjectOfType<Score>();
            if (score)
                score.AddMultiplier(m_Multiplier);
        }
    }
}
