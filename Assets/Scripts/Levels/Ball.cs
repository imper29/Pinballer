using Game.Logic.Inputs;
using UnityEngine;

namespace Game
{
    [AddComponentMenu("Game/Levels/Ball"), RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        private Rigidbody2D m_Rigidbody;

        /// <summary>
        /// Returns the rigidbody.
        /// </summary>
        public Rigidbody2D Rigidbody
        {
            get => m_Rigidbody;
        }

        private void Start()
        {
            foreach (var listener in FindObjectsOfType<InputBallAlive>())
                listener.m_BallCreated.Invoke(this);
        }
        private void OnDestroy()
        {
            foreach (var listener in FindObjectsOfType<InputBallAlive>())
                listener.m_BallDestroyed.Invoke(this);
        }
        private void OnValidate()
        {
            if (!m_Rigidbody)
                m_Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
