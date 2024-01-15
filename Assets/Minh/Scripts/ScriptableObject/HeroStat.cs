namespace Minh
{
    [UnityEngine.CreateAssetMenu(fileName = "HeroStat", menuName = "HeroStat", order = 0)]
    public class HeroStat : UnityEngine.ScriptableObject
    {
        public int _maxHealth;
        public float _speed;
        public float _attackRate;
        public float _damage;
    }
}