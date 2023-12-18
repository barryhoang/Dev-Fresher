using UnityEngine;

public class TransformListHandler : MonoBehaviour
{
    [SerializeField] private ScriptableListTransform _scriptableListTransform;

    private void Start()
    {
        _scriptableListTransform.Add(transform);
    }

    private void OnDestroy()
    {
        _scriptableListTransform.Remove(transform);
    }
}
