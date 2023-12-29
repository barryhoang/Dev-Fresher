using Obvious.Soap;
using UnityEngine;
using PrimeTween;
using Unity.VisualScripting;

public class Hero : MonoBehaviour
{
    [SerializeField] private float curTime = 0;
    [SerializeField] private float receivedDmgDelay = 1;
    
    [SerializeField] private FloatVariable heroHealth;
    [SerializeField] private FloatVariable heroMaxHealth;
    [SerializeField] private FloatVariable heroSpeed;
    [SerializeField] private ScriptableListHero scriptableListHero;
    [SerializeField] private ScriptableListEnemy scriptableListEnemy;
    [SerializeField] private ScriptableEventInt onHeroDamaged;

    [SerializeField] private HeroStateMachines HSM;
    [SerializeField] private Animator animator;
    [SerializeField] private Grid grid;
    [SerializeField] private GridMap gridMap;
    
    private static readonly int Hp = Animator.StringToHash("HP");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private Rigidbody2D selectedObject;
    private Vector3 mousePos;
    private Vector3 offset;



    private void Awake()
    {
        scriptableListHero.Add(this);
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
        
        SetHero();
        //SnapHero();
    }

    private void FixedUpdate()
    {
        if (selectedObject)
        {
            selectedObject.MovePosition(mousePos + offset);
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
    

    private void SetHero()
    {
        var heroPosX = transform.position.x;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePos);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePos;
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
        //Debug.Log(heroPosX);
        //Debug.Log(gridMap.CheckHeroPos(heroPosX));
        /*if (gridMap.CheckHeroPos(heroPosX))
        {
            
        }*/
        
    }

    private void SnapHero()
    {
        Vector3Int cellPos;
        cellPos = grid.WorldToCell(transform.position);
        transform.position = grid.GetCellCenterWorld(cellPos);
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
            if (closest != null)
            {
                var distance = (transform.position - closest.transform.position).sqrMagnitude;
                if (distance > 1f)
                {
                    animator.SetBool(IsMoving,true);
                    var newPos = transform.position;
                    newPos =  closest.transform.position - transform.position;
                    transform.position += newPos.normalized *heroSpeed * Time.deltaTime;
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

