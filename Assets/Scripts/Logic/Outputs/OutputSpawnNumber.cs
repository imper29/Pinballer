using TMPro;
using UnityEngine;

namespace Game.Logic.Outputs
{
    public class OutputSpawnNumber : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_Offset;
        [SerializeField]
        private TextMeshPro m_Prefab;
        [SerializeField]
        private Gradient m_Gradient;
        [SerializeField]
        private int m_GradientMinimum, m_GradientMaximum;

        public void Spawn(long number)
        {
            float sample = Mathf.InverseLerp(m_GradientMinimum, m_GradientMaximum, Mathf.Clamp(number, m_GradientMinimum, m_GradientMaximum));
            TextMeshPro text = Instantiate(m_Prefab, transform.position + m_Offset, Quaternion.identity);
            text.color = m_Gradient.Evaluate(sample);
            text.text = string.Format(text.text, number);
        }
    }
}
