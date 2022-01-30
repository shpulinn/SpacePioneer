using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    private Transform _playerInventoryTransform;

    private bool _startMoving = false;

    public void SendResourceToPlayer(Transform inventroyTransform) {
        _playerInventoryTransform = inventroyTransform;
        _startMoving = true;
    }

    private void Update() {
        if (_playerInventoryTransform != null) {
            Vector3.Lerp(transform.position, _playerInventoryTransform.position, .1f * Time.deltaTime);
        }
    }

}
