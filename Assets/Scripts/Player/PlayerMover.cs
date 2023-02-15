using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private int _runAnimation = Animator.StringToHash("Run");
    private int _jumpAnimation = Animator.StringToHash("Jump");
    private int _doubleJumpAnimation = Animator.StringToHash("DoubleJump");
    private int _fallAnimation = Animator.StringToHash("Fall");
    private float _fallVelocity = -2f;

    private Rigidbody2D _rigidbody2D;
    private PlayerInput _input;
    private Animator _animator;
    private Vector2 _moveDirection;
    private bool _isDoubleJump;
    private bool _isGround;
    private bool _isWall;
    private float _timer;

    public Vector2 MoveDirection => _moveDirection;

    private void Awake() => _input = new PlayerInput();

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Jump.performed += context => OnJump();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Jump.performed -= context => OnJump();
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime; 
        _moveDirection = _input.Player.Move.ReadValue<Vector2>();

        if (_moveDirection.x != 0)
        {
            Move(_moveDirection);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }
    }

    private void Move(Vector3 direction)
    {
        if (_isGround)
        {
            _animator.Play(_runAnimation);
        }
        else if (_rigidbody2D.velocity.y < _fallVelocity)
        {
            _animator.Play(_fallAnimation);
        }

        _rigidbody2D.velocity = new Vector2(direction.x * _speed, _rigidbody2D.velocity.y);
    }

    private void OnJump()
    {
        float time = 0.7f;

        if (_isGround)
        {
            Jump(Vector2.up, _jumpAnimation);
        }
        else if (_isWall && _timer > time)
        {
            _timer = 0;
            Jump(Vector2.up, _jumpAnimation);
        }
        else if (!_isDoubleJump)
        {
            Jump(Vector2.up, _doubleJumpAnimation);
            _isDoubleJump = true;
        }
    }

    private void Jump(Vector2 direction, int animation)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _animator.Play(animation);
        _rigidbody2D.AddForce(direction * _jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground) || collision.collider.TryGetComponent(out Platfrom platfrom))
        {
            _isDoubleJump = false;
            _isGround = true;
        }

        if (collision.collider.TryGetComponent(out Wall wall))
        {
            _isWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground) || collision.collider.TryGetComponent(out Platfrom platfrom))
        {
            _isGround = false;
        }

        if (collision.collider.TryGetComponent(out Wall wall))
        {
            _isWall = false;
        }
    }
}
