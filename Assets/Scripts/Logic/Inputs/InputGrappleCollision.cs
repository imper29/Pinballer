using Game.Grapples;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Inputs
{
    public class InputGrappleCollision : MonoBehaviour
    {
        [SerializeField]
        internal UnityEvent<Grapple> m_GrappleCollisionEntered;
        [SerializeField]
        internal UnityEvent<Grapple> m_GrappleCollisionExited;
    }
}
