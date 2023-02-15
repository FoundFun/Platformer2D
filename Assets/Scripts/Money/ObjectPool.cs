using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TMP_Text _maxNumberMoney;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(Money prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Money spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned.gameObject);
        }

        _maxNumberMoney.text = _capacity.ToString();
    }

    protected bool TryGetObject(out GameObject result) => result = _pool.FirstOrDefault(gameObject => gameObject.activeSelf == false);
}
