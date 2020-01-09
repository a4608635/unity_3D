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

    private Animator aniDoor;       // 門 (動畫)
    private Image imgCross;         // 轉場

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

        
    }

    /// <summary>
    /// 顯示技能
    /// </summary>
    private void Skill()
    {
        skill.SetActive(true);
    }
    /// <summary>
    /// 顯示燈光、開門
    /// </summary>
    private void OpenDoor()
    {
        objLight.SetActive(true);
        aniDoor.SetTrigger("開門處發");
    }

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

}
