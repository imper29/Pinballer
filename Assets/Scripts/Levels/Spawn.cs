using UnityEngine;

namespace Game.Levels
{
    [AddComponentMenu("Game/Level/Spawn")]
    public class Spawn : MonoBehaviour
    {
        [SerializeField]
        internal Ball m_Prefab;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}
