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

    Rigidbody _rb = null;
    GameObject _gameController = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gameController = GameObject.Find("GameController");
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    void MoveShip()
    {
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _moveSpeed;
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        _rb.AddForce(moveDirection);

    }

    void TurnShip()
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
}
