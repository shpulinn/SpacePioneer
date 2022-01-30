using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    [SerializeField] private int _maxCapacity;

    private int _currentResourcesCount = 0;

    private List<GameObject> _resources = new List<GameObject>();

    private bool _transportingResources = false;

    public int MaxCapacity { get { return _maxCapacity; } }

    public int CurrentResourcesCount { get { return _currentResourcesCount; } }

    public void AddResource(GameObject resource) {
        GameObject spawnedResource = Instantiate(resource, new Vector3(transform.position.x, transform.position.y + _currentResourcesCount * .35f, 
            transform.position.z), Quaternion.identity.normalized);
        spawnedResource.transform.localScale = new Vector3(1, .25f, 1);
        spawnedResource.transform.parent = transform;
        spawnedResource.name = resource.name + " " + _currentResourcesCount;
        _resources.Add(spawnedResource);
        _currentResourcesCount++;
    }

    private IEnumerator GiveResourceToWarehouse() {
        _transportingResources = true;
        for (int i = 0; i < _resources.Count; i++) {
            Destroy(_resources[i]);
            _resources.RemoveAt(i);
            _currentResourcesCount--;
            yield return new WaitForSeconds(.5f);
        }
        _transportingResources = false;
    }
}