using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("玩家資料")]
    public PleyerData dataPlayer;

    public void BuyHp_500()
    {
        dataPlayer.hpmax += 500;                // 生命最大值遞增
        dataPlayer.hp = dataPlayer.hpmax;       // 目前生命=生命最大值
    }

    public void BuyAtk_50()
    {
        dataPlayer.attack += 50;                // 攻擊力遞增
    }

    public void LoadLeve1()
    {
        dataPlayer.hp = dataPlayer.hpmax;
        SceneManager.LoadScene("關卡1");
    }
}
