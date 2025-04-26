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
        // �J�ڂ������Active�̐؂�ւ����������A���Ɉړ����ď�������
        gameObject.SetActive(flag);
    }

    private void OnDestroy()
    {
        
    }
}
