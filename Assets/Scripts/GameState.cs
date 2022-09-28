using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField] Text _endgameText;
    [SerializeField] GameObject _UITint;

    [SerializeField] AudioClip _themeMelodyIntro;
    [SerializeField] AudioClip _themeMelodyLoop;

    private void Awake()
    {
        _endgameText.enabled = false;
        _UITint.SetActive(false);
        if (_themeMelodyIntro != null && _themeMelodyLoop != null)
        {
            AudioHelper.PlayBGMWithIntro(_themeMelodyIntro, _themeMelodyLoop, 0.2f);
        }
    }

    public void EndGame(string msg, Color clr)
    {
        _UITint.SetActive(true);

        _endgameText.text = msg;
        _endgameText.color = clr;
        _endgameText.enabled = true;
    }
}
