using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Inputs
{
    public class InputBallCollision : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Ball> m_BallCollisionEntered;
        [SerializeField]
        private UnityEvent<Ball> m_BallCollisionExited;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.rigidbody)
            {
                var ball = collision.rigidbody.GetComponent<Ball>();
                if (ball)
                    m_BallCollisionEntered.Invoke(ball);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.rigidbody)
            {
                var ball = collision.rigidbody.GetComponent<Ball>();
                if (ball)
                    m_BallCollisionExited.Invoke(ball);
            }
        }
    }
}
