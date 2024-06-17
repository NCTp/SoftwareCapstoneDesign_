using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyWorm
{
    public interface IEnemyBossWormState
    {
        void Handle(EnemyBossWorm enemyBossWorm);
        void DeleteController();
    }
    public class EnemyBossWorm : Enemy
    {
        public GameObject[] bodies;
        public float speed = 30.0f;
        public int rotationNumber = 4;
        public float idleTime = 5.0f;
        public float CurrentSpeed {get; set;}
        public float CurrentRotationNumber { get; set; }
        public float CurrentIdleTimer { get; set; }
    
        private EnemyBossWormStateContext _enemyBossWormStateContext;
        private IEnemyBossWormState _idleState, _moveState, _attackState;
    
        // Start is called before the first frame update
        void Start()
        {
            _enemyBossWormStateContext = new EnemyBossWormStateContext(this);
            _idleState = gameObject.AddComponent<EnemyBossWormIdleState>();
            _moveState = gameObject.AddComponent<EnemyBossWormMoveState>();
            _attackState = gameObject.AddComponent<EnemyBossWormAttackState>();
        
            CurrentSpeed = speed;
            CurrentRotationNumber = rotationNumber;
            CurrentIdleTimer = 0.0f;
            _enemyBossWormStateContext.Transition(_moveState);
        }
        // Update is called once per frame
        void Update()
        {
            /*
            foreach (GameObject body in bodies)
            {
                body.transform.LookAt(_target.transform.position);
            }
            */
        
        }
        public void IdleWorm()
        {
            _enemyBossWormStateContext.Transition(_idleState);
        }

        public void MoveWorm()
        {
            _enemyBossWormStateContext.Transition(_moveState);
        }

        public void AttackWorm()
        {
            _enemyBossWormStateContext.Transition(_attackState);
        }
    }
    public class EnemyBossWormStateContext
    {
        public IEnemyBossWormState CurrentState
        {
            get; set;
        }
        private readonly EnemyBossWorm _enemyBossWorm;

        public EnemyBossWormStateContext(EnemyBossWorm enemyBossWorm)
        {
            _enemyBossWorm = enemyBossWorm;
        }

        public void Transition()
        {
            CurrentState.Handle(_enemyBossWorm);
        }
        public void Transition(IEnemyBossWormState state)
        {
            CurrentState = state;
            CurrentState.Handle(_enemyBossWorm);
        }
    }
    public class EnemyBossWormIdleState : MonoBehaviour, IEnemyBossWormState
    {
        private float _idleTime = 2.0f;
        private float _idleTimer = 0.0f;
        private EnemyBossWorm _enemyBossWorm;
        public void Handle(EnemyBossWorm enemyBossWorm)
        {
            if (!_enemyBossWorm)
            {
                _enemyBossWorm = enemyBossWorm;
                _enemyBossWorm.CurrentIdleTimer = 0.0f;
            }
            Debug.Log("Enemy Boss Worm Idle");
        }

        public void DeleteController()
        {
            _enemyBossWorm = null;
        }
        void Update()
        {
            if (_enemyBossWorm)
            {
                if (_enemyBossWorm.CurrentIdleTimer >= _enemyBossWorm.idleTime)
                {
                    _enemyBossWorm.MoveWorm();
                    DeleteController();
                }
                else
                {
                    _enemyBossWorm.CurrentIdleTimer += Time.deltaTime;
                }
            }
        }
    }
    
    public class EnemyBossWormMoveState : MonoBehaviour, IEnemyBossWormState
    {
        private EnemyBossWorm _enemyBossWorm;
        private float _rotationRate = 2.0f;
        private float _rotationTime = 0.0f;
        public void Handle(EnemyBossWorm enemyBossWorm)
        {
            if (!_enemyBossWorm)
            {
                _enemyBossWorm = enemyBossWorm;
                _enemyBossWorm.CurrentRotationNumber = _enemyBossWorm.rotationNumber;
            }
            Debug.Log("Enemy Boss Worm Move");
        }
        public void DeleteController()
        {
            _enemyBossWorm = null;
        }
        void Update()
        {
            if (_enemyBossWorm)
            {
                _enemyBossWorm.transform.Translate(Vector3.forward * _enemyBossWorm.speed * Time.deltaTime);
                if (_enemyBossWorm.CurrentRotationNumber > 0)
                {
                    if (_rotationTime >= _rotationRate)
                    {
                        _enemyBossWorm.transform.Rotate(0.0f,Random.Range(30.0f, 150.0f),0.0f);
                        _rotationTime = 0.0f;
                        _enemyBossWorm.CurrentRotationNumber--;
                    }
                    else
                    {
                        _rotationTime += Time.deltaTime;
                    }
                
                }
                else
                {
                    _enemyBossWorm.IdleWorm();
                    DeleteController();
                }
            }
        }
    }

    public class EnemyBossWormAttackState : MonoBehaviour, IEnemyBossWormState
    {
        private EnemyBossWorm _enemyBossWorm;
        public void Handle(EnemyBossWorm enemyBossWorm)
        {
            if (!_enemyBossWorm)
            {
                _enemyBossWorm = enemyBossWorm;
            }
            Debug.Log("Enemy Boss Worm Attack");
        }
        public void DeleteController()
        {
            _enemyBossWorm = null;
        }
        void Update()
        {
            if (_enemyBossWorm)
            {
            
            }
        }
    }
}


