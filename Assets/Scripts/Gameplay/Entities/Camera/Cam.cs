using UnityEngine;

namespace Gameplay.Entities.Camera
{
    public class Cam : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        private Vector3 offset;

        private void Start()
        {
            var transformPosition = transform.position;
            offset = _target.transform.position - transformPosition;
        }

        void Update()
        {
            float desiredAngle = _target.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = _target.transform.position -(rotation * offset);
            transform.LookAt(_target.transform);
        }
    }
}
