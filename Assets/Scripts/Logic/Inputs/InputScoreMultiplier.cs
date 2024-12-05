using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Inputs
{
    public class InputScoreMultiplier : MonoBehaviour
    {
        [SerializeField]
        internal UnityEvent<long> m_ScoreMultiplier;
        [SerializeField]
        internal UnityEvent<long> m_ScoreMultiplierDelta;
    }
}
