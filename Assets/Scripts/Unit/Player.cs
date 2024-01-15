using Obvious.Soap;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ScriptableListGameObject _scriptableListPlayer;
    
    private void Start()
    {
        _scriptableListPlayer.Add(gameObject);
    }
}
