using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public enum Players
    {
        None,
        O,
        X
    }
    //public Players currentPlayer;
    public Players currentPlayer; //  false 0,  true 1
    private Players lastPlayerWhoBegan;

    private const int numberRows = 3;
    private const int numberColumns = 3;
    public Players[,] grid;

    #endregion


    #region Monobehaviour
    void Awake()
    {
        Director.Instance.gameManager = this;
    }

    private void Start()
    {
        Director.Instance.EverythingBeginsHere();

        lastPlayerWhoBegan = Players.None;
    }
    #endregion


    #region Private methods
    private void ResetGrid()
    {
        grid = new Players[numberRows, numberColumns];
        for( int i = 0; i < numberRows; i++ )
        {
            for( int j = 0; j < numberColumns; j++ )
            {
                grid[i, j] = Players.None;
            }
        }
    }

    private void PassTurn()
    {
        if( currentPlayer == Players.O )
        {
            currentPlayer = Players.X;
        }
        else//if( currentPlayer == Players.X )
        {
            currentPlayer = Players.O;
        }
    }

    #endregion


    #region Public
    public void StartGame()
    {
        ResetGrid();

        // If this is the first game, X begins
        if( lastPlayerWhoBegan == Players.None || lastPlayerWhoBegan == Players.O)
        {
            currentPlayer = Players.X;
            lastPlayerWhoBegan = Players.X;
        }
        else
        {
            currentPlayer = Players.O;
            lastPlayerWhoBegan = Players.O;
        }

        Debug.Log( "Begin the game with player: "+ currentPlayer.ToString() );
    }

    public Players PlayTurn( int row, int column )
    {
        grid[row, column] = currentPlayer;


        // Save current player
        Players lastPlayer = currentPlayer;

        // Change to other player
        PassTurn();

        // And return the player who just played
        return lastPlayer;
    }

    public Players GetCurrentPlayer()
    {
        return currentPlayer;
    }
    #endregion
}
