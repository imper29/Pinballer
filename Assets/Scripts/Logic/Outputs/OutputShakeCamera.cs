using UnityEngine;

namespace Game.Logic.Behaviours
{
    public class OutputShakeCamera : MonoBehaviour
    {
        [SerializeField]
        private float m_InputMin, m_InputMax;
        [SerializeField]
        private AnimationCurve m_IntensityLinear;
        [SerializeField]
        private AnimationCurve m_IntensityAngular;

        public void Execute(float input)
        {
            var camera = FindObjectOfType<CameraShaker>();
            if (camera)
            {
                var lerp = Mathf.InverseLerp(m_InputMin, m_InputMax, Mathf.Clamp(input, m_InputMin, m_InputMax));
                var intensityLinear = m_IntensityLinear.Evaluate(lerp);
                var intensityAngular = m_IntensityAngular.Evaluate(lerp);

                camera.ShakeLinear(intensityLinear);
                camera.ShakeAngular(intensityAngular);
            }
        }
    }
}
