using UnityEngine;
using Obvious.Soap;
namespace Minh
{
    
    public class Hero : MonoBehaviour
    {
        [SerializeField] private ScriptableListHero _soapListHero;

        private void Start()
        {
            _soapListHero.Add(this);
        }
    }
    
}