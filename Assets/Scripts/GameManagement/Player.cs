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
        //TODO replace the false with a check if the player is moving
        if(!redlightGreenlight.currentEquationCorrect && false)
        {
            //TODO add functionality for the player losing the game, make it vibrate and send the player back to start
        }

        //TODO add check for if player is close enough to the finish
        if(false)
        {
            points++;
            playerState = PlayerState.RETURNING;
        }
    }
}
