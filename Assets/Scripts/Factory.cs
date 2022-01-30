using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    [SerializeField] private string _factoryName;

    [SerializeField] private float _timeToProduce;

    [SerializeField] private Transform _incomeWarehouse;
    [SerializeField] private int _incomeWarehouseMaxAmount;

    [SerializeField] private OutcomeWarehouse _outcomeWarehouse;

    [SerializeField] private List<Resource> _requiredResources = new List<Resource>();
    [SerializeField] private GameObject _producedResourcePrefab;

    private float _timer;

    private bool _reachedMax = false;

    private void Start() {
        StartCoroutine(ProduceResource());
    }

    private void Update() {
        if (_reachedMax == false) return;
        if (_outcomeWarehouse.CurrentResourcesCount < _outcomeWarehouse.MaxCapacity) StartCoroutine(ProduceResource());
    }

    private IEnumerator ProduceResource() {
        if (_outcomeWarehouse.CurrentResourcesCount == _outcomeWarehouse.MaxCapacity) {
            Debug.Log($"Max resources on {_factoryName} factory!");
            _reachedMax = true;
            yield break;
        }
        _timer++;
        if (_timer >= _timeToProduce) {
            _timer = 0;
            _outcomeWarehouse.AddResource(_producedResourcePrefab);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(ProduceResource());
    }

}