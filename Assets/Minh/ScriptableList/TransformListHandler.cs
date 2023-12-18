using UnityEngine;

namespace Minh
{
    public class TransformListHandler : MonoBehaviour
    {
        [SerializeField] private ScriptableListTransform _soapListTransform;

        // Start is called before the first frame update
        void Start()
        {
            _soapListTransform.Add(transform);
        }

        // Update is called once per frame
        void OnDestroy()
        {
            _soapListTransform.Remove(transform);
        }
    }
}