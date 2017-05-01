using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPosition : MonoBehaviour
{

    public enum Value
    {
        None,
        O,
        X
    }
    public Value currentValue { private set; get; }

    public int row;// { private set; get; }
    public int column;// { private set; get; }


    //public delegate void Delegate();
    //public Delegate OnPressed;
    public GameObject boardCubePrefab;
    private GameObject boardCubeInstance;
    private ParticleSystem particles;

    new private Renderer renderer;
    public Material materialBase;
    public Material materialX;
    public Material materialO;

    // Use this for initialization
    void Start()
    {
        // Get position
        //row = System.Convert.ToInt32( transform.parent.name );
        //row = int.Parse( transform.parent.name ) - 1;
        //column = System.Convert.ToInt32( transform.name );
        //column = int.Parse( transform.name ) - 1;

        // Get components

        boardCubePrefab = Instantiate( boardCubePrefab, transform );
        particles = GetComponentInChildren<ParticleSystem>();
        renderer = GetComponentInChildren<Renderer>();
        // Initialize
        Set( Value.None );

        // Debug.Log( string.Format( "Setting position({0},{1}) with value {2}", row, column, currentValue ) );
    }

    public void Set( Value to )
    {
        currentValue = to;

        switch( currentValue )
        {
            default:
            case Value.None:
                renderer.material = materialBase;
                break;
            case Value.X:
                renderer.material = materialX;
                break;
            case Value.O:
                renderer.material = materialO;
                break;
        }
    }

    /*
    void OnMouseDown()
    {
                Debug.Log( "This is BoardPosition" + name );
    }
    */


    public void PlayEffects()
    {
        if( particles  != null )
        {
            particles.Play();
        }
    }
}
