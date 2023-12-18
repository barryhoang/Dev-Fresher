using Enemy;
using Obvious.Soap;
using UnityEngine;

namespace Player.Weapon.DataBullet
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private FloatReference _speed;
        [SerializeField] private FloatReference _speedMultipier;
        [SerializeField] private TransformVariable _playerTranform;
        [SerializeField] private float distance = 5f;

        private float angle = 0f;
        private float offsetAngle = 45f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                var Enemy = other.GetComponent<Enemy_1>();
                Enemy.Die();
            }
        }
        private void Update()
        {
            var position = _playerTranform.Value.position;
            Vector3 newPosition = new Vector3(
                position.x + distance * Mathf.Cos(Mathf.Deg2Rad * (angle + offsetAngle)),
                position.y,
                position.z + distance * Mathf.Sin(Mathf.Deg2Rad * (angle + offsetAngle))
            );

            // Đặt vị trí mới cho hành tinh
            transform.position = newPosition;

            // Tăng góc quay dựa trên tốc độ xoay
            angle += _speed * _speedMultipier * Time.deltaTime;

            // Đảm bảo góc không vượt quá 360 độ
            if (angle >= 360f)
            {
                angle -= 360f;
            }
        }
    }
}
