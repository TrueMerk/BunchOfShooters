using SarrrGames.GoldenRush.Gameplay.Entities;
using UI;
using UnityEngine;

namespace Gameplay.Components.Input
{
    public class JoystickInput : InputComponent
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private BtnClicked _attackButton;
        private bool _attack;

        private void Start()
        {
            _attackButton.OnPointDown += BtnClicked;
            _attackButton.OnPointUp += BtnUP;
        }

        public override Vector3 GetMovementDirection()
        {
            return Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;
        }

        public override Quaternion GetRotation()
        {
            var target = Vector3.forward *_joystick.Vertical + Vector3.right*_joystick.Horizontal;
            var rot = Quaternion.LookRotation(target, target);
            return rot;
        }

        public override bool IsAttacking()
        {
            return true;
        }

        private void BtnClicked()
        {
            _attack = true;
        }

        private void BtnUP()
        {
            _attack = false;
        }
    }
}
