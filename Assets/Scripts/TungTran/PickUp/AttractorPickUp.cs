using System.Collections.Generic;
using System.Linq;
using MEC;
using Obvious.Soap;
using TungTran.Enemy;
using UnityEngine;

namespace TungTran.PickUp
{
    public class AttractorPickUp : PickUp
    {
        [SerializeField] private float _radius = 100f;
        [SerializeField] private float _speed = 20f;
        [SerializeField] private TransformVariable _playerTransform;
        [SerializeField] private ScriptableListTransform _scriptableListTransform;
        
        public override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Timing.RunCoroutine(Cr_Attract().CancelWith(gameObject));
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private IEnumerator<float> Cr_Attract()
        {
            var pickupsInRange = new List<Transform>(_scriptableListTransform.Where(x =>
                Vector3.Distance(x.transform.position, transform.position) < _radius));
            var count = pickupsInRange.Count;
            foreach (var pickup in pickupsInRange.Select(p => p.GetComponent<global::TungTran.PickUp.PickUp>()))
            {
           
                pickup.OnPickUp += () => { count--; };
            }
            while (count > 0)
            {
                for (int i = pickupsInRange.Count - 1; i >= 0; i--)
                {
                    var pickup = pickupsInRange[i];
                    if (pickup != null)
                    {
                        var position = pickup.position;
                        var dir = (_playerTransform.Value.position - position).normalized;
                        position += dir * (_speed * Time.deltaTime);
                        pickup.position = position;
                    }
                }

                yield return 0;

            }
            Destroy(gameObject);
        }
    }
}
