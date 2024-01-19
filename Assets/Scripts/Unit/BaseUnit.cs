using System;
using System.Collections.Generic;
using Map;
using MEC;
using Obvious.Soap;
using UnityEngine;

namespace Unit
{ 
    public class BaseUnit : MonoBehaviour 
        //Unit: thuộc tính và function chung. Ví dụ: Stats, Init, Move, Attack, TakeDamage, Die...
    {
        [SerializeField] private FloatVariable currentHealth;
        [SerializeField] private FloatVariable maxHealth;
        [SerializeField] private FloatVariable atkSpd;
        [SerializeField] private FloatVariable dmg;
        [SerializeField] private FloatVariable spd;
        [SerializeField] private HealthViewer healthViewer;
        [SerializeField] private UnitViewer unitViewer;
        [SerializeField] private ScriptableEventNoParam onDead;
        [SerializeField] private GridMapVariable gridMapVariable;
        [SerializeField] private Pathfinding pathfinding;

        private List<PathNode> _pathNodes;
        
        public static BaseUnit Target;
        public Vector2Int unitGridPosition;
        public bool attacking;
        public bool moving;

        private void OnEnable()
        {
            currentHealth.Value = maxHealth;
            var unitPos = transform.position;
            unitGridPosition = new Vector2Int((int)unitPos.x,(int)unitPos.y);
            unitViewer.SetAnimation(UnitViewer.AnimationState.Idle);
            healthViewer.SetMaxHealth(currentHealth);
            onDead.OnRaised += Die;
            gridMapVariable.Init();
        }

        private void OnDisable()
        {
            onDead.OnRaised -= Die;
        }

        private void Die()
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            //gameObject.SetActive(false);
        }

        private void TakeDamage(float damage)
        {
            currentHealth.Value -= damage;
            healthViewer.UpdateHealthBar(currentHealth);
            if (currentHealth.Value <= 0)
            {
                currentHealth.Value = 0;
                onDead.Raise();
            }
        }

        public IEnumerator<float> Move(BaseUnit targetPos)
        {
            if (Target == null) moving = false;
            Target = targetPos;
            if (moving
                && CheckDistanceCell(transform.position, Target.transform.position))
            {
                unitViewer.SetAnimation(UnitViewer.AnimationState.Idle);
                moving = false;
                attacking = true;
                Timing.RunCoroutine(Attack().CancelWith(gameObject));
            }

            if (moving)
            {
                unitViewer.SetAnimation(UnitViewer.AnimationState.Move);
                var unitPos = transform.position;
                var startPos = new Vector2Int((int) unitPos.x, (int) unitPos.y);
                //var endPos = DistanceDir(Target.unitGridPosition);
                var endPos = new Vector2Int((int) Target.transform.position.x,
                    (int) Target.transform.position.y);
                _pathNodes = pathfinding.FindPath(startPos.x, startPos.y,
                    endPos.x, endPos.y);
                var targetNodePosition = new Vector2Int(_pathNodes[0].xPos, _pathNodes[0].yPos);
                gridMapVariable.Value[startPos.x, startPos.y] = null;
                gridMapVariable.Value[targetNodePosition.x, targetNodePosition.y] = this;
                unitGridPosition = targetNodePosition;
                while (_pathNodes != null && _pathNodes.Count > 0)
                {
                    var position = transform.position;
                    var x = Target.transform.position.x - position.x;
                    unitViewer.SetFlip(x);
                    position = Vector2.MoveTowards(position, targetNodePosition,
                        spd.Value * Time.deltaTime);
                    transform.position = position;

                    if ((Vector2) transform.position == targetNodePosition)
                    {
                        if (CheckDistanceCell(targetNodePosition, Target.unitGridPosition))
                        {
                            unitViewer.SetAnimation(UnitViewer.AnimationState.Idle);
                            moving = false;
                            attacking = true;
                            Timing.RunCoroutine(Attack().CancelWith(gameObject));
                            break;
                        }

                        unitPos = transform.position;
                        startPos = new Vector2Int((int)unitPos.x,(int)unitPos.y);
                        //endPos = DistanceDir(Target.unitGridPosition);
                        endPos = new Vector2Int((int) Target.transform.position.x,
                            (int) Target.transform.position.y);
                        _pathNodes = pathfinding.FindPath(startPos.x, startPos.y,
                            endPos.x, endPos.y);
                        targetNodePosition = new Vector2Int(_pathNodes[0].xPos, _pathNodes[0].yPos);
                        gridMapVariable.Value[startPos.x, startPos.y] = null;
                        gridMapVariable.Value[targetNodePosition.x, targetNodePosition.y] = this;
                        unitGridPosition = targetNodePosition;
                    }

                    yield return Timing.WaitForOneFrame;
                }
            }
        }

        private IEnumerator<float> Attack()
        {
            Timing.WaitUntilDone(Timing.RunCoroutine(CheckDistance().CancelWith(gameObject)));
            while (attacking)
            {
                if (!Target.gameObject.activeInHierarchy)
                {
                    attacking = false;
                    break;
                }

                var direction = (Target.transform.position - transform.position).normalized;
                unitViewer.direction = direction;
                unitViewer.StartAttackingAnimation(this);
                yield return Timing.WaitForSeconds(1f);
                Target.TakeDamage(dmg);
                unitViewer.EndAttackingAnimation(this);
                yield return Timing.WaitForSeconds(atkSpd);
            }
        }

        private IEnumerator<float> CheckDistance()
        {
            while (Vector2.Distance(transform.position,Target.transform.position)>=1f)
            {
                yield return Timing.WaitForOneFrame;
            }
        }
        
        private Vector2Int DistanceDir(Vector2Int unitTarget)
        {
            var unitPos = transform.position;
            var start = new Vector2Int((int)unitPos.x,(int)unitPos.y);
            var value = Vector2Int.zero;
            var minDistance = float.MaxValue;
            Vector2Int[] listDir = {unitTarget + Vector2Int.down,unitTarget + Vector2Int.left,
                unitTarget+ Vector2Int.right ,unitTarget + Vector2Int.up};
            foreach (var dir in listDir)
            {
                var distance = Vector2Int.Distance(start,dir);
                if (distance < minDistance && gridMapVariable.Value[dir.x, dir.y] == null)
                {
                    minDistance = distance;
                    value = dir;
                }
            }
            return value;
        }
        
        private static bool CheckDistanceCell(Vector2 pos ,Vector2 posTarget)
        {
            
            var distance = (int) (Math.Abs(pos.x - posTarget.x) + Math.Abs(pos.y - posTarget.y));
            return Math.Abs(distance) <= 1f;
        }
        
        private static bool CheckDistanceCell(Vector2Int pos ,Vector2Int posTarget)
        {
            var distance = Math.Abs(pos.x - posTarget.x) + Math.Abs(pos.y - posTarget.y);
            return Math.Abs(distance) <= 1f;
        }
    }
}
