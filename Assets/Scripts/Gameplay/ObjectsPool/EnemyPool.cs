using System.Collections;
using System.Collections.Generic;
using Gameplay.Entities.Enemy;
using SO;
using UnityEngine;
using Zenject;

namespace Gameplay.ObjectsPool
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private bool _autoExpand = true;
        [SerializeField] private Enemy _enemyPrefabAuto;
        [SerializeField] private Enemy _enemyPrefabNoGun;
        [SerializeField] private WaveData _waveData;
        [SerializeField] private List<Transform> _positions;

        private int _poolCount;
        private int _maxCount;
        private List<Enemy> _enemyList = new List<Enemy>();
        
        private Pool _poolGun;
        private Pool _poolNoGun;

        [Inject]
        public void Construct(Pool pool,Pool noGun, EnemyContainer container)
        {
            _enemyList = container.EnemyList;
            _poolGun = pool;
            _poolNoGun = noGun;
            _poolCount = _waveData.StartEnemyCount;
            _maxCount = _waveData.MaxEnemyCount;
            CreatePool();
            StartCoroutine(SpawnEnemyCoroutine(1));
        }
        
        private void CreatePool()
        {
            _poolGun.CreatePool<Enemy>(_enemyPrefabAuto.gameObject,_poolCount/2,transform);
            _poolNoGun.CreatePool<Enemy>(_enemyPrefabNoGun.gameObject,_poolCount/2,transform);
        }

        private void CreateEnemy(Transform enemySpawner, Pool pool)
        {
            var enemy = pool.GetFreeElement<Enemy>();
            var o = enemy.gameObject;
            o.transform.position = RandomPosition().position;
            _enemyList.Add(enemy);
            
        }

        private Transform RandomPosition()
        {
            var pos = Random.Range(0, _positions.Count);
            return _positions[pos].transform;
        }
        
        private IEnumerator SpawnEnemyCoroutine(float rate)
        {
            while (true)
            {
                if (_enemyList.Count == 0 )
                {
                    _enemyList.Clear();
                    for (var i = 0; i < _poolCount/2; i++)
                    {
                        CreateEnemy(transform,_poolGun);
                        CreateEnemy(transform,_poolNoGun);
                    }

                    if (_poolCount < _maxCount)
                    {
                        _poolCount++;
                    }
                    
                }
                yield return new WaitForSeconds(rate);
            }
        }
    }
}
