using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(200)]
public class ShowCard : MonoBehaviour
{
    [SerializeField, Header("卡片")]
    private Image[] imgCards;

    private void Awake()
    {
        Show();
    }

    private void Show()
    {
        for (int i = 0; i < imgCards.Length; i++)
        {
            bool isGet = RandomCard.instance.picturesState[i];
            imgCards[i].sprite = isGet ? RandomCard.instance.pictures[i] : null;
            print(1);
        }
    }
}
