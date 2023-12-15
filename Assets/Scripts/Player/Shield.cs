using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private FloatReference _Speed;
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
        Vector3 newPosition = new Vector3(
            _playerTranform.Value.position.x + distance * Mathf.Cos(Mathf.Deg2Rad * (angle + offsetAngle)),
                _playerTranform.Value.position.y,
            _playerTranform.Value.position.z + distance * Mathf.Sin(Mathf.Deg2Rad * (angle + offsetAngle))
        );

        // Đặt vị trí mới cho hành tinh
        transform.position = newPosition;

        // Tăng góc quay dựa trên tốc độ xoay
        angle += _Speed * _speedMultipier * Time.deltaTime;

        // Đảm bảo góc không vượt quá 360 độ
        if (angle >= 360f)
        {
            angle -= 360f;
        }
    }
}
