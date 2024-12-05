using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Inputs
{
    public class InputScore : MonoBehaviour
    {
        [SerializeField]
        internal UnityEvent<long> m_Score;
        [SerializeField]
        internal UnityEvent<long> m_ScoreDelta;
    }
}
