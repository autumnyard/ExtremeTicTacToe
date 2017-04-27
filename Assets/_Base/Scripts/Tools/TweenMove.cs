using UnityEngine;
using DG.Tweening;

public class TweenMove : MonoBehaviour {

    public bool playOnStart = true;
    //public float positionInit = -1f;
    //public float positionFinish = 2f;
    public Vector3 positionShift;
    public float time = 1f;
    public Ease ease;
    public bool loop = false;
    public LoopType loopType;

    Tweener tween;

    // Use this for initialization
    void Start()
    {
        if (playOnStart)
        {
            Play();
        }
    }


    public void Play()
    {
        int loops = (loop) ? -1 : 0;
        Vector3 positionTotal =transform.position + positionShift;
        tween = transform.DOMove( positionTotal, time)
                      .SetEase(ease)
                      .SetLoops(loops, loopType);
    }

    public void Stop()
    {
        tween.Kill();
    }
}
