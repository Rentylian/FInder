using UnityEngine;
using UnityEngine.UI;

public class LifeUi : MonoBehaviour
{
    [SerializeField] private Image[] _lifes;

    public void SetCountLife(int countLife)
    {
        if (countLife > _lifes.Length)
        {
            countLife = _lifes.Length;
        }

        SwitchActivityLifes(false);
        SwitchActivityLifes(true, countLife);
    }

    private void SwitchActivityLifes(bool enable, int countLife = 0)
    {
        countLife = countLife == 0 ? _lifes.Length : countLife;
        for (int i = 0; i < countLife; i++)
        {
            _lifes[i].gameObject.SetActive(enable);
        }
    }
}
