using Gameplay.Components;
using Gameplay.Entities.Player;
using UnityEngine;

namespace Gameplay.Entities.Bullet
{
    public class BulletDamageDealer : MonoBehaviour
    {
        public bool FromEnemy;
        private void OnTriggerEnter(Collider other)
        {
            if (FromEnemy)
            {
                var player = other.GetComponent<PlayerUnit>();
                var health = other.GetComponent<HealthComponent>();
                if (player != null && health != null)
                {
                    health.TakeDamage();
                }
            }

            if (!FromEnemy)
            {
                var player = other.GetComponent<PlayerUnit>();
                var health = other.GetComponent<HealthComponent>();
                if (player == null && health != null)
                {
                    health.TakeDamage();
                }
            }
            
        }
    }
}
