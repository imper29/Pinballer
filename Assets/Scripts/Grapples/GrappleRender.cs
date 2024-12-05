using UnityEngine;

namespace Game.Grapples
{
    [RequireComponent(typeof(Grapple))]
    public class GrappleRender : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer m_Renderer;
        [SerializeField, HideInInspector]
        private Grapple m_Grapple;

        private void Update()
        {
            m_Renderer.positionCount = m_Grapple.Vertices.Count;
            var index = 0;
            var current = m_Grapple.Vertices.First;
            while (current != null)
            {
                m_Renderer.SetPosition(index++, current.Value.Position);
                current = current.Next;
            }
        }
        private void OnValidate()
        {
            m_Grapple = GetComponent<Grapple>();
        }
    }
}
