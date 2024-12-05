using UnityEngine;

namespace Game.Graphics
{
    [RequireComponent(typeof(MeshFilter), typeof(PolygonCollider2D)), ExecuteAlways]
    public class PolygonRenderer : MonoBehaviour
    {
        private MeshFilter m_MeshFilter;
        private PolygonCollider2D m_Collider;

        private void OnEnable()
        {
            m_MeshFilter = GetComponent<MeshFilter>();
            m_Collider = GetComponent<PolygonCollider2D>();
            Refresh();
        }

        public void Refresh()
        {
            m_MeshFilter.sharedMesh = m_Collider.CreateMesh(false, false);
        }
    }
}
