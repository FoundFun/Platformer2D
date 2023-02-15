using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Finish : MonoBehaviour
{
    private bool _isFinished = false;
    private int FlagOutAnimation = Animator.StringToHash("FlagOut");
    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && !_isFinished)
        {
            _animator.Play(FlagOutAnimation);
            _audioSource.Play();
            _isFinished = true;
        }
    }
}
