using System;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    
    private Transform _playerTarget;
    
    public void SetPlayer(Transform target)
    {
        _playerTarget = target;
    }

    private void Update()
    {
        if (_playerTarget == null)
            return;
        
        var finalPosition = _rigidbody.position + transform.forward * 2 * Time.fixedDeltaTime;
        _rigidbody.MovePosition(finalPosition);
        transform.LookAt(_playerTarget);
    }
}
