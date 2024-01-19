using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Tung
{
    public class AniImage : MonoBehaviour
    {
        public Image a;
        private void Start()
        {
            Tween.Alpha(a, 0f, 1f);
        }
    }
}
