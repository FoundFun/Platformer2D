using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Box : MonoBehaviour
{
    private int Hit = Animator.StringToHash("Hit");

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _animator.Play(Hit);

            StartCoroutine(Ignore(collision));
        }
    }

    private IEnumerator Ignore(Collision2D collision)
    {
        WaitForSeconds colliderShutdownTimer = new WaitForSeconds(0.5f);
        WaitForSeconds shutdownTimer = new WaitForSeconds(2f);

        yield return colliderShutdownTimer;

        Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);

        yield return shutdownTimer;

        gameObject.SetActive(false);
    }
}
