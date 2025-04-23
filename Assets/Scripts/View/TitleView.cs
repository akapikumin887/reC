using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour, IView
{
    [field: SerializeField]
    public Button startButton {  get; private set; }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void TransitionGameScreen()
    {
        // ‘JˆÚ‚ªŒ»ó‚ÍActive‚ÌØ‚è‘Ö‚¦‚¾‚¯‚¾‚ªA¶‚ÉˆÚ“®‚µ‚ÄÁ‚µ‚½‚¢
        gameObject.SetActive(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // Œ»ó‚Í‘Î‰–³‚µ
        if (disposing)
        {

        }
    }
}
