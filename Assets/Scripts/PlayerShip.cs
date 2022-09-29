using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;
    [Space(20)]

    [Header("Dash")]
    [SerializeField] float _dashStrength = 1000f;
    [SerializeField] TrailRenderer _dashTrail;
    [SerializeField] float _dashTrailDuration = 0.3f;
    [SerializeField] AudioClip _dashClip;
    [SerializeField] float _dashVolume = 0.5f;
    [Space(20)]

    [Header("Victory")]
    [SerializeField] string _victoryMsg;
    [SerializeField] AudioClip _winClip;
    [SerializeField] float _winVolume = 0.5f;
    [Space(20)]

    [Header("Death")]
    [SerializeField] string _deathMsg;
    [SerializeField] AudioClip _deathClip;
    [SerializeField] float _deathVolume = 0.5f;
    [Space(20)]

    [Header("Booster")]
    [SerializeField] GameObject _boosters;
    [Space(20)]

    [Header("Gun")]
    [SerializeField] ParticleSystem _gunPowerUpParticles;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletSpeed = 70f;
    [SerializeField] AudioClip _gunSound;
    [SerializeField] float _gunSoundVolume = 0.5f;

    public bool _gunEnabled { get; set; } = false;
    Rigidbody _rb = null;
    GameObject _gameController = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gameController = GameObject.Find("GameController");
        SetBoosters(false);
        if (_gunPowerUpParticles != null)
        {
            _gunPowerUpParticles.Stop();
        }
    }
    private void Update()
    {
        if (_gunEnabled && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Dash(-transform.right);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Dash(transform.right);
        }
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    private void MoveShip()
    {
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _moveSpeed;
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        _rb.AddForce(moveDirection);

    }

    private void TurnShip()
    {
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }
    
    private void Dash(Vector3 direction)
    {
        _rb.AddForce(direction * _dashStrength);
        if (_dashClip != null)
        {
            AudioHelper.PlayClip2D(_dashClip, _dashVolume);
        }
        StartCoroutine(RenderDashTrail());
    }

    private IEnumerator RenderDashTrail()
    {
        _dashTrail.emitting = true;
        yield return new WaitForSeconds(_dashTrailDuration);
        _dashTrail.emitting = false;
    }

    public void Kill()
    {
        Debug.Log("Player has been killed!");
        if (_deathClip != null)
        {
            AudioHelper.PlayClip2D(_deathClip, _deathVolume);
        }
        this.gameObject.SetActive(false);
        if (_gameController)
        {
            _gameController.GetComponent<GameState>().EndGame(_deathMsg, Color.red);
        }

    }

    public void Win()
    {
        Debug.Log("Player has won!");
        if (_winClip != null)
        {
            AudioHelper.PlayClip2D(_winClip, _winVolume);
        }
        this.gameObject.SetActive(false);
        if (_gameController)
        {
            _gameController.GetComponent<GameState>().EndGame(_victoryMsg, Color.green);
        }
    }

    public void SetMoveSpeed(float speedIncreaseAmount)
    {
        _moveSpeed += speedIncreaseAmount;
    }

    public void SetBoosters(bool activeState)
    {
        foreach (Transform trail in _boosters.transform)
        {
            TrailRenderer tr = trail.gameObject.GetComponent<TrailRenderer>();
            tr.emitting = activeState;
        }
    }

    public void SetGunParticles(bool activeState)
    {
        if (activeState)
        {
            _gunPowerUpParticles.Play();
        }
        else
        {
            _gunPowerUpParticles.Stop();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed;
        if (_gunSound !=null)
        {
            AudioHelper.PlayClip2D(_gunSound, _gunSoundVolume);
        }
    }
}
