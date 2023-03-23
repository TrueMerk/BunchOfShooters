using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "")]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private int _startEnemyCount;
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private int _enemyPercent;

        public int StartEnemyCount
        {
            get => _startEnemyCount;
            
        }
        public int MaxEnemyCount
        {
            get => _maxEnemyCount;
        }

        public int EnemyPercent
        {
            get => _enemyPercent;
        }

    }
}
