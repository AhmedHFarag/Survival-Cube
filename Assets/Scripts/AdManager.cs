//using UnityEngine;
//using System.Collections;
//using UnityEngine.Advertisements;

//public class AdManager : MonoBehaviour
//{
//    string gameID = "1140954";
//    // Use this for initialization
//    void Awake()
//    {
//        Advertisement.Initialize(gameID, true);
//      //  StartCoroutine(WaitForAds());
//    }
////    public void ShowAd()
////    {
////#if UNITY_EDITOR
////        StartCoroutine(WaitForAds());
////#endif
////        if (Advertisement.IsReady())
////        {
////            Advertisement.Show();
////        }
////    }

//    public void ShowAd(string ID="")
//    {
//#if UNITY_EDITOR
//        StartCoroutine(WaitForAds());
//#endif
//        if (string.Equals(ID, ""))
//            ID = null;
//        if (Advertisement.IsReady(ID))
//        {
//            Advertisement.Show(ID);
//        }
//    }


//    IEnumerator WaitForAds()
//    {
//        float currentTimeScale = Time.timeScale;
//        Time.timeScale = 0f;
//        yield return null;
//        while (Advertisement.isShowing)
//            yield return null;
//        Time.timeScale = currentTimeScale;
//    }
//    // Use this for initialization

//}
