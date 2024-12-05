using Game.Grapples;
using UnityEngine;

namespace Game.Logic.Outputs
{
    public class OutputGrappleBreak : MonoBehaviour
    {
        public void Execute(Grapple grapple)
        {
            grapple.BreakGrapple();
        }
    }
}
