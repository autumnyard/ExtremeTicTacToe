using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenShake : TweenBase
{
    public float time = 1f;
    public Vector3 strength = Vector3.zero;
    public int vibrato = 100;



    public override void Play()
    {
        if( debug ) Debug.Log( "Play on TweenShake: "+name );
        transform.DOShakePosition( time, strength, vibrato );
    }
}
