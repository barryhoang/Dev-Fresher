using System.Collections.Generic;
using System.Linq;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Minh
{
    public class AttractorPickup : Pickup
    {
        [SerializeField] private float _radius = 20f;
        [SerializeField] private float _speed = 20f;
        [SerializeField] private TransformVariable _playerTransform;
        [SerializeField] private ScriptableListTransform _scriptableListTransform;

        protected override void OnTriggerEnter(Collider other)
        {
            Timing.RunCoroutine(Attract());
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private IEnumerator<float> Attract()
        {
            //find all exp pickups in range
            var pickupsInrange = new List<Transform>(
                _scriptableListTransform.Where(
                    x => Vector3.Distance(x.transform.position, transform.position) < _radius));
            var count = pickupsInrange.Count;
            //move them all to the player
            foreach (var p in pickupsInrange)
            {
                var pickup = p.GetComponent<Pickup>();
                pickup.OnPickedup += () => { count--; };
            }

            //wait until they are absorbed
            while (count > 0)
            {
                for (int i = pickupsInrange.Count - 1; i >= 0; i--)
                {
                    var pickup = pickupsInrange[i];
                    if (pickup == null)
                    {
                        continue;
                    }

                    var position = pickup.position;
                    var dir = (_playerTransform.Value.position - position).normalized;
                    position += dir * _speed * Time.deltaTime;
                    pickup.position = position;
                }

                yield return Timing.WaitForOneFrame;
            }

            //destroy self
            Destroy(this);
        }
    }
}