using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private MapVariable mapVariable;

    private void Start()
    {
        mapVariable.Init();
    } 
}
