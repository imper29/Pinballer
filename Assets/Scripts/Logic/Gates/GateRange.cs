using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Gates
{
    public class GateRange : MonoBehaviour
    {
        [SerializeField]
        private long m_Minimum;
        [SerializeField]
        private long m_Maximum;
        [SerializeField]
        private UnityEvent<long> m_Activated;

        public void Execute(long value)
        {
            if (value >= m_Minimum && value <= m_Maximum)
                m_Activated.Invoke(value);
        }
    }
}
