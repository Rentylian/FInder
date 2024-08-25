using UnityEngine;
using UnityEngine.UI;

public class HintUi : MonoBehaviour
{
    [SerializeField] private Button _hintButton;
    [SerializeField] private GameObject _hintContainer;
    
    private void Awake()
    {
        _hintButton.onClick.AddListener(SwitchHint);
    }

    private void SwitchHint()
    {
        if (_hintContainer.activeInHierarchy)
        {
            _hintContainer.gameObject.SetActive(false);
        }
        else
        {
            _hintContainer.gameObject.SetActive(true);
        }
    }
}
