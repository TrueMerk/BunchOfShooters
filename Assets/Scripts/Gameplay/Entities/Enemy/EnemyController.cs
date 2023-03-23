using Gameplay.Components;
using Gameplay.Components.Input;
using Gameplay.Entities.Player;

using UnityEngine;

namespace Gameplay.Entities.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private InputComponent _inputComponent;
        [SerializeField] private AnimationComponent _animatorComponent;
        [SerializeField] private AttackComponent _attackComponent;

        
        public void Update()
        {
            var direction = _inputComponent.GetMovementDirection();
           
            var rotation = _inputComponent.GetRotation();
            _movementComponent.Rotate(rotation);
        
            var isAttack = _inputComponent.IsAttacking();
            var canAttack = _attackComponent.CanAttack;
        
            
            if (direction!=Vector3.zero)
            {
                _animatorComponent.PlayRunningAnimation();
                Debug.Log("RUNANIMATION");
                _movementComponent.Move(Vector3.forward);
            }
        
            else if (!canAttack || !isAttack)
            {
                _animatorComponent.PlayIdleAnimation();
            }
        
            if(canAttack && isAttack)
            {
                _animatorComponent.PlayAttackAnimation();
                _attackComponent.Attack();
            }
        
        }
    }
}
