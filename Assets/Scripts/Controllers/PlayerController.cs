using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _bulletSpawnPoint;

    [SerializeField]
    private int _movementSpeed = 1;
    
    [FormerlySerializedAs("_speed")]
    [SerializeField]
    private int _bulletSpeed = 1;

    [SerializeField]
    private Rigidbody _rigidbody;

    [FormerlySerializedAs("_bulletAudio")]
    [SerializeField]
    private AudioSource _bulletAudioSource;

    [SerializeField]
    private AudioClip _bulletAudioClip;
    
    private Vector3 _moveDirection;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        LookDirection();
        PlayerInput();
    }

    private void FixedUpdate()
    {
        SetMoveDirection(_moveDirection);
    }

    private void PlayerInput()
    {
        _moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnFireGun();
        }

        if (Input.GetKey(KeyCode.W))
        {
            _moveDirection += transform.forward;
        }
    
        if (Input.GetKey(KeyCode.A))
        {
            _moveDirection += -transform.right;
        }
    
        if (Input.GetKey(KeyCode.S))
        {
            _moveDirection += -transform.forward;
        }
    
        if (Input.GetKey(KeyCode.D))
        {
            _moveDirection += transform.right;
        }

        _moveDirection.Normalize(); // prevents diagonal speed boost
    }

    private void SetMoveDirection(Vector3 moveDirection)
    {
        var finalPosition = _rigidbody.position + moveDirection * _movementSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(finalPosition);
    }

    private void LookDirection()
    {
        float y = Input.GetAxis("Mouse Y");
        float x = Input.GetAxis("Mouse X");
        float finalX = Mathf.Clamp(x, -90f, 90f);
        
        var lookDirection = new Vector3(-y, finalX, 0);
        transform.eulerAngles += lookDirection;
    }

    private void OnFireGun()
    {
        Debug.Log($"#{GetType()}: Gun fired!");
        _bulletAudioSource.PlayOneShot(_bulletAudioClip);
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(_bulletSpawnPoint.forward * _bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DeleteBullet(bullet));
    }

    private IEnumerator DeleteBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        Destroy(bullet);
    }
}
