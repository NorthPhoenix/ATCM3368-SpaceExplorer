using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    [SerializeField] ParticleSystem _destroyParticlePrefab;
    [SerializeField] AudioClip _destroySound;
    [SerializeField] float _volumeOfDestroy = 0.5f;
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip != null)
        {
            playerShip.Kill();
        }
    }

    public void Destroy()
    {
        if (_destroySound != null)
        {
            StartCoroutine(PlayDestroySound()); 
        }
        if (_destroyParticlePrefab != null)
        {
            Instantiate(_destroyParticlePrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    private IEnumerator PlayDestroySound()
    {
        AudioHelper.PlayClip2D(_destroySound, _volumeOfDestroy);
        yield return new WaitForSeconds(_destroySound.length);
    }

    
}
