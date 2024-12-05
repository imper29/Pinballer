using UnityEngine;

namespace Game
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField]
        private float m_LinearIntensityMaximum, m_LinearTimescaleX, m_LinearTimescaleY;
        [SerializeField]
        private AnimationCurve m_LinearIntensityDamping;
        [SerializeField]
        private float m_AngularIntensityMaximum, m_AngularTimescale;
        [SerializeField]
        private AnimationCurve m_AngularIntensityDamping;
        
        [SerializeField]
        private AnimationCurve m_AngularCurve;
        [SerializeField]
        private AnimationCurve m_LinearCurveX, m_LinearCurveY;

        private float m_LinearIntensity;
        private float m_AngularIntensity;

        private void Update()
        {
            //Apply shake damping.
            m_LinearIntensity = Mathf.Max(0f, m_LinearIntensity - m_LinearIntensityDamping.Evaluate(m_LinearIntensity / m_LinearIntensityMaximum) * Time.deltaTime);
            m_AngularIntensity = Mathf.Max(0f, m_LinearIntensity - m_AngularIntensityDamping.Evaluate(m_AngularIntensity / m_AngularIntensityMaximum) * Time.deltaTime);
            //Apply shake state.
            transform.localPosition = new(m_LinearCurveX.Evaluate(Time.time * m_LinearTimescaleY) * m_AngularIntensity, m_LinearCurveY.Evaluate(Time.time * m_LinearTimescaleY) * m_AngularIntensity);
            transform.rotation = Quaternion.AngleAxis(m_AngularCurve.Evaluate(Time.time * m_AngularTimescale) * m_AngularIntensity, Vector3.back);
        }

        public void ShakeLinear(float intensity)
        {
            m_LinearIntensity = Mathf.Min(m_LinearIntensity + intensity, m_LinearIntensityMaximum);
        }
        public void ShakeAngular(float intensity)
        {
            m_AngularIntensity = Mathf.Max(m_AngularIntensity + intensity, m_AngularIntensityMaximum);
        }
    }
}
