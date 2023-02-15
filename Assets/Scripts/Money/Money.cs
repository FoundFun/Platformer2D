using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Money : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeMoney();
            _animator.Play("Collected");
            _audioSource.Play();
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        WaitForSeconds timeShutdown = new WaitForSeconds(0.2f);

        yield return timeShutdown;

        gameObject.SetActive(false);
    }
}
