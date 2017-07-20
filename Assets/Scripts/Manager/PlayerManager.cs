using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 播放处理类
/// </summary>
public class PlayerManager : MonoBehaviour
{
    private static int OnPlayNum = -1;                      //正在播放的零件num
    private static int OnPlayGroup;                         //正在播放的组的num

    /// <summary>
    /// 播放下一个
    /// </summary>
    public void OnPlayNextManager()
    {
        if (OnPlayNum < GlobalVar._AllModelsLogic.GetModelNum())
        {
            OnPlayNum++;
            if (null != GetPlayerGameObject(OnPlayNum))
            {
                if (GetPlayerGameObject(OnPlayNum).GetComponent<IndividualModelLogic>()._Model.OnPlayState == PlayState.None || GetPlayerGameObject(OnPlayNum).GetComponent<IndividualModelLogic>()._Model.OnPlayState == PlayState.Stop)
                {
                    GetPlayerGameObject(OnPlayNum).GetComponent<IndividualModelLogic>()._Model.OnPlayState = PlayState.Playing;
                    if (OnPlayNum + 1 < GlobalVar._AllModelsLogic.GetModelNum() && null != GetPlayerGameObject(OnPlayNum + 1) && true == GetPlayerGameObject(OnPlayNum + 1).GetComponent<IndividualModelLogic>()._Model.IsMove)
                    {
                        OnPlayNextManager();
                    }
                }
                else
                {
                    OnPlayNum--;
                }
            }
        }
    }

    /// <summary>
    /// 播放上一个
    /// </summary>
    public void OnPlayLastManager()
    {
        if (OnPlayNum > -1)
        {
            if (null != GetPlayerGameObject(OnPlayNum))
            {
                if (GetPlayerGameObject(OnPlayNum).GetComponent<IndividualModelLogic>()._Model.OnPlayState == PlayState.None || GetPlayerGameObject(OnPlayNum).GetComponent<IndividualModelLogic>()._Model.OnPlayState == PlayState.Stop)
                {
                    GetPlayerGameObject(OnPlayNum).GetComponent<IndividualModelLogic>()._Model.OnPlayState = PlayState.FallBack;
                }
                else
                {
                    OnPlayNum++;
                }
            }
            bool onPlayLastFlag = false;
            if (OnPlayNum > -1 && null != GetPlayerGameObject(OnPlayNum) && true == GetPlayerGameObject(OnPlayNum).GetComponent<IndividualModelLogic>()._Model.IsMove)
            {
                onPlayLastFlag = true;
            }
            OnPlayNum--;
            if (true == onPlayLastFlag)
            {
                onPlayLastFlag = false;
                OnPlayLastManager();
            }
        }
    }

    /// <summary>
    /// 播放单独模型零件
    /// </summary>
    /// <param name="index"></param>
    /// <param name="onPlayState">拆/装</param>
    public void OnPlayIndividualModel(int index, PlayState onPlayState)
    {
        index = 1;
        onPlayState = PlayState.Playing;
        if (null != GetPlayerGameObject(index))
        {
            if (GetPlayerGameObject(index).GetComponent<IndividualModelLogic>()._Model.OnPlayState == PlayState.None || GetPlayerGameObject(index).GetComponent<IndividualModelLogic>()._Model.OnPlayState == PlayState.Stop)
            {
                GetPlayerGameObject(index).GetComponent<IndividualModelLogic>()._Model.OnPlayState = onPlayState;
            }
        }
    }

    /// <summary>
    /// 根据播放序号，找到播放相应的播放类的实例
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayerGameObject(int onPlayNum)
    {
        GameObject _GameObject = null;
        List<ModelClass> list = GlobalVar._AllModelsLogic.GetModelList();
        if (list.Count > onPlayNum && -1 != onPlayNum)
        {
            _GameObject = list[onPlayNum].GameObj;
        }
        return _GameObject;
    }
}
