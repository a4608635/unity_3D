using UnityEngine;
using UnityEngine.Advertisements;

// C# 繼承僅限一個
// C# 介面無線多個
// 介面 interface,介面都是 I 開頭
// IUnityAdsListener 廣告監聽者 : 監聽玩家看廣告的行為，例如:失敗、略過、成功
public class AdManager : MonoBehaviour,IUnityAdsListener
{
    private string googleID = "3459578";                // Google ID 廣告
    private string placementRevival = "revival";        // 廣告名稱
    private pleyer pleyer;

    private void Start()
    {
        Advertisement.Initialize(googleID, false);       // 廣告.初始化(廣告ID,是否啟動測試)
        Advertisement.AddListener(this);                // 廣告.增加監聽者(此腳本)
        pleyer = FindObjectOfType<pleyer>();
    }

    /// <summary>
    /// 顯示復活廣告
    /// </summary>
    public void ShowADRevival()
    {
        if (Advertisement.IsReady(placementRevival))
        {
            Advertisement.Show(placementRevival);
            Advertisement.AddListener(this);
        }
    }

    // 廣告準備完成
    public void OnUnityAdsReady(string placementId)
    {
    }
    // 廣告錯誤
    public void OnUnityAdsDidError(string message)
    {
    }
    // 廣告開始
    public void OnUnityAdsDidStart(string placementId)
    {
    }
    // 廣告完成
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == placementRevival)        // 如果 目前廣告 = 復活廣告
        {
            switch (showResult)                     // switch 判斷式
            {
                case ShowResult.Failed:             // 第一種可能
                    //print("失敗");
                    break;
                case ShowResult.Skipped:            // 第二種可能
                    //print("略過");
                    break;
                case ShowResult.Finished:           // 第三種可能
                    //print("完成");
                    GameObject.Find("Fox").GetComponent<pleyer>().Revival();
                    break;
            }
        }
    }
}
