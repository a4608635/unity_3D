using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameObject skill;        // 隨機技能 (遊戲物件)
    public GameObject objLight;     // 光照 (遊戲物件)

    [Header("是否自動顯示技能")]
    public bool autoShowSkill;      // 是否顯示技能
    [Header("是否自動開門")]
    public bool autoOpenDoor;       // 是否自動開門
    [Header("復活畫面，看廣告復活按鈕")]
    public GameObject panelRevival;
    public Button btnRevival;

    private Animator aniDoor;       // 門 (動畫)
    private Image imgCross;         // 轉場
    private AdManager adMnager;     // 廣告管理器

    private void Start()
    {
        // GameObject.find("") 無法找到隱藏的物件
        aniDoor = GameObject.Find("門").GetComponent<Animator>();
        imgCross = GameObject.Find("轉場效果").GetComponent<Image>();
        // 如果 是 顯示技能 呼叫 顯示技能方法
        if (autoShowSkill) Skill();

        // 如果 是 自動開門 延遲呼叫 開門方法
        if (autoOpenDoor) Invoke("OpenDoor", 5);

        // 延遲調用("方法名稱",延遲時間)
        // Invoke("OpenDoor",0,1.5F);

        // 重複調用("方法名稱",延遲時間,重複頻率
        // InvokeRepeating("OpenDoor"0,1.5F);

        adMnager = FindObjectOfType<AdManager>();                   // 透過類行尋找物件<廣告管理器>
        btnRevival.onClick.AddListener(adMnager.ShowADRevival);     // 按鈕.點擊.增加監聽者(廣告管理器.顯示復活廣告)

    }

    /// <summary>
    /// 顯示技能
    /// </summary>
    private void Skill()
    {
        //skill.SetActive(true);
    }
    /// <summary>
    /// 顯示燈光、開門
    /// </summary>
    private void OpenDoor()
    {
        objLight.SetActive(true);
        aniDoor.SetTrigger("開門處發");
    }
    /// <summary>
    /// 載入下一關
    /// </summary>
    /// <returns></returns>
    public IEnumerator NextLevel()
    {
        print("載入下一關");

        for (int i = 0; i < 50; i++)
        {
            imgCross.color += new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene("關卡2");
    }
    /// <summary>
    /// 顯示復活畫面
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowRevival()
    {
        panelRevival.SetActive(true);
        Text textSecond = panelRevival.transform.GetChild(1).GetComponent<Text>();

        for (int i = 3; i > 0; i--)
        {
            textSecond.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }
    /// <summary>
    /// 關閉復活畫面
    /// </summary>
    public void HideRevival()
    {
        StopCoroutine(ShowRevival());       // 停止協程
        panelRevival.SetActive(false);
    }
    /// <summary>
    /// 過關：開門、叫金幣前往玩家位置
    /// </summary>
    public void Pass()
    {
        OpenDoor();                                     // 呼叫開門

        Item[] items = FindObjectsOfType<Item>();       // 取得所有道具

        for (int i = 0; i < items.Length; i++)          // 迴圈跑每一個道具
        {
            items[i].pass = true;                       // 每個道具.過關 = 勾選
        }
    }


}
