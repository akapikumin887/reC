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
        // 遷移が現状はActiveの切り替えだけだが、左に移動して消したい
        obj.SetActive(!obj.activeSelf);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // 現状は対応無し
        if (disposing)
        {

        }
    }
}
