using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAlpha : TweenBase
{
    public override void Play()
    {
        if( debug ) Debug.Log( "Play on TweenAlpha: "+name );

    }
}
