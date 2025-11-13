using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ZombieController zombieController))
        {
            Destroy(zombieController.gameObject);
        }
    }
}
// comment ni sya
// another commenet ni syaa
