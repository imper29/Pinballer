using System.Collections.Generic;
using UnityEngine;

namespace Game.Grapples
{
    public class Grapple : MonoBehaviour
    {
        private const string OBSTACLE_MASK = "Obstacle";
        private const float SEGMENT_MINIMUM_BEND_LENGTH = 0.1f;

        [SerializeField]
        private Rigidbody2D m_Rigidbody;
        [SerializeField]
        private string m_Input;
        [SerializeField, Min(0)]
        private float m_Strength;
        [SerializeField, Min(0)]
        private float m_LengthSeeking;
        [SerializeField, Min(0)]
        private float m_LengthResting;
        [SerializeField, Min(0)]
        private float m_LengthBreaking;
        [SerializeField]
        private float m_BreakingStunTime;
        [SerializeField]
        private bool m_DynamicRestingLength;

        /// <summary>
        /// Determines if the grapple is active.
        /// </summary>
        public bool Active
        {
            get => m_Vertices.Count >= 2;
        }
        /// <summary>
        /// Determines the current length of the grapple.
        /// </summary>
        public float Length
        {
            get
            {
                var length = 0f;
                var current = m_Vertices.First;
                while (current != null && current.Next != null)
                {
                    length += Vector2.Distance(current.Value.Position, current.Next.Value.Position);
                    current = current.Next;
                }
                return length;
            }
        }
        /// <summary>
        /// Determines the vertices.
        /// </summary>
        public LinkedList<Vertex> Vertices
        {
            get => m_Vertices;
        }

        private readonly LinkedList<Vertex> m_Vertices = new();
        private float m_StunTimeEnd = float.NegativeInfinity;

