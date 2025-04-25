using UnityEngine;

public class ResultView : MonoBehaviour, IView
{
    public void Initialize()
    {

    }

    public void TransitionScene(bool flag)
    {
        gameObject.SetActive(flag);
    }

    public void Dispose()
    {

    }
}
