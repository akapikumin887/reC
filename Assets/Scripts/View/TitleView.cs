using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour, IView
{
    [field: SerializeField]
    public GameObject self {  get; private set; }

    [field: SerializeField]
    public Button startButton {  get; private set; }

    public TitleView()
    {

    }

    public void Initialize()
    {

    }

    public void Uninitialize()
    {
        if (self.activeSelf)
            self.SetActive(!self.activeSelf);
    }

    public void TransitionGameScreen(GameObject obj)
    {
        // ‘JˆÚ‚ªŒ»ó‚ÍActive‚ÌØ‚è‘Ö‚¦‚¾‚¯‚¾‚ªA¶‚ÉˆÚ“®‚µ‚ÄÁ‚µ‚½‚¢
        obj.SetActive(!obj.activeSelf);
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
