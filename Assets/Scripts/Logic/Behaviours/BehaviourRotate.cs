using UnityEngine;

namespace Game.Logic.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BehaviourRotate : MonoBehaviour
    {
        [SerializeField]
        private float m_CounterClockwiseDegreesPerSecond;
        [SerializeField]
        private Rigidbody2D m_Rigidbody;

        private void FixedUpdate()
        {
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation + m_CounterClockwiseDegreesPerSecond * Time.fixedDeltaTime);
        }
        private void OnValidate()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
