using System.Collections.Generic;
using UnityEngine;

namespace Minh
{
    [CreateAssetMenu(fileName = "AllLevel", menuName = "criptableObject/AllLevel")]
    public class AllLevel : ScriptableObject
    {
        public List<LevelDetail> _levelDetail;
    }
}