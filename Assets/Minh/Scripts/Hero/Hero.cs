using System;
using System.Collections.Generic;
using MEC;
using Obvious.Soap;
using UnityEngine;
using PrimeTween;
namespace Minh
{
    
    public class Hero : MonoBehaviour
    {
        [SerializeField] public HeroStat _heroStats;
        [SerializeField] public FightingGrid _fightingGrid;
        [SerializeField] public FightingMapVariable _fightingMapVariable;
        [SerializeField] public int currentX = 0;
        [SerializeField] public int currentY = 0;
        public Vector2Int prevPosition;
        [SerializeField] public TweenSettings _tweenSettings;
        public ScriptableListEnemy _soapListEnemy;
        public ScriptableListPlayer _soapListPlayer;
        private int _health;
        private Hero closet;

        private void Awake()
        {
            _fightingGrid = GameObject.Find("FightingManager").GetComponent<FightingGrid>();
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
           
        }
        public  IEnumerator<float> Move()
        {
            closet = CheckClosest();
           Debug.Log("move");
            List<PathNode> path = new List<PathNode>();
            while (true)
            {
                //_targetTilemap.ClearAllTiles();
                // Debug.Log(_characterState + "ENEMY STATE");
                closet = CheckClosest();
                currentX = (int) this.transform.position.x;
                currentY = (int) this.transform.position.y;
                if (closet != null)
                {
                    if (closet.transform.position.x - this.transform.position.x < 0)
                    {
                        this.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        this.transform.localScale = new Vector3(1, 1, 1);
                    }
                    Debug.Log((int) closet.transform.position.x+""+(int) closet.transform.position.y);
                    Debug.Log(currentX+"  "+currentY);
                    path = _fightingGrid.GetPath(currentX, currentY, 9,
                        5);

                    if (Vector2.Distance(closet.transform.position, transform.position) > 1.8f)
                    {
                        //_animator.SetBool("EnemyRun", true);
                        _fightingMapVariable.Value[prevPosition.x, prevPosition.y] = null;
                        _fightingMapVariable.Value[path[0].xPos, path[0].yPos] = this;
                        Tween.Position(transform, new Vector3(path[0].xPos, path[0].yPos, 0), _tweenSettings);
                        prevPosition = transform.position.ToV2Int();
                         yield return Timing.WaitForSeconds(_tweenSettings.duration);
                    }
                    else
                    {
                        // _animator.SetBool("EnemyRun", false);
                        // Timing.RunCoroutine(EnemyAttack(closet).CancelWith(closet.gameObject),
                        //     "enemyAttack" + _gameObjectID);

                        yield return Timing.WaitForSeconds(1 / (float) _heroStats._attackRate * 2);
                    }
                }
            }

        }

        private Hero CheckClosest()
        {
            var hero=new Hero();
            if (this.CompareTag("Player"))
            {
                Debug.Log("PLAYER MOVE");
                hero=_soapListEnemy.GetClosest(transform.position);
            }
            else if (this.CompareTag("Enemy"))
            {
                hero =_soapListPlayer.GetClosest(transform.position);
            }

            return hero;
        }
        private IEnumerator<float> CheckHealth()
        {
            while (true)
            {
                if (_health <= 0)
                {
                    var position = transform.position.ToV2Int();
                    _fightingMapVariable.Value[position.x, position.y] = null;
                    gameObject.SetActive(false);
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
    
}