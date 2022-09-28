using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;

    [SerializeField] string _victoryMsg;
    [SerializeField] string _deathMsg;

    [SerializeField] GameObject _boosters;

    Rigidbody _rb = null;
    GameObject _gameController = null;

    public bool _gunEnabled { get; set; } = false;
    [SerializeField] ParticleSystem _gunPowerUpParticles;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletSpeed = 70f;
    [SerializeField] AudioClip _gunSound;
    [SerializeField] float _gunSoundVolume = 0.5f;

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

    public void Kill()
    {
        Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
        if (_gameController)
        {
            _gameController.GetComponent<GameState>().EndGame(_deathMsg, Color.red);
        }

    }

    public void Win()
    {
        Debug.Log("Player has won!");
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
