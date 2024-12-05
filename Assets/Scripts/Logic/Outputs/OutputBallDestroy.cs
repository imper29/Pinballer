using UnityEngine;

namespace Game.Logic.Outputs
{
    public class OutputBallDestroy : MonoBehaviour
    {
        public void Destroy(Ball ball)
        {
            Destroy(ball.gameObject);
        }
    }
}
