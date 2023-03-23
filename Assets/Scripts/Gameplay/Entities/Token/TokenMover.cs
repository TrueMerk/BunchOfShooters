using System;
using UnityEngine;
using Zenject;

namespace SarrrGames.GoldenRush.Gameplay.Entities.Token
{
    public class TokenMover : MonoBehaviour
    {
        private float _existingTime;
        private TokenProgressModel _token;

        [Inject]
        public void Construct(TokenProgressModel token)
        {
            _token = token;
        }
        
        private void OnEnable()
        {
            Debug.Log("ENABLE");
            transform.SetParent(null);
        }

        private void Update()
        {
            if (_existingTime < 2 && isActiveAndEnabled)
            {
                transform.Translate(new Vector3(0,1*0.1f,0));
                _existingTime += Time.deltaTime;
            }
            else if(_existingTime>=2)
            {
                _token.Soft.SetValue(_token.Soft.GetValue()+15);
                _token.BloxCount.SetValue(_token.BloxCount.GetValue()-1);
                
                _existingTime = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
