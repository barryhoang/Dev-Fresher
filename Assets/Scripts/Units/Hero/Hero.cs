using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Hero : MonoBehaviour
{
    [SerializeField] private float curTime = 0;
    [SerializeField] private float receivedDmgDelay = 1;
    
    [SerializeField] private FloatVariable heroHealth;
    [SerializeField] private FloatVariable heroMaxHealth;
    [SerializeField] private FloatVariable heroSpeed;
    [SerializeField] private ScriptableListHero scriptableListHero;
    [SerializeField] private ScriptableListGameObject listHero;
    [SerializeField] private ScriptableListEnemy scriptableListEnemy;
    [SerializeField] private ScriptableEventInt onHeroDamaged;

    [SerializeField] private HeroStateMachines HSM;
    [SerializeField] private Animator animator;
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private TileBase highlightTile;
    [SerializeField] private GridManager gridManager;

    private static readonly int Hp = Animator.StringToHash("HP");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    public string Name;

    Pathfinding pathfinding;
    int currentX = 0; 
    int currentY = 0;
    int targetPosX = 0;
    int targetPosY = 0;
    

    private void Awake()
    {
        pathfinding = gameObject.GetComponent<Pathfinding>();
        scriptableListHero.Add(this);
        listHero.Add(gameObject);
        heroHealth.Value = heroMaxHealth;
        animator.SetFloat(Hp, Mathf.Abs(heroHealth.Value));
    }
    
    private void Update()
    {
        if (heroHealth <= 0)
        {
            animator.SetFloat(Hp, 0);
            Die();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (curTime <= 0 && other.CompareTag("Enemy"))
        {
            onHeroDamaged.Raise(0);
            curTime = receivedDmgDelay;
            TweenAttack();
        }
        else 
        {
            curTime -= Time.deltaTime;
        }
    }
    
    
    public void TakeDamage(int damage)
    {
        heroHealth.Add(-damage);
    }
    
    public void Die()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        scriptableListHero.Remove(this);
        HSM.currentState = HeroStateMachines.TurnState.DEAD;
    }
    
    public void Move()
    {
        if(gameObject != null)
        {
            var closest = scriptableListEnemy.GetClosest(transform.position);
            currentX = (int)transform.position.x;
            currentY = (int) transform.position.y;
            targetPosX = (int)closest.gameObject.transform.position.x;
            targetPosY = (int)closest.gameObject.transform.position.y;
            
            Debug.Log(currentX+", "+currentY+", "+targetPosX+", "+targetPosY);
            
            List<PathNode> path = pathfinding.FindPath(currentX,currentY,targetPosX,targetPosY);
            if (closest != null)
            {
                var distance = (transform.position - closest.transform.position).sqrMagnitude;
                if (distance > 1f && path != null)
                {
                    animator.SetBool(IsMoving,true);
                    
                    for (int i = 0; i < path.Count; i++)
                    {
                        targetTilemap.SetTile(new Vector3Int(path[i].xPos,path[i].yPos,0),highlightTile );
                    }
                    
                    /*var newPos = transform.position;
                    newPos =  closest.transform.position - transform.position;
                    transform.position += newPos.normalized *heroSpeed * Time.deltaTime;*/
                }

                if (distance <= 1f)
                {
                    animator.SetBool(IsMoving,false);
                }
            }
        }
    }

    public void TweenAttack()
    {
        Tween.PositionX(transform, transform.position.x+0.3f, 0.5f, Ease.Default, 2, CycleMode.Yoyo);
    }
}

