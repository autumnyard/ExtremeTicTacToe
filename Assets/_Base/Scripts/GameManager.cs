using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    // Board management
    [SerializeField] private Board board;
    private const int numberRows = 3;
    private const int numberColumns = 3;
    private Players[,] grid;

    // Player management
    public enum Players
    {
        None = 0,
        O,
        X,
        MaxValues
    }
    [SerializeField] private Players currentPlayer; //  false 0,  true 1
    [SerializeField] private Players lastPlayerWhoBegan;

    // Winner count
    [SerializeField] private int[] winnerCount = new int[(int)Players.MaxValues];



    public delegate void Delegate( Players winner );
    public Delegate OnEndGame;
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

    private void SwitchPlayer()
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

    private void CheckEndGameConditions()
    {

        // We must check for straight lines of 3 grid positions with the same player

        // For each row, check if all 3 of its placements are the same player
        for( int i = 0; i < numberRows; i++ )
        {
            Players check = grid[i, 0];

            if( check == Players.None )
            {
                continue;
            }
            else if( grid[i, 1] == check && grid[i, 2] == check )
            {

                EndGame( check );
                return;
            }
        }

        // For each column, check if all 3 of its placements are the same player
        for( int i = 0; i < numberColumns; i++ )
        {
            Players check = grid[0, i];

            if( check == Players.None )
            {
                continue;
            }

            else if( grid[1, i] == check && grid[2, i] == check )
            {
                EndGame( check );
                return;
            }
        }

        // Diagonal left to right
        {
            Players check = grid[0, 0];

            if( check != Players.None && grid[1, 1] == check && grid[2, 2] == check )
            {
                EndGame( check );
                return;
            }
        }

        // Diagonal right to left
        {
            Players check = grid[0, 2];

            if( check != Players.None && grid[1, 1] == check && grid[2, 0] == check )
            {
                EndGame( check );
                return;
            }
        }
    }

    private void EndGame( Players which )
    {
        Debug.Log( "Finished game, winner is: " + which );
        // Add to count
        winnerCount[(int)which]++;
        UpdateUIWithWinner();

        if( OnEndGame != null )
        {

            OnEndGame( which );
        }
        else
        {
            Debug.LogError( "Couldn't finish game properly. FATAL ERROR." );
        }
    }
    #endregion


    #region Public
    public void StartGame()
    {
        // If this is the first game, X begins
        if( lastPlayerWhoBegan == Players.None || lastPlayerWhoBegan == Players.O )
        {
            currentPlayer = Players.X;
            lastPlayerWhoBegan = Players.X;
        }
        else
        {
            currentPlayer = Players.O;
            lastPlayerWhoBegan = Players.O;
        }

        // Set UI with player who will begin
        UpdateUIWithCurrentPlayer();
    }

    public void PlayTurn( int row, int column )
    {
        // Trigger graphical feedback, info and effects
        board.PressOnPosition( row, column, currentPlayer );

        // Modify game state
        PassTurn( row, column );

    }

    private Players PassTurn( int row, int column )
    {
        grid[row, column] = currentPlayer;

        // Check end game conditions
        CheckEndGameConditions();

        // Save current player
        Players lastPlayer = currentPlayer;

        // Change to other player
        SwitchPlayer();

        UpdateUIWithCurrentPlayer();

        // And return the player who just played
        return lastPlayer;
    }

    public void ResetGame()
    {
        board.Reset();
        ResetGrid();
    }

    private void UpdateUIWithCurrentPlayer()
    {
        Director.Instance.managerUI.SetCurrentPlayer( currentPlayer.ToString() );
        Debug.Log( "Current player: "+ currentPlayer.ToString() );
    }

    private void UpdateUIWithWinner()
    {
        Director.Instance.managerUI.SetWinner( currentPlayer, winnerCount[(int)Players.X], winnerCount[(int)Players.O] );
        Debug.Log( " + Winner: "+ currentPlayer.ToString() );
    }
    #endregion
}
