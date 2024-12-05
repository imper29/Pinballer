using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Gates
{
    public class GateDelay : MonoBehaviour
    {
        [SerializeField]
        private float m_Delay;
        [SerializeField]
        private UnityEvent m_Activated;

        public void Execute()
        {
            StartCoroutine(ExecuteCoroutine(m_Delay));
        }

        private IEnumerator ExecuteCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            m_Activated.Invoke();
        }
    }
}
