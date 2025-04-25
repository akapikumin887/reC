using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour, IView
{
    [SerializeField]
    private Button startButton;
    public Button _StartButton => startButton;


    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void TransitionScene(bool flag)
    {
        // ‘JˆÚ‚ªŒ»ó‚ÍActive‚ÌØ‚è‘Ö‚¦‚¾‚¯‚¾‚ªA¶‚ÉˆÚ“®‚µ‚ÄÁ‚µ‚½‚¢
        gameObject.SetActive(flag);
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