        private void Update()
        {
            ProcessInputs();
        }
        private void FixedUpdate()
        {
            SimplifyGeometry();
            ComplexifyGeometry();

            var length = Length;
            if (length >= m_LengthResting)
                ApplyForces((length - m_LengthResting) * m_Strength);
            if (length >= m_LengthBreaking)
                BreakGrapple();
        }
        private void OnDrawGizmos()
        {
            //Draw grapple stats.
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, m_LengthSeeking);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_LengthResting);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_LengthBreaking);
            //Draw grapple line.
            var length = Length;
            if (length >= m_LengthResting)
                Gizmos.color = Color.Lerp(Color.green, Color.red, Mathf.InverseLerp(m_LengthResting, m_LengthBreaking, length));
            else
                Gizmos.color = Color.Lerp(Color.white, Color.green, m_LengthResting / length);
            var current = m_Vertices.First;
            while (current != null && current.Next != null)
            {
                Gizmos.DrawLine(current.Value.Position, current.Next.Value.Position);
                current = current.Next;
            }
        }

        private void ApplyForces(float tension)
        {
            var curr = m_Vertices.First;
            while (curr != null)
            {
                //Process all nodes with bodies.
                var body = curr.Value.Rigidbody;
                if (body)
                {
                    var next = curr.Next;
                    var prev = curr.Previous;
                    if (prev == null)
                    {
                        //First vertex.
                        var direction = (next.Value.Position - curr.Value.Position).normalized;
                        body.AddForceAtPosition(direction * tension, curr.Value.Position);
                    }
                    else if (next == null)
                    {
                        //Last vertex.
                        var direction = (prev.Value.Position - curr.Value.Position).normalized;
                        body.AddForceAtPosition(direction * tension, curr.Value.Position);
                    }
                    else
                    {
                        //Middle vertex.
                        var tangent = (prev.Value.Position - next.Value.Position).normalized;
                        var direction = new Vector2(-tangent.y, tangent.x);
                        body.AddForceAtPosition(direction * tension, curr.Value.Position);
                    }
                }
                curr = curr.Next;
            }
        }
        private void SimplifyGeometry()
        {
            if (m_Vertices.Count != 0)
            {
                if (!m_Vertices.Last.Value.Parent)
                {
                    BreakGrapple();
                    return;
                }
            }

            var current = m_Vertices.First;
            if (current != null)
            {
                current = current.Next;
                while (current != null && current.Next != null)
                {
                    if (current.Value.Parent)
                    {
                        var lineStart = current.Previous.Value.Position;
                        var lineEnd = current.Next.Value.Position;
                        var lineDir = (lineEnd - lineStart).normalized * SEGMENT_MINIMUM_BEND_LENGTH;
                        var remove = current;
                        current = current.Next;
                        if (!Physics2D.Linecast(lineStart + lineDir, lineEnd - lineDir, LayerMask.GetMask(OBSTACLE_MASK)))
                        {
                            m_Vertices.Remove(remove);
                            var collisionResponse = remove.Value.Parent.GetComponent<Logic.Inputs.InputGrappleCollision>();
                            if (collisionResponse)
                                collisionResponse.m_GrappleCollisionExited.Invoke(this);
                        }
                    }
                    else
                    {
                        var remove = current;
                        current = current.Next;
                        m_Vertices.Remove(remove);
                    }
                }
            }
        }
        private void ComplexifyGeometry()
        {
            var current = m_Vertices.First;
            while(current != null && current.Next != null)
            {
                var lineStart = current.Value.Position;
                var lineEnd = current.Next.Value.Position;
                var hits = Physics2D.LinecastAll(lineStart, lineEnd, LayerMask.GetMask(OBSTACLE_MASK));
                foreach (var hit in hits)
                {
                    //Ignore contacts that are very close to line start and end.
                    if (Vector2.Distance(hit.point, lineStart) < SEGMENT_MINIMUM_BEND_LENGTH)
                        continue;
                    if (Vector2.Distance(hit.point, lineEnd) < SEGMENT_MINIMUM_BEND_LENGTH)
                        continue;
                    //Ensure contact actually hit an obstacle.
                    var obstacle = hit.collider.GetComponent<GrappleObstacle>();
                    if (obstacle)
                    {
                        //Grapple touches a collider so it should bend around the collider.
                        if (obstacle.IsCollidable)
                        {
                            m_Vertices.AddAfter(current, new LinkedListNode<Vertex>(new(hit.collider.transform, hit.collider.transform.InverseTransformPoint(hit.point))));
                            //There is a collision response that must be handled.
                            var collisionResponse = hit.collider.GetComponent<Logic.Inputs.InputGrappleCollision>();
                            if (collisionResponse)
                                collisionResponse.m_GrappleCollisionEntered.Invoke(this);
                        }
                    }
                }
                current = current.Next;
            }
        }
        private void ProcessInputs()
        {
            if (!Active && Input.GetButton(m_Input))
                StartGrapple();
            if (Active && !Input.GetButton(m_Input))
                StopGrapple();
        }
        private void StartGrapple()
        {
            //Ignore if still stunned.
            if (Time.time <= m_StunTimeEnd)
                return;

            Vector2 lineStart = transform.position;
            Vector2 lineDir = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - lineStart).normalized;
            Vector2 lineEnd = lineStart + lineDir * m_LengthSeeking;
            var hit = Physics2D.Linecast(lineStart, lineEnd, LayerMask.GetMask(OBSTACLE_MASK));
            if (hit)
            {
                var obstacle = hit.collider.GetComponent<GrappleObstacle>();
                if (obstacle && obstacle.IsGrabbable)
                {
                    m_Vertices.Clear();
                    m_Vertices.AddFirst(new Vertex(m_Rigidbody.transform, Vector2.zero));
                    m_Vertices.AddLast(new Vertex(obstacle.transform, obstacle.transform.InverseTransformPoint(hit.point)));
                    if (m_DynamicRestingLength)
                        m_LengthResting = (m_Vertices.First.Value.Position - m_Vertices.Last.Value.Position).magnitude;
                    var input = obstacle.GetComponent<Logic.Inputs.InputGrappleGrabbed>();
                    if (input)
                        input.m_GrappleGrabbed.Invoke(this);
                }
            }
        }

        public void StopGrapple()
        {
            if (Active)
            {
                var input = m_Vertices.First.Value.Parent.GetComponent<Logic.Inputs.InputGrappleGrabbed>();
                m_Vertices.Clear();
                if (input)
                    input.m_GrappleReleased.Invoke(this);
            }
        }
        public void BreakGrapple()
        {
            m_StunTimeEnd = Time.time + m_BreakingStunTime;
            StopGrapple();
        }

        public struct Vertex
        {
            /// <summary>
            /// Constructs the vertex.
            /// </summary>
            /// <param name="parent">The parent.</param>
            /// <param name="anchor">The anchor.</param>
            public Vertex(Transform parent, Vector3 anchor)
            {
                Parent = parent;
                Anchor = anchor;
            }

            /// <summary>
            /// Determines the parent.
            /// </summary>
            public Transform Parent
            {
                get;
            }
            /// <summary>
            /// Determines the rigidbody.
            /// </summary>
            public Rigidbody2D Rigidbody
            {
                get => Parent.GetComponent<Collider2D>().attachedRigidbody;
            }
            /// <summary>
            /// Determines the anchor.
            /// </summary>
            public Vector3 Anchor
            {
                get;
            }
            /// <summary>
            /// Determines the position.
            /// </summary>
            public Vector3 Position
            {
                get => Parent.TransformPoint(Anchor);
            }
        }
    }
}
