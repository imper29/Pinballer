using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Gates
{
    public class GateCooldown : MonoBehaviour
    {
        [SerializeField]
        private float m_Cooldown;
        [SerializeField]
        private UnityEvent m_Activated;

        private float m_CooldownTimeNow;

        public void Execute()
        {
            if (m_CooldownTimeNow <= Time.time)
            {
                m_CooldownTimeNow = Time.time + m_Cooldown;
                m_Activated.Invoke();
            }
        }
    }
}
