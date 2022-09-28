using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] AudioClip _missSound;
    [SerializeField] float _soundVolume = 0.5f;
    [SerializeField] float _maxLifetime = 7f;
    private float _currentLifetime = 0f;
    private void Update()
    {
        _currentLifetime += Time.deltaTime;
        if (_currentLifetime >= _maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HazardVolume hazard = other.gameObject.GetComponent<HazardVolume>();
        if (hazard != null)
        {
            hazard.Destroy();
            Destroy(gameObject);
        }
        Transform _transform = other.gameObject.transform;
        if (_transform.parent != null)
        {
            if (_transform.parent.CompareTag("Environment"))
            {
                StartCoroutine(PlayMissSound());
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator PlayMissSound()
    {
        AudioHelper.PlayClip2D(_missSound, _soundVolume);
        yield return new WaitForSeconds(_missSound.length);
    }
}
