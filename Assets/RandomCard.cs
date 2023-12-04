using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class RandomCard : MonoBehaviour
{
    public static RandomCard instance;

    [SerializeField, Header("所有日記圖片")]
    public Sprite[] pictures;

    [SerializeField, Header("是否拿到日記圖片")]
    public bool[] picturesState;

    private Image imgPicture;

    private void Awake()
    {
        instance = this;

        GetCardIsGetInformation();

        imgPicture = GameObject.Find("圖片").GetComponent<Image>();

        GetRandomPicture();                                                         // 開始遊戲時取得隨機圖片
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < picturesState.Length; i++)
            {
                PlayerPrefs.SetString("圖片" + i, "未拿到");
            }

            GetCardIsGetInformation();
        }
    }

    [ContextMenu("取得所有卡片是否獲得")]
    private void  GetCardIsGetInformation()
    {
        picturesState = new bool[pictures.Length];                                  // 新增全部的圖片狀態陣列 有幾張圖就有幾個狀態

        for (int i = 0; i < picturesState.Length; i++)                              // 使用迴圈跑所有的陣列資料，將拿過的設定為勾選狀態
        {
            picturesState[i] = PlayerPrefs.GetString("圖片" + i) == "已拿到";
        }
    }

    private void GetRandomPicture()
    {
        int indexRandom = Random.Range(0, pictures.Length);                         // 隨機獲得一張圖片

        // 拿狀態內還沒有獲得的卡片數量
        var pictureStateFalse = picturesState.Where(x => x == false);
        // 如果全部卡片都拿過 就 跳出
        if (pictureStateFalse.Count() == 0)
        {
            print("全部都拿過了");
            return;
        }

        while (PlayerPrefs.GetString("圖片" + indexRandom) == "已拿到")              // 不重複處理 while (true) { 程式 } - 當 () 布林值為 true 會持續執行
        {
            indexRandom = Random.Range(0, pictures.Length);
        }

        imgPicture.sprite = pictures[indexRandom];                                  // 更新圖片
        imgPicture.color = Color.white;                                             // 更新顏色 白色不透明

        PlayerPrefs.SetString("圖片" + indexRandom, "已拿到");                       // 設定該圖片為 已拿到
    }
}
