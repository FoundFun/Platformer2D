using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class WallSlide : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;

    private int _wallJumpAnimation = Animator.StringToHash("WallJump");

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            if (_rigidbody2D.velocity.y < _speed)
            {
                _animator.Play(_wallJumpAnimation);
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _speed);
            }
        }
    }
}
