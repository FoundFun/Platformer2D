using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private Money _moneyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        Initialize(_moneyPrefab);

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            if (TryGetObject(out GameObject money))
            {
                SetMoney(money, _spawnPoints[i].position);
            }
        }
    }

    private void SetMoney(GameObject money, Vector3 spawnPoint)
    {
        money.SetActive(true);
        money.transform.position = spawnPoint;
    }
}
