using UnityEngine;

public class AirScrew : MonoBehaviour
{
    [SerializeField] private float _airForce;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TryGetComponent(out Rigidbody2D rigidbody2D);
            rigidbody2D.AddForce(new Vector2(0, _airForce), ForceMode2D.Force);
        }
    }
}
