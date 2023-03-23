using System.Collections;
using System.Collections.Generic;
using Gameplay.Entities.Bullet;
using Gameplay.Entities.Enemy;
using UnityEngine;
using Zenject;

namespace Gameplay.ObjectsPool
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private int _poolCount = 20;
        [SerializeField] private bool _autoExpand = true;
        [SerializeField] private BulletDamageDealer _bulletPrefab;

        [SerializeField] private bool isEnemy;
        private Pool _pool;

        [Inject]
        public void Construct(Pool pool)
        {
            _pool = pool;
            CreatePool();
        }
        
        private void CreatePool()
        {
            _pool.CreatePool<BulletDamageDealer>(_bulletPrefab.gameObject,_poolCount,transform);
        }

        public void CreateBullet(Transform bulletSpawner, bool isEnemy)
        {
            var dd = _pool.GetFreeElement<BulletDamageDealer>();
            dd.FromEnemy = isEnemy;
            var o = dd.gameObject;
            o.transform.position = bulletSpawner.position;
            o.transform.rotation = bulletSpawner.rotation;
            o.transform.SetParent(null);


        }

    }
}
