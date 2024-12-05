using Game.Logic.Inputs;
using UnityEngine;

public class OutputUpdateGradient : MonoBehaviour
{
    [SerializeField]
    private long m_RepeatDuration;
    [SerializeField]
    private Source[] m_Sources;
    [SerializeField]
    private InputGradient m_Target;

    private void Awake()
    {
        m_Target.Gradient = m_Sources[0].m_Gradient.Gradient;
    }
    public void Execute(long value)
    {
        long total = 0;
        for (int i =0; i < m_Sources.Length; ++i)
        {
            total += m_Sources[i].m_Duration;
            if (total >= value)
            {
                m_Target.Gradient = m_Sources[i].m_Gradient.Gradient;
                return;
            }
        }
        value %= total;
        total = 0;
        for (int i = 0; i < m_Sources.Length; ++i)
        {
            total += m_Sources[i].m_Duration;
            if (total >= value)
            {
                m_Target.Gradient = m_Sources[i].m_Gradient.Gradient;
                return;
            }
        }
    }

    [System.Serializable]
    public struct Source
    {
        public InputGradient m_Gradient;
        public long m_Duration;
    }
}
