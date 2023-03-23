using System;
using System.Collections.Generic;
using Gameplay.Components;
using Gameplay.Components.Input;
using UnityEngine;

namespace Gameplay.Entities.Player
{
    public class Squad : MonoBehaviour
    {
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private InputComponent _inputComponent;
        [SerializeField] private AnimationComponent _animationComponent;
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private List<PlayerUnit> _units;
        
        public List<PlayerUnit> Units => _units;
        
        public void Update()
        {
            var direction = _inputComponent.GetMovementDirection();
            var rotation = _inputComponent.GetRotation();
            
            if (rotation != Quaternion.Euler(0,0,0))
            {
                _movementComponent.Move(direction);
                _movementComponent.Rotate(rotation);
            }
            
            
            foreach (var unit in _units)
            {
                var isAttack = _inputComponent.IsAttacking();
                var canAttack = unit.AttackComponent;

                if (direction != Vector3.zero) 
                {
                    unit.AnimationComponent.PlayRunningAnimation();

                }
                
                else 
                {
                    unit.AnimationComponent.PlayIdleAnimation();
                   
                }
        
                if(canAttack && isAttack)
                {
                    unit.AnimationComponent.PlayAttackAnimation();
                    unit.AttackComponent.Attack();
                }
            }
            
        }
    }
}
