using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagerCamera : MonoBehaviour
{

    public enum Cameras
    {
        Static = 0,
        Motion
    }
    public enum Transition
    {
        Straight = 0,
        Smooth
    }

    // Camera work
    [SerializeField] Camera[] cameras;
    [SerializeField] Transform[] camerasT;// debug
    Vector3 initPositionGame;
    Vector3 initRotationGame;
    Vector3 initPositionScore;

    #region Monobehaviour
    void Awake()
    {
        Director.Instance.managerCamera = this;
    }

    private void Start()
    {
        initPositionGame = camerasT[0].transform.position;
        initRotationGame = camerasT[0].transform.eulerAngles;
        initPositionScore = camerasT[1].transform.position;
    }
    #endregion

    private void MoveSmoothlyTo( Transform what, Vector3 newPosition, float time = 1f )
    {
        what.DOMove( newPosition, time );
    }

    private void MoveSmoothlyTo( Transform what, Vector3 newPosition, TweenCallback method, float time = 1f )
    {
        what.DOMove( newPosition, time ).OnComplete( method );
    }

    public void SwitchToCamera( Cameras to )
    {
        switch( to )
        {
            default:
            case Cameras.Static:
                // Old method with 2 cameras
                //cameras[(int)Cameras.Motion].gameObject.SetActive( false );
                //cameras[(int)Cameras.Static].gameObject.SetActive( true );

                // New method with 1 camera and 2 transforms
                DisableCircularMotion();
                //cameras[0].transform.position = initPositionGame;
                MoveSmoothlyTo( cameras[0].transform, initPositionGame );
                cameras[0].transform.eulerAngles = initRotationGame;
                break;

            case Cameras.Motion:
                // New method with 1 camera and 2 transforms
                //cameras[0].transform.position = initPositionScore;
                EnableCircularMotion();
                //MoveSmoothlyTo( cameras[0].transform, initPositionScore, EnableCircularMotion );

                // Old method with 2 cameras
                //cameras[(int)Cameras.Static].gameObject.SetActive( false );
                //cameras[(int)Cameras.Motion].gameObject.SetActive( true );
                break;
        }
    }

    private void EnableCircularMotion()
    {
        cameras[0].GetComponent<CircularMotion3D>().enabled = true;
    }

    private void DisableCircularMotion()
    {
        cameras[0].GetComponent<CircularMotion3D>().enabled = false;
    }
}
