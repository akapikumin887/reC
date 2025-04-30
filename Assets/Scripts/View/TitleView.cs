using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour, IView
{
    [SerializeField]
    private GameObject scene;

    [SerializeField]
    private Button startButton;
    public Button _StartButton => startButton;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void TransitionScene(bool flag)
    {
        scene.transform.DOLocalMove(new Vector2(-110.0f, 0.0f), 0.5f).OnComplete(() => gameObject.SetActive(flag));
    }

    private void OnDestroy()
    {
        
    }
}
