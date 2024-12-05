using UnityEngine;

namespace Game
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private float m_SmoothTime;
        [SerializeField]
        private bool m_ResizeToFitVertical;
        [SerializeField]
        private bool m_ResizeToFitHorizontal;
        [SerializeField]
        private Rect m_Limitations;
        [SerializeField]
        private Transform m_Target;
        [SerializeField]
        private Camera m_Camera;
        
        private Vector3 m_Destination;
        private Vector3 m_Velocity;

        private void Awake()
        {
            m_Destination = transform.position;
            UpdateCameraSize();
            UpdateCameraPosition();
        }
        private void FixedUpdate()
        {
            UpdateCameraSize();
            UpdateCameraPosition();
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(m_Limitations.center, m_Limitations.size);
        }

        private void UpdateCameraSize()
        {
            //Resize camera to vertical limitations.
            if (m_ResizeToFitVertical)
                m_Camera.orthographicSize = m_Limitations.height * 0.5f;
            //Resize camera to horizontal limitations.
            if (m_ResizeToFitHorizontal)
                m_Camera.orthographicSize = m_Limitations.width / m_Camera.aspect * 0.5f;
        }
        private void UpdateCameraPosition()
        {
            //Update target position to ball position.
            if (m_Target)
                m_Destination = (Vector2)m_Target.position;
            //Update target position to respect limitations.
            Vector2 screenSizeHalf = new(m_Camera.orthographicSize * m_Camera.aspect, m_Camera.orthographicSize);
            m_Destination = Vector2.Max(Vector2.Min(m_Destination, m_Limitations.max - screenSizeHalf), m_Limitations.min + screenSizeHalf);
            //Move towards target position.
            Vector3 position = transform.position;
            Vector3 positionTarget = m_Destination;
            Vector3 positionUnclamped = Vector3.SmoothDamp(position, positionTarget, ref m_Velocity, m_SmoothTime);
            Vector3 positionClamped = Vector2.Max(Vector2.Min(positionUnclamped, m_Limitations.max - screenSizeHalf), m_Limitations.min + screenSizeHalf);
            positionClamped.z = transform.position.z;
            transform.position = positionClamped;
        }

        public void SetTarget(Ball ball)
        {
            m_Target = ball.transform;
        }
    }
}
