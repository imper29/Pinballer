using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Inputs
{
    public class InputBallAlive : MonoBehaviour
    {
        [SerializeField]
        internal UnityEvent<Ball> m_BallCreated;
        [SerializeField]
        internal UnityEvent<Ball> m_BallDestroyed;
    }
}
