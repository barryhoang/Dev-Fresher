using PrimeTween;
using UnityEngine;

namespace Units
{
    public class UnitViewer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Vector3 atkDirection;
        public bool defaultDirection;

        public void ResetDirection()
        {
            spriteRenderer.flipX = defaultDirection;
        }

        public void Flip(float x)
        {
            spriteRenderer.flipX = !(x > 0);
        }

        public void StartAtkAnimation(Unit unit)
        {
            var unitTransform = unit.transform;
            Tween.Position(unitTransform, unitTransform.position + atkDirection * 0.3f, 0.5f);
        }
        public void EndAtkAnimation(Unit unit)
        {
            var unitTransform = unit.transform;
            Tween.Position(unitTransform, unitTransform.position - atkDirection * 0.3f, 0.5f);
        }
    }
}
