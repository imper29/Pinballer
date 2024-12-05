using UnityEngine;

namespace Game.Grapples
{
    public class GrappleObstacle : MonoBehaviour
    {
        [SerializeField]
        private bool m_IsGrabbable = true;
        [SerializeField]
        private bool m_IsCollidable = true;

        /// <summary>
        /// Determines if the obstacle is grabbable.
        /// </summary>
        public bool IsGrabbable
        {
            get => m_IsGrabbable;
            set => m_IsGrabbable = value;
        }
        /// <summary>
        /// Determines if the obstacle is collidable.
        /// </summary>
        public bool IsCollidable
        {
            get => m_IsCollidable;
            set => m_IsCollidable = value;
        }
    }
}
