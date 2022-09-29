using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpeedPowerup : MonoBehaviour
{
    [SerializeField] float _speedIncreaseAmount = 20;
    [SerializeField] float _powerUpDuration = 3;

    [SerializeField] GameObject _artToDisable = null;

    [SerializeField] ParticleSystem _collectParticlePrefab;

    [SerializeField] AudioClip _activationClip;
    [SerializeField] AudioClip _deactivationClip;
    [SerializeField] float _volume = .5f;

    Collider _colider = null;

    private void Awake()
    {
        _colider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        if (playerShip != null)
        {
            StartCoroutine(PowerupSequence(playerShip));
        }
        
    }

    public IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        // soft disable 
        _colider.enabled = false;
        _artToDisable.SetActive(false);

        ActivatePowerup(playerShip);
        yield return new WaitForSeconds(_powerUpDuration);
        DeactivatePowerup(playerShip);

        Destroy(gameObject);
    }

    private void ActivatePowerup(PlayerShip playerShip)
    {
        playerShip.SetMoveSpeed(_speedIncreaseAmount);
        playerShip.SetBoosters(true);
        if (_activationClip != null)
        {
            AudioHelper.PlayClip2D(_activationClip, _volume);
        }
        if (_collectParticlePrefab != null)
        {
            Instantiate(_collectParticlePrefab, transform.position, transform.rotation);
        }
    }

    private void DeactivatePowerup(PlayerShip playerShip)
    {
        playerShip.SetMoveSpeed(-_speedIncreaseAmount);
        playerShip.SetBoosters(false);
        if (_deactivationClip != null)
        {
            AudioHelper.PlayClip2D(_deactivationClip, _volume);
        }
    }
}
