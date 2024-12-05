using UnityEngine;

namespace Game.Logic.Behaviours
{
    public class BehaviourContactSpawner : MonoBehaviour
    {
        [SerializeField]
        private float m_VelocityThreshold;
        [SerializeField]
        private GameObject m_Prefab;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var speed = collision.relativeVelocity.magnitude;
            if (speed > m_VelocityThreshold)
                foreach (var contact in collision.contacts)
                    Instantiate(m_Prefab, contact.point, Quaternion.FromToRotation(Vector2.up, contact.normal));
        }
    }
}
