using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion3D : MonoBehaviour
{
    [SerializeField] Transform target;

    public enum Axis
    {
        X,
        Y,
        Z
    }
    public Axis axis = Axis.Y;

    [SerializeField] private float speed = 0.3f;
    [SerializeField] private float radius = 5f;
    //private Vector2 center;
    public Vector3 center = Vector3.zero;

    private float _angle;

    private void Start()
    {
        // _centre = transform.position;
        //center = Vector2.zero;
        //radius = 5f;
        //speed = 0.4f;
    }

    private void Update()
    {

        _angle += speed * Time.deltaTime;

        Vector3 offset = Vector3.zero;
        if( axis == Axis.X )
        {
            offset = new Vector3( Mathf.Sin( _angle ), Mathf.Cos( _angle ), 0f ) * radius; // X:Mathf.Sin( _angle ), Y:Mathf.Cos( _angle )
        }
        else if( axis == Axis.Y )
        {
            offset = new Vector3( Mathf.Sin( _angle ), 0f, Mathf.Cos( _angle ) ) * radius; // X:Mathf.Sin( _angle ), Z:Mathf.Cos( _angle )

        }
        else //if( axis == Axis.Z )
        {

        }
        transform.position = center + offset;


        transform.LookAt( target );
    }
}
