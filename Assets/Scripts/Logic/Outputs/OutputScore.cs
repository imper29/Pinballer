using Game.Levels;
using UnityEngine;

namespace Game.Logic.Outputs
{
    public class OutputScore : MonoBehaviour
    {
        [SerializeField]
        private int m_Points;

        public void Execute()
        {
            var score = FindObjectOfType<Score>();
            if (score)
                score.Add(m_Points);
        }
    }
}
