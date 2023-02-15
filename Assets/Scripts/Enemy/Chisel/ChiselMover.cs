using UnityEngine;

public class ChiselMover : MonoBehaviour
{
    [SerializeField] private Transform _startWayPoints;
    [SerializeField] private Transform _endWayPoints;

    private float _duration = 1;

    private float _timer;

    private void Start() => transform.position = _startWayPoints.position;

    private void Update() => Move();

    private void Move()
    {
        transform.position = Vector3.Lerp(_startWayPoints.position, _endWayPoints.position, Mathf.PingPong(_timer, _duration));
        _timer += Time.deltaTime;
    }
}
