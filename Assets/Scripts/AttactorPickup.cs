using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;

public class AttactorPickup : Pickup
{
    [SerializeField] private float _radius = 20f;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private TransformVariable _playerTransform;
    [SerializeField] private ScriptableListTransform _scriptableListTransform;

    protected void OntriggerEnter(Collider other)
    {
        StartCoroutine(Cr_Attract());
        GetComponent<Collider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
    private IEnumerator Cr_Attract()
    {
        var pickupsInRange = new List<Transform>(
            _scriptableListTransform.Where(x=>Vector3.Distance(x.transform.position,transform.position)<_radius));
        var count = pickupsInRange.Count;
        foreach (var p in pickupsInRange)
        {
            var pickup = p.GetComponent<Pickup>();
            pickup.OnPickedup += () => { count--; };
        }

        while (count>0)
        {
            for (int i = pickupsInRange.Count-1; i >=0; i--)
            {
                var pickup = pickupsInRange[i];
                if(pickup==null)
                    continue;
                var dir = (_playerTransform.Value.position - pickup.position).normalized;
                pickup.position += dir * _speed * Time.deltaTime;
            }

            yield return null;
        }
        Destroy(this);
    }


}
