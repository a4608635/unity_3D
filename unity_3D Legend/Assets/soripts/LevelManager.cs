using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject skill;        // 隨機技能 (遊戲物件)
    public GameObject objLight;     // 光照 (遊戲物件)



    [Header("是否自動顯示技能")]
    public bool autoShowSkill;
    [Header("是否自動開門")]
    public bool autoOpenDoor;

    private Animator aniDoor;       // 門 (動畫)

    private void Start()
    {
        // GameObject.find("") 無法找到隱藏的物件
        aniDoor = GameObject.Find("門").GetComponent<Animator>();

        // 如果 是 顯示技能 呼叫 顯示技能方法
        if (autoShowSkill) Skill();

        // 如果 是 自動開門 延遲呼叫 開門方法
        if (autoOpenDoor) Invoke("OpenDoor", 6);

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

}
