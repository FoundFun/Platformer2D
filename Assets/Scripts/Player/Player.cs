using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _forcePush;
    [SerializeField] private TMP_Text _wallet;

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private int _hitAnimation = Animator.StringToHash("Hit");
    private string _slash = "/";

    private PlayerMover _playerMover;
    private Animator _animator;
    private AudioSource _audioSource;
    private Rigidbody2D _rigidbody2D;
    private bool _isFlip;
    private int _money;

    public bool IsFlip => _isFlip;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.Play(_hitAnimation);
        _audioSource.Play();
        HealthChanged?.Invoke(_health);
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _forcePush);

        if (_health <= 0)
            Die();
    }

    public void TakeMoney()
    {
        _money++;
        _wallet.text = _money.ToString() + _slash;
    }

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        HealthChanged?.Invoke(_health);

        _money = 0;
        _wallet.text = _money.ToString() + _slash;
        _audioSource.volume = 0.1f;
    }

    private void Update()
    {
        Flip();
    }

    private void Flip()
    {
        Vector3 flipY = transform.eulerAngles;

        if (_playerMover.MoveDirection.x < 0 && _isFlip == false)
        {
            flipY.y = 180;
            _isFlip = true;
        }
        else if (_playerMover.MoveDirection.x > 0 && _isFlip == true)
        {
            _isFlip = false;
            flipY.y = 0;
        }

        transform.eulerAngles = flipY;
    }

    private void Die()
    {
        Died?.Invoke();
    } 
}