using SarrrGames.GoldenRush.Gameplay.Entities.Token;
using UnityEngine;
using Zenject;

namespace Gameplay.Entities.Token
{
    public class GoldenToken : MonoBehaviour
    {
        [SerializeField] private Vector3 _counterTransform;
        [SerializeField] private Vector3 _startTransform;
        private TokenProgressModel _token;
        
        
        [Inject]
        public void Construct(TokenProgressModel token)
        {
            _token = token;
            _startTransform = transform.position;
            _counterTransform = new Vector3(-5,20,0);
            
            StartAnimation();
        }
        
        private void OnEnable()
        {
            transform.position = _startTransform;
        }
        
        public void StartAnimation()
        {
             //_tween = transform.DOMove(_counterTransform, 2)
             //   .OnComplete(EndAnimation)
            //    .Play();
            // transform.DORestart();
        }

        private void EndAnimation()
        {
            Debug.Log("Анимация кончилась");
            if (gameObject.activeInHierarchy)
            {
                Debug.Log("Анимация кончилась");
                gameObject.SetActive(false);
                _token.Soft.SetValue(_token.Soft.GetValue()+15);
                _token.BloxCount.SetValue(_token.BloxCount.GetValue()-1);
            }
            
        }
        
    }
}
