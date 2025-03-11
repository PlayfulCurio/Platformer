using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsScreen : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TMP_Text[] _buttonsTexts;

    private void Awake()
    {
        for (int i = 1; i <= GameManager.Instance.FurthestLevelComplete; i++)
        {
            _buttons[i].interactable = true;
            _buttonsTexts[i].color = Color.white;
        }
    }

    public void LoadLevel(int number) => GameManager.Instance.LoadLevel(number);
}
