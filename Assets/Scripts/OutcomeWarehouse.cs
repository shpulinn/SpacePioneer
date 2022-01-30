using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutcomeWarehouse : MonoBehaviour {

    [SerializeField] private int _maxCapacity;

    [SerializeField] private Text _producedAmountLabel;

    private int _currentResourcesCount = 0;

    private List<GameObject> _resources = new List<GameObject>();

    private bool _transportingResources = false;

    private PlayerInventory _playerInventory;

    public int MaxCapacity { get { return _maxCapacity; } }

    public int CurrentResourcesCount { get { return _currentResourcesCount; } }

    private void Start() {
        _producedAmountLabel.text = $"{_currentResourcesCount}/{_maxCapacity}";
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            _playerInventory = other.GetComponentInChildren<PlayerInventory>();
            if (_transportingResources) {
                return;
            }
            StartCoroutine(GiveResourceToPlayer());
        }
    }

    private void OnTriggerExit(Collider other) {
        _transportingResources = false;
        _playerInventory = null;
        _resources.Sort();
    }

    public void AddResource(GameObject resource) {
        GameObject spawnedResource = Instantiate(resource, new Vector3(transform.position.x, transform.position.y + CurrentResourcesCount * .3f, 
            transform.position.z), Quaternion.identity);
        spawnedResource.transform.parent = transform;
        spawnedResource.name = resource.name + " " + _currentResourcesCount;
        _resources.Add(spawnedResource);
        _currentResourcesCount++;
        _producedAmountLabel.text = $"{_currentResourcesCount}/{_maxCapacity}";
    }

    private IEnumerator GiveResourceToPlayer() {
        _transportingResources = true;
        for (int i = 0; i < _resources.Count; i++) {
            if (_transportingResources == false) {
                yield break;
            }

            _playerInventory.AddResource(_resources[i]);
            _resources[i].GetComponent<Resource>().SendResourceToPlayer(_playerInventory.transform);

            Destroy(_resources[i]);
            _resources.RemoveAt(i);
            _currentResourcesCount--;
            _producedAmountLabel.text = $"{_currentResourcesCount}/{_maxCapacity}";
            yield return new WaitForSeconds(.5f);
        }
        _transportingResources = false;
    }
}