using Game.Logic.Inputs;
using System.Collections;
using UnityEngine;

namespace Game.Logic.Outputs
{
    [RequireComponent(typeof(MeshRenderer))]
    public class OutputGradientMeshPulse : MonoBehaviour
    {
        [SerializeField]
        private float m_Duration;
        [SerializeField]
        private AnimationCurve m_Interpolation;
        [SerializeField]
        private InputGradient m_Gradient;

        private Material m_Renderer;
        private Coroutine m_Coroutine;

        public void Pulse()
        {
            if (m_Coroutine != null)
                StopCoroutine(m_Coroutine);
            m_Coroutine = StartCoroutine(PulseCoroutine());
        }
        private IEnumerator PulseCoroutine()
        {
            var gradient = m_Gradient.Gradient;
            var remaining = m_Duration;
            while (remaining > 0f)
            {
                m_Renderer.color = gradient.Evaluate(m_Interpolation.Evaluate(1f - remaining / m_Duration));
                remaining -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            m_Renderer.color = gradient.Evaluate(m_Interpolation.Evaluate(1f));
        }

        private void Start()
        {
            var mesh = GetComponent<MeshRenderer>();
            mesh.material = new(mesh.sharedMaterial);
            m_Renderer = mesh.material;
            m_Renderer.color = m_Gradient.Gradient.Evaluate(1f);
        }
    }
}
