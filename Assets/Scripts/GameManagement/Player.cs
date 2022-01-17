using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private RedlightGreenlight redlightGreenlight;
    [SerializeField]
    private MovementChecker movementChecker;
    [SerializeField]
    private LED led;
    private int points;
    private enum PlayerState
    {
        STARTING,
        PLAYING,
        RETURNING
    }
    PlayerState playerState = PlayerState.STARTING;

    private void Update()
    {
        switch (playerState)
        {
            case PlayerState.STARTING:
                if(redlightGreenlight.gameStarted)
                {
                    Return();
                }
                break;
            case PlayerState.PLAYING:
                if (!redlightGreenlight.currentEquationCorrect && movementChecker.IsPlayerMoving())
                {
                    Handheld.Vibrate();
                    led.Lose();
                    playerState = PlayerState.RETURNING;
                }

                //TODO add check for if player is close enough to the finish
                if (false)
                {
                    Finish();
                }

                break;
            case PlayerState.RETURNING:
                //TODO add check if player is close enough to start
                if(false)
                {
                    Return();
                }
                break;
            default:
                break;
        }
    }

    //public for mocking purposes
    public void Finish()
    {
        points++;
        led.Win();
        playerState = PlayerState.RETURNING;
    }

    //public for mocking purposes
    public void Return()
    {
        led.StartGame();
        playerState = PlayerState.PLAYING;
    }
}
