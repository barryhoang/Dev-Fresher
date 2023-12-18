using UnityEngine;

namespace TungTran.PickUp
{
    public class TransformListhandle : MonoBehaviour
    {
        [SerializeField] private ScriptableListTransform _listTransform;

        private void Start()
        {
            _listTransform.Add(this.transform);
        
        }

        private void OnDestroy()
        {
            _listTransform.Remove(this.transform);
        }
    }
}
