using UnityEngine;

namespace Tung
{
    public class VfxUnitViewer : MonoBehaviour
    {
        [SerializeField] private GameObject vfxPunch;

        public void SetVfxPunch(Vector2 dir)
        {
            float zAngle = CalculateZAngle(dir);
            vfxPunch.transform.rotation = Quaternion.Euler(0,0,zAngle);
            vfxPunch.transform.position = (Vector2) transform.position +  dir * 0.6f;
            vfxPunch.SetActive(true);
        }
        private float CalculateZAngle(Vector2 direction)
        {
           
            float angleRad = Mathf.Atan2(direction.y, direction.x);
            float angleDeg = angleRad * Mathf.Rad2Deg;
            return angleDeg;
        }
    }
}
