using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 게임세팅데이터
    // public GameSetting gameSetting; // 객체 생성
    // 
    // private void Start()
    // {
    //     gameSetting = Resources.Load<GameSetting>("GameSettingAsset");
    // 
    //     Debug.Log("사운드 볼륨 : " + gameSetting.soundVolume);
    //     Debug.Log("튜토리얼 표시 여부 : " + gameSetting.showTutorial);
    // }
    #endregion

    #region 프리팹을 경로로 찾아서 생성
    // public GameSetting resourcePath;
    // 
    // private void Start()
    // {
    //     resourcePath = Resources.Load<GameSetting>("ResourcesPathAsset");
    // 
    //     GameObject characterPrefab = Resources.Load<GameObject>(resourcePath.characterPrefabPath);
    //     Instantiate(characterPrefab);
    // }
    #endregion

}
