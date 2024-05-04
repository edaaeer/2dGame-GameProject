using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SetGameButton : MonoBehaviour
{
    

     public enum EButtonType
     {
         NotSet,
         PairNumberBtn,
         PuzzleCategoryBtn,
     };

     [SerializeField] public EButtonType ButtonType = EButtonType.NotSet;
     [HideInInspector] public GameSettings.EPairNumber PairNumber = GameSettings.EPairNumber.NotSet;
     [HideInInspector] public GameSettings.EPuzzleCategories Puzzlecategories = GameSettings.EPuzzleCategories.NotSet;

    private string _sceneName = string.Empty;

    void Start()
    {

    }

    private void OnEnable()
    {
        UnityAdsManager.OnInterstitialVideoAdFailed += LoadNextScene;
        UnityAdsManager.OnInterstitialVideoAdSkipped += LoadNextScene;
        UnityAdsManager.OnInterstitialVideoAdCompleted += LoadNextScene;
    }

    private void OnDisable()
    {
        UnityAdsManager.OnInterstitialVideoAdFailed -= LoadNextScene;
        UnityAdsManager.OnInterstitialVideoAdSkipped -= LoadNextScene;
        UnityAdsManager.OnInterstitialVideoAdCompleted -= LoadNextScene;
    }

    private void LoadNextScene()
    {
        if(_sceneName.Length >0)
          SceneManager.LoadScene(_sceneName);
    }


    public void SetGameOption(string GameSceneName)
     {
         var comp = gameObject.GetComponent<SetGameButton>();
        
         switch (comp.ButtonType)
         {
             case SetGameButton.EButtonType.PairNumberBtn:
                 GameSettings.Instance.SetPairNumber(comp.PairNumber);
                 break;

             case EButtonType.PuzzleCategoryBtn:
                 GameSettings.Instance.SetPuzzleCategories(comp.Puzzlecategories);
                 break;

         }

         if (GameSettings.Instance.AllSettingsReady())
         {
            _sceneName = GameSceneName;
            UnityAdsManager.Instance.PlayInterstitialAd();
         }
     }


}
