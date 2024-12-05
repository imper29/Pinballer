using UnityEngine;

namespace Game
{
    public class BallRenderer : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D m_Rigidbody;
        [SerializeField]
        private SpriteRenderer m_Sprite;
        [SerializeField]
        private Gradient m_ColorGradient;
        [SerializeField]
        private float m_ColorVelocityMin, m_ColorVelocityMax;

        public Color Color
        {
            get
            {
                var velocity = Mathf.Clamp(m_Rigidbody.velocity.magnitude, m_ColorVelocityMin, m_ColorVelocityMax);
                return m_ColorGradient.Evaluate(Mathf.InverseLerp(m_ColorVelocityMin, m_ColorVelocityMax, velocity));
            }
        }

        private void Update()
        {
            m_Sprite.color = Color;
        }
    }
}
