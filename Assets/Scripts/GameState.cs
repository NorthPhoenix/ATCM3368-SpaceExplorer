using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField] Text _endgameText;
    [SerializeField] GameObject _UITint;

    private void Awake()
    {
        _endgameText.enabled = false;
        _UITint.SetActive(false);
    }
    public void EndGame(string msg, Color clr)
    {
        _UITint.SetActive(true);

        _endgameText.text = msg;
        _endgameText.color = clr;
        _endgameText.enabled = true;
    }
}
