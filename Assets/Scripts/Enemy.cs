using UnityEngine;
using Obvious.Soap;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;

    private void Start()
    {
        _scriptableListEnemy.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(10);
            Die();
        }
    }

    public void Die()
    {
        _scriptableListEnemy.Remove(this);
        Destroy(gameObject);
    }
}
