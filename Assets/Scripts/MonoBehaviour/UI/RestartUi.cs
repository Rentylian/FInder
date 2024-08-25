using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartUi : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    public Action RestartClick; 
        
    private void Awake()
    {
        _restartButton.onClick.AddListener(() => RestartClick.Invoke());
    }

}
