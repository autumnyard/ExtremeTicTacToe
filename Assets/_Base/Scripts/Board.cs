using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    private const int numberRows = 3;
    private const int numberColumns = 3;
    // int[,] myArray = new int[4,2];
    public BoardPosition[,] grid;
    [SerializeField] private BoardPosition[] gridRow1 = new BoardPosition[numberColumns];
    [SerializeField] private BoardPosition[] gridRow2 = new BoardPosition[numberColumns];
    [SerializeField] private BoardPosition[] gridRow3 = new BoardPosition[numberColumns];

    public Rigidbody model;

    private Vector3 initRotation;

    private void Awake()
    {
        Director.Instance.board = this;
    }
    void Start()
    {
        PopulateBoard();
        initRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        CorrectPosition();
    }

    private void CorrectPosition()
    {

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
    }

    public void PressOnPosition( int row, int column )
    {
        //Debug.Log( string.Format( "We pressed on the position ({0},{1})", row, column ) );

        // Apply torque
        Vector3 withForce = new Vector3( 0f, 0f, 10f );
        Debug.Log( string.Format( "Applying torque at position ({0},{1},{2})",
            transform.localPosition.x, transform.localPosition.y, transform.localPosition.z ) );
        AddTorqueAtPosition( model, Vector3.one, grid[row, column].transform.localPosition );

        switch (row)
        {

            default:
            case 1:
                if (column == 1)
                {
                }
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    public Vector3 OrthoNormalize( Vector3 vector1, Vector3 vector2 )
    {
        vector1.Normalize();
        Vector3 temp = Vector3.Cross( vector1, vector2 );
        temp.Normalize();
        vector2 = Vector3.Cross( temp, vector2 );
        return vector2;
        //var output = new Vector3[] {vector1, vector2};
        //return output;
    }


    public void AddTorqueAtPosition( Rigidbody thisRigidbody, Vector3 torque, Vector3 position )
    {
        //http://forum.unity3d.com/threads/torque-at-offset.187297/
        Vector3 torqueAxis = torque.normalized;
        Vector3 ortho = new Vector3( 1, 0, 0 );
        // prevent torqueAxis and ortho from pointing in the same direction
        if ((torqueAxis - ortho).sqrMagnitude < float.Epsilon)
        {
            ortho = new Vector3( 0, 1, 0 );
        }

        var orthoNorm = OrthoNormalize( torqueAxis, ortho );


        // calculate force
        Vector3 force = Vector3.Cross( 0.5f * torque, orthoNorm );
        thisRigidbody.AddForceAtPosition( force, position + orthoNorm );
        thisRigidbody.AddForceAtPosition( -force, position - orthoNorm );

    }
}
