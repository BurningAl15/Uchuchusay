using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Game_State
{
    Message,
    Gameplay,
    End
}

public class GameState : MonoBehaviour
{
    public static GameState _instance;

    public Game_State currentState;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    public void ChangeState(Game_State _state)
    {
        currentState = _state;
    }

    public bool IsState(Game_State _state)
    {
        return currentState == _state;
    }
}
