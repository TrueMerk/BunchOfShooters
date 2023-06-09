using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Components;
using Gameplay.Entities.Player;
using Gameplay.ObjectsPool;
using UnityEngine;
using Zenject;

namespace Gameplay.Entities.Enemy
{
    public class EnemyShooter : AttackComponent
    {
        private HealthComponent _playerHealth;

        [SerializeField] private bool isRange;
        [SerializeField] private BulletPool _pool;
        [SerializeField] public Transform _shotDir;
        [SerializeField] private float _shootRate;
        private GameObject _playerPos;
        private Squad _squad;
        private bool _isReload;
        private bool _playerExist;
        
        public override bool CanAttack => !_isReload && _squad.Units.Count>0;

        [Inject]
        public void Construct(Squad squad)
        {
            _playerHealth = squad.Units[0].HealthComponent;
            _squad = squad;
        }

        private void OnEnable()
        {
            _isReload = false;
        }

        private void Shoot()
        {
            if ( _squad.Units.Count!= 0)
            {
                var differenceMin = Target();
                var rotateZ = Math.Atan2(differenceMin.x, differenceMin.z) * Mathf.Rad2Deg;
                var target = Quaternion.Euler(0f,((float) (rotateZ)),0f);
                _shotDir.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 1000);
                _pool.CreateBullet(_shotDir, true);
                StartCoroutine(Reload());
            }
            else
            {
                _playerExist = false;
            }
        }

        private IEnumerator Reload()
        {
            _isReload = true;
            yield return new WaitForSeconds(_shootRate);
            _isReload = false;
        }
    
        private Vector3 Target()
        {
            var transformPositionFirst = _squad.Units[0].transform.position;
            var differenceMin = transformPositionFirst - transform.position;
            foreach (var unit in _squad.Units)
            {
                var transformPosition = unit.transform.position;
                var difference = transformPosition - transform.position;
                if (difference.magnitude < differenceMin.magnitude)
                {
                    differenceMin = difference;
                }
            }
            return differenceMin;
        }
    
        public override void Attack()
        {
            if (isRange)
            {
                Shoot();
            }
            else
            {
                _squad.Units[0].HealthComponent.TakeDamage();
            }
            
        }
    }
}
