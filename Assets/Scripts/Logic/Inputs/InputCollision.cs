using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Inputs
{
    public class InputCollision : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<float> m_CollisionEntered;
        [SerializeField]
        private UnityEvent<float> m_CollisionExited;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            m_CollisionEntered.Invoke(collision.relativeVelocity.magnitude);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            m_CollisionExited.Invoke(collision.relativeVelocity.magnitude);
        }
    }
}
