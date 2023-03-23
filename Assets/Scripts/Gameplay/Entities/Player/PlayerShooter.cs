using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Components;
using Gameplay.Entities.Enemy;
using Gameplay.ObjectsPool;
using UnityEngine;
using Zenject;

namespace Gameplay.Entities.Player
{
    public class PlayerShooter : AttackComponent
    {
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] public Transform _shotDir;
        [SerializeField] private float _shootRate;
        private GameObject _playerPos;
        private bool _isReload;
        private List<Enemy.Enemy> _enemies;
        private bool _enemyExist;

        public override bool CanAttack => !_isReload && _enemies.Count>0;

        [Inject]
        public void Construct(EnemyContainer _container)
        {
            _enemies = _container.EnemyList;
            _enemyExist = _container.EnemyExist;
        }
        
        private void Shoot()
        {
        
            if ( _enemies.Count != 0)
            {
                var differenceMin = Target();
                var rotateZ = Math.Atan2(differenceMin.x, differenceMin.z) * Mathf.Rad2Deg;
                var target = Quaternion.Euler(0f,((float) (rotateZ)),0f);
                _shotDir.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 1000);
                _bulletPool.CreateBullet(_shotDir,false);
                StartCoroutine(Reload());
            }
            else
            {
                _enemyExist = false;
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
            var transformPositionFirst = _enemies[0].transform.position;
            var differenceMin = transformPositionFirst - transform.position;
            foreach (var Enemy in _enemies)
            {
                var transformPosition = Enemy.transform.position;
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
            if (!_isReload)
            {
                Shoot();
            }
        }
    }
}
