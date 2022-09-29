using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow = null;

    Vector3 _objectOffset;

    private void Awake()
    {
        _objectOffset = this.transform.position - _objectToFollow.position;
    }

    private void LateUpdate()
    {
        this.transform.position = _objectToFollow.position + _objectOffset;
    }
}
