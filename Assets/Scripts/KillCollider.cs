using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class KillCollider : MonoBehaviour
    {
        [SerializeField] private string _playerTag = "Player";
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(_playerTag))
            {
                GameStateManager.Instance.Die();
            }
        }
        
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag(_playerTag))
            {
                GameStateManager.Instance.Die();
            }
        }
    }
}