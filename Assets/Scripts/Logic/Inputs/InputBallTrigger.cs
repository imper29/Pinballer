using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Inputs
{
    public class InputBallTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<Ball> m_BallTriggerEntered;
        [SerializeField]
        private UnityEvent<Ball> m_BallTriggerExited;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.attachedRigidbody)
            {
                var ball = collision.attachedRigidbody.GetComponent<Ball>();
                if (ball)
                    m_BallTriggerEntered.Invoke(ball);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.attachedRigidbody)
            {
                var ball = collision.attachedRigidbody.GetComponent<Ball>();
                if (ball)
                    m_BallTriggerExited.Invoke(ball);
            }
        }
    }
}
