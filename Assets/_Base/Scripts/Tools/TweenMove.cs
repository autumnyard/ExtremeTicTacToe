using UnityEngine;
using DG.Tweening;

public class TweenMove : TweenBase
{

    //public float positionInit = -1f;
    //public float positionFinish = 2f;
    public Vector3 positionShift;
    public float time = 1f;
    public Ease ease;
    public bool loop = false;
    public LoopType loopType;


    // Relative movement
    private Vector3 initPosition;
    private Vector3 alteredPosition;

    private void Awake()
    {
        initPosition = transform.position;
        alteredPosition = transform.position + positionShift;
    }


    public override void Play()
    {
        if( debug ) Debug.Log( "Play on TweenMove: "+name );

        int loops = (loop) ? -1 : 0;
        tween = transform.DOMove( alteredPosition, time )
                      .SetEase( ease )
                      .SetLoops( loops, loopType );
    }

    public override void PlayReverse()
    {
        if( debug ) Debug.Log( "PlayReverse on TweenMove: "+name );

        int loops = (loop) ? -1 : 0;
        tween = transform.DOMove( initPosition, time )
                      .SetEase( ease )
                      .SetLoops( loops, loopType );
    }

}
