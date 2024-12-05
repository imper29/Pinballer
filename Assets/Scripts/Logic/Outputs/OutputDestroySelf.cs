using UnityEngine;

namespace Game.Logic.Outputs
{
    public class OutputDestroySelf : MonoBehaviour
    {
        public void Execute()
        {
            Destroy(gameObject);
        }
    }
}
