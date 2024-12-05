using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BehaviourLinearMovement : MonoBehaviour
{
    [SerializeField]
    private Transform m_PointA, m_PointB;
    [SerializeField]
    private AnimationCurve m_Interpolation;
    
    Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        m_Rigidbody.MovePosition(Vector3.Lerp(m_PointA.position, m_PointB.position, m_Interpolation.Evaluate(Time.fixedTime)));
    }

    private void OnDrawGizmos()
    {
        if (m_PointA)
            Gizmos.DrawWireSphere(m_PointA.position, 0.1f);
        if (m_PointB)
            Gizmos.DrawWireSphere(m_PointB.position, 0.1f);
        if (m_PointA && m_PointB)
            Gizmos.DrawLine(m_PointA.position, m_PointB.position);
    }
}
