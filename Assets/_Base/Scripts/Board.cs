using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    #region Variables
    // Logic
    private const int numberRows = 3;
    private const int numberColumns = 3;
    public BoardPosition[,] grid;
    // int[,] myArray = new int[4,2];

    // Components
    public Rigidbody model;
    [SerializeField] private BoardPosition[] gridRow1 = new BoardPosition[numberColumns];
    [SerializeField] private BoardPosition[] gridRow2 = new BoardPosition[numberColumns];
    [SerializeField] private BoardPosition[] gridRow3 = new BoardPosition[numberColumns];

    // Rotation on click
    private float rotationForce = 500f;
    private float angularDrag = 5f;

    // Correction lerp
    private float lerpingThreshold = 5f; // this is the angular velocity value under which we will lerp
    private float lerpingSpeed = 5f;
    private Quaternion initialRotationValue;

    // Other effects
    private TweenShake tweenShake;

    // Sound effects
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] sfxExplosion;

    #endregion


    #region Monobehaviour
    void Awake()
    {
        Director.Instance.board = this;
    }

    void Start()
    {
        initialRotationValue = model.transform.rotation; // save the initial rotation
        model.angularDrag = angularDrag;
        tweenShake = GetComponent<TweenShake>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CorrectPosition();
    }
    #endregion


    #region Private

    private void CorrectPosition()
    {
        if( model.angularVelocity.magnitude < lerpingThreshold )
        {
            // If I use Time.time it will lerp faster the longer the game goes on
            model.transform.rotation = Quaternion.Slerp( model.transform.rotation, initialRotationValue, Time.deltaTime * lerpingSpeed );
            //Debug.Log( Quaternion.Angle( transform.rotation, originalRotationValue ) );
            //Debug.Log( GetComponent<Rigidbody>().angularVelocity.magnitude );
        }
    }

    private void PopulateBoard()
    {
        grid = new BoardPosition[numberRows, numberColumns];

        grid[0, 0] = gridRow1[0];
        grid[0, 1] = gridRow1[1];
        grid[0, 2] = gridRow1[2];

        grid[1, 0] = gridRow2[0];
        grid[1, 1] = gridRow2[1];
        grid[1, 2] = gridRow2[2];

        grid[2, 0] = gridRow3[0];
        grid[2, 1] = gridRow3[1];
        grid[2, 2] = gridRow3[2];

        for( int i = 0; i < numberRows; i++ )
        {
            for( int j = 0; j < numberColumns; j++ )
            {
                grid[i, j].Set( GameManager.Players.None );
            }
        }
    }

    private void PlaySoundExplosion()
    {
        audioSource.clip = sfxExplosion[Random.Range( 0, sfxExplosion.Length )];
        audioSource.Play();
    }
    #endregion

    #region Public
    public void Reset()
    {
        PopulateBoard();

    }

    public void PressOnPosition( int row, int column, GameManager.Players player )
    {
        //Debug.Log( string.Format( "We pressed on the position ({0},{1})", row, column ) );

        // Calculate the position
        Vector3 position = grid[row, column].transform.localPosition * rotationForce;
        //Debug.Log( string.Format( "Original position ({0},{1},{2})", position.x, position.y, position.z ) );

        // For some reason I have to "rotate" the X and Y axis for it to properly align with the model
        Vector3 correctedPosition = Quaternion.Euler( 0, 90, 0 ) * position;
        // Debug.Log( string.Format( "Corrected position ({0},{1},{2})", correctedPosition.x, correctedPosition.y, correctedPosition.z ) );

        // Apply the torque
        model.AddTorque( correctedPosition );

        // Set grid place
        grid[row, column].Set( player );

        // Play effects
        PlaySoundExplosion();
        grid[row, column].PlayEffects();
        tweenShake.Play();

        // I-ll save this snippet for special considerations
        //switch (row)
        //{
        //    default:
        //    case 1:
        //        if (column == 1)
        //        {
        //        }
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //}
    }
    #endregion
}
