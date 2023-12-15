using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Obvious.Soap;
using UnityEngine;
using System;
using Random = System.Random;

namespace Player
{
    public class AtractorPickUp : PickUp
    {
        [SerializeField] private float _radius = 100f;
        [SerializeField] private float _speed = 20f;
        [SerializeField] private TransformVariable _playerTransform;
        [SerializeField] private ScriptableListTransform _scriptableListTransform;

        public override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            StartCoroutine(Cr_Attract());
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private IEnumerator Cr_Attract()
        {
            //find all exp pickups in range
            var pickupsInRange = new List<Transform>(_scriptableListTransform.Where(x =>
                Vector3.Distance(x.transform.position, transform.position) < _radius));

            var count = pickupsInRange.Count;
            foreach (var pickup in pickupsInRange.Select(p => p.GetComponent<PickUp>()))
            {
                pickup.OnPickUp += () => { count--; };
            }

            //move them all to the player
            //wait until they are absorbed
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

                yield return null;

            }
            // destroy self
            Destroy(gameObject);
        }
    }
}
