using Game.Grapples;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Inputs
{
    public class InputGrappleGrabbed : MonoBehaviour
    {
        [SerializeField]
        internal UnityEvent<Grapple> m_GrappleGrabbed;
        [SerializeField]
        internal UnityEvent<Grapple> m_GrappleReleased;
    }
}
