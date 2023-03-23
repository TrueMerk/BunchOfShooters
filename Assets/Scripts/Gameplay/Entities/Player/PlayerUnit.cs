using System;
using Gameplay.Components;
using UnityEngine;
using Zenject;

namespace Gameplay.Entities.Player
{
    public class PlayerUnit : Unit
    {
        private Squad _squad;

        public Action GameOverAction;
        
        [Inject]
        public void Construct(Squad squad)
        {
            _squad = squad;
        }
        
        private void Start()
        {
            HealthComponent.Dead += Dead;
        }

        private void Dead()
        {
            _squad.Units.Remove(this);
            if (_squad.Units.Count == 0)
            {
                GameOverAction?.Invoke();
            }
        }
    }
}
