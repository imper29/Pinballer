using UnityEngine;

namespace Game.Logic.Inputs
{
    [CreateAssetMenu]
    public class InputGradient : ScriptableObject
    {
        [SerializeField]
        private Gradient m_Gradient;

        public Gradient Gradient
        {
            get => m_Gradient;
            set => m_Gradient = value;
        }
    }
}
