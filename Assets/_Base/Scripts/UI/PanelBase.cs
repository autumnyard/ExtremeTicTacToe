using UnityEngine;


public class PanelBase : MonoBehaviour
{
    private CanvasRenderer canvas;

    [SerializeField] private TweenBase[] tweens;
    //[SerializeField] private TweenBase[] tweensAppear;
    //[SerializeField] private TweenBase[] tweensDisappear;


    private void Awake()
    {
        canvas = GetComponent<CanvasRenderer>();

        if( canvas == null )
        {
            Debug.LogWarning( "UI menu without Unity Canvas: " + name );
        }
    }


    public void Show()
    {
        if( tweens == null )
        {
            ForceShow();
        }
        else
        {
            foreach( var tween in tweens )
            {
                tween.Play();
            }
        }
    }

    public void Hide()
    {
        if( tweens == null )
        {
            ForceHide();
        }
        else
        {
            foreach( var tween in tweens )
            {
                tween.PlayReverse();
            }
        }
    }


    public void ForceShow()
    {
        gameObject.SetActive( true );
    }

    public void ForceHide()
    {
        gameObject.SetActive( false );
    }
}
