namespace Minh
{
    [UnityEngine.CreateAssetMenu(fileName = "HeroStat", menuName = "HeroStat", order = 0)]
    public class HeroStat : UnityEngine.ScriptableObject
    {
        private int _maxHealth;
        private float _speed;
        private float _attackRate;
        private float _damage;
    }
}