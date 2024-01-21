using System;
using System.Linq;
using Obvious.Soap;
using UnityEngine;
using VHierarchy.Libs;


public class DungeonLevelGrid : MonoBehaviour
{
    [SerializeField] private ScriptableVariable<int> currentLevel;
    [SerializeField] private ScriptableEventNoParam win;
    [SerializeField] private ScriptableEventNoParam lose;
    [SerializeField] private ScriptableEventNoParam nextLevel;
    [SerializeField] private Unit enemyPrefab;
    [SerializeField] private FightingGridVariable _fightingGrid;
    [SerializeField] private ScriptableListUnit listEnemy;

    private void OnEnable()
    {
        nextLevel.OnRaised += SpawnEnemyFollowLevel;
    }

    private void Start()
    {
        InstantiateEnemy();
    }

    private void SpawnEnemyFollowLevel()
    {
        if (listEnemy.Count > 0)
        {
            foreach (var enemy in listEnemy)
            {
                _fightingGrid.Value[enemy.transform.position.ToV2Int().x, enemy.transform.position.ToV2Int().y] = null;
                enemy.gameObject.Destroy();
            }
            
            Debug.Log(listEnemy.Count);
            listEnemy.RemoveRange(0, listEnemy.Count);
        }

        for (int i = 0; i < currentLevel.Value; i++)
        {
            InstantiateEnemy();
        }
    }

    private void InstantiateEnemy()
    {
        Vector2Int pos = ValidPosition();
        Unit unit = Instantiate(enemyPrefab, new Vector3(pos.x, pos.y), Quaternion.identity);
        listEnemy.Add(unit);
    }

    private Vector2Int ValidPosition()
    {
        System.Random random = new System.Random();
        Vector2Int randomVector;
        do
        {
            randomVector = new Vector2Int(random.Next(9, 15), random.Next(0, 6));
        } while (_fightingGrid.Value[randomVector.x, randomVector.y] != null);

        return randomVector;
    }

    private void OnDisable()
    {
        nextLevel.OnRaised -= SpawnEnemyFollowLevel;
    }
}