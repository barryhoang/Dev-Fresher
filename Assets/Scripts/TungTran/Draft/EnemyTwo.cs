using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace TungTran.Draft
{
    public class EnemyTwo : MonoBehaviour
    {
        public void PushOnGameObject(Vector3 forward)
        {
            transform.position += forward * (3 * Time.deltaTime);
        }

        IEnumerator<float> Shot(float time, string text)
        {
            yield return Timing.WaitForSeconds(time);
 
            Debug.Log(text);
        }
    }
}
