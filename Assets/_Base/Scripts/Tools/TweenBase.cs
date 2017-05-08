using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenBase : MonoBehaviour
{
    [SerializeField] private bool playOnStart = true;
    [SerializeField] protected bool debug = false;
    protected Tweener tween;

    void Start()
    {
        if( playOnStart )
        {
            Play();
        }
    }

    public virtual void Play()
    {
        if( debug ) Debug.Log( "Play on TweenBase: "+name );
    }

    public virtual void PlayReverse()
    {
        if( debug ) Debug.Log( "PlayReverse on TweenBase: "+name );
    }

    public virtual void Stop()
    {
        if( tween  != null )
        {
            tween.Kill();
        }
    }
}
