using UnityEngine;
using DG.Tweening;

public class TweenRotation : TweenBase
{
    public float rotationInit = -20f;
    public float rotationFinish = 20f;
    public float time = 1f;
    public Ease ease;
    public bool loop = false;
    public LoopType loopType;


    public override void Play()
    {
        if( debug ) Debug.Log( "Play on TweenRotation: "+name );
        int loops = (loop) ? -1 : 0;
        // if (rotationInit != -1)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotationInit);
        }
        transform.DORotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotationFinish), time)
                      .SetEase(ease)
                      .SetLoops(loops, loopType);
    }
}
