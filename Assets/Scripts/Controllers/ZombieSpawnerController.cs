using System.Collections;
using UnityEngine;

public class ZombieSpawnerController : MonoBehaviour
{
    [SerializeField]
    private ZombieController _zombieControllerPrefab;

    [SerializeField]
    private PlayerController _playerController;
    
    [SerializeField]
    private Transform _spawnPoint;
    private IEnumerator Start()
    {
        while (true)
        {
            var zombieController = Instantiate(_zombieControllerPrefab, _spawnPoint.position, Quaternion.identity);
            zombieController.SetPlayer(_playerController.transform);
            yield return new WaitForSeconds(2f);
        }
    }
}
