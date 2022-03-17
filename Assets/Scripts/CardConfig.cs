using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CardConfig", menuName = "Configs/CardConfig", order = 0)]
    public class CardConfig : ScriptableObject
    {
        public Material[] Diamond;
        public Material[] Heart;
        public Material[] Club;
        public Material[] Spade;
    }
}