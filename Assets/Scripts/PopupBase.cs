using UnityEngine;

public abstract class PopupBase : MonoBehaviour
{
    public PopupBase()
    {
        SetDefault();
    }

    public virtual AllPages CurrentPage { get; }

    public virtual bool IsActive { get; }

    public virtual void Open()
    {

    }

    public virtual void Close()
    {

    }

    protected virtual void SetDefault()
    {

    }

    public virtual void MoreGames()
    {

    }

    public virtual void ButtonClickSound()
    {
        
    }
}