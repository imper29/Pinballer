using TMPro;
using UnityEngine;

namespace Game.Logic.Behaviours
{
    public class BehaviourHighlight : MonoBehaviour
    {
        [SerializeField]
        private float m_LifetimeMin, m_LifetimeMax;
        
        [SerializeField]
        private AnimationCurve m_LinearVelocityScale;
        [SerializeField]
        private Vector2 m_LinearVelocityMin, m_LinearVelocityMax;
        [SerializeField]
        private Vector2 m_LinearOffsetMin, m_LinearOffsetMax;
        
        [SerializeField]
        private AnimationCurve m_AngularVelocityScale;
        [SerializeField]
        private float m_AngularVelocityMin, m_AngularVelocityMax;
        [SerializeField]
        private float m_AngularOffsetMin, m_AngularOffsetMax;

        [SerializeField]
        private AnimationCurve m_TextOpacity;
        [SerializeField]
        private TextMeshPro m_Text;

        private Vector2 m_LinearVelocity;
        private float m_AngularVelocity;

        private float m_Lifetime;
        private float m_Deathtime;

        private void Awake()
        {
            transform.Translate(new(Random.Range(m_LinearOffsetMin.x, m_LinearOffsetMax.x), Random.Range(m_LinearOffsetMin.y, m_LinearOffsetMax.y)));
            transform.Rotate(Vector3.back, Random.Range(m_AngularOffsetMin, m_AngularOffsetMax));
            m_LinearVelocity = new(Random.Range(m_LinearVelocityMin.x, m_LinearVelocityMax.x), Random.Range(m_LinearVelocityMin.y, m_LinearVelocityMax.y));
            m_AngularVelocity = Random.Range(m_AngularVelocityMin, m_AngularVelocityMax);

            m_Lifetime = Time.time;
            m_Deathtime = Time.time + Random.Range(m_LifetimeMin, m_LifetimeMax); 
        }
        private void Update()
        {
            if (m_Deathtime <= Time.time)
                Destroy(gameObject);
            else
            {
                float time = Mathf.InverseLerp(m_Lifetime, m_Deathtime, Time.time);
                var alpha = m_TextOpacity.Evaluate(time);
                transform.Translate(m_LinearVelocity * m_LinearVelocityScale.Evaluate(time) * Time.deltaTime);
                transform.Rotate(Vector3.back, m_AngularVelocity * m_AngularVelocityScale.Evaluate(time) * Time.deltaTime);
                m_Text.alpha = alpha;
            }
        }
    }
}
