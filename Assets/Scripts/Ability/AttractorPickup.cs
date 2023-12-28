using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Obvious.Soap;
using UnityEngine;

public class AttractorPickup : Pickup
{
    [SerializeField] private float _radius = 20f;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private TransformVariable _playerTransform;
    [SerializeField] private ScriptableListTransform _scriptableListTransform;

    protected override void OnTriggerEnter(Collider other)
    {
        Timing.RunCoroutine(Cr_Attract());
        GetComponent<Collider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private IEnumerator<float> Cr_Attract()
    {
        //find all the exp pickup in range
        var pickupsInRange = new List<Transform>(
            _scriptableListTransform.Where(x => Vector3.Distance(x.transform.position, transform.position) < _radius));
        var count = pickupsInRange.Count;
        foreach (var p in pickupsInRange)
        {
            var pickup = p.GetComponent<Pickup>();
            pickup.OnPickedup += () => { count--; };
        }
        
        //move them all to the player
        //wait until they are absorbed
        while (count > 0)
        {
            for (int i = pickupsInRange.Count -1; i >= 0; i--)
            {
                var pickup = pickupsInRange[i];
                if (pickup == null)
                    continue;
                var dir = (_playerTransform.Value.position - pickup.position).normalized;
                pickup.position += dir * _speed * Time.deltaTime;
            }
            yield return Timing.WaitForOneFrame;
        }
        //destroy self
        Destroy(gameObject);
    }
}
