using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private RedlightGreenlight redlightGreenlight;
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
                break;
            case PlayerState.PLAYING:
                //TODO replace the false with a check if the player is moving
                if (!redlightGreenlight.currentEquationCorrect && false)
                {
                    Handheld.Vibrate();
                    playerState = PlayerState.RETURNING;
                }

                //TODO add check for if player is close enough to the finish
                if (false)
                {
                    points++;
                    playerState = PlayerState.RETURNING;
                }

                break;
            case PlayerState.RETURNING:
                //TODO add check if player is close enough to start
                if(false)
                {
                    playerState = PlayerState.PLAYING;
                }
                break;
            default:
                break;
        }
    }
}
