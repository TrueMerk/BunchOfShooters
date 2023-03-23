using Gameplay.ObjectsPool;
using UnityEngine;
using Zenject;

namespace Gameplay.Entities.Token
{
    public class TokenPool : MonoBehaviour
    {
        [SerializeField] private int _poolCount = 3;
        [SerializeField] private bool _autoExpand = true;
        [SerializeField] private GoldenToken _tokenPrefab;
        [SerializeField] private Transform _tokenSpawner;
        
        
        private Pool _pool;

        [Inject]
        public void Construct(Pool pool)
        {
            _pool = pool;
            CreatePool();
        }
        
        private void CreatePool()
        {
            _pool.CreatePool<GoldenToken>(_tokenPrefab.gameObject,_poolCount,transform);
        }
    
        public void CreateToken(Transform tokenSpawner)
        {
            var token = _pool.GetFreeElement<GoldenToken>();
            token.StartAnimation();
            var o = token.gameObject;
            o.transform.position = _tokenSpawner.position;
            
            //o.transform.rotation = _tokenSpawner.rotation;
        }
    }
}
