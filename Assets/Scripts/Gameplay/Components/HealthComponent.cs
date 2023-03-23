using System;
using Gameplay.Entities.Enemy;
using Gameplay.Entities.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Components
{
   public class HealthComponent : MonoBehaviour
   {
          [SerializeField] private int _health;
          private int _startHealth ;

          private Squad _squad;
          
          public event Action Dead;

          [Inject]
          public void Construct(Squad squad)
          {
              _squad = squad;
          }
          
          private void Awake()
          {
              _startHealth = _health;
          }
          
          private void OnEnable()
          {
              _health = _startHealth;
          }
      
          public void TakeDamage()
          {
              if (_health >1)
                  _health--;
              else
              {
                  _health--;
                  Dead?.Invoke();
                 
                  gameObject.SetActive(false);
              }
          }
   }
}
