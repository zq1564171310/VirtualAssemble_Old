using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 零件模型播放类
/// </summary>
public class PlayerCommon : MonoBehaviour
{
    public void OnPlyerCommon(int index, PlayState onPlayState)
    {
        GlobalVar._PlayerManager.OnPlayIndividualModel(index, onPlayState);
    }

    public void OnPlayNext()
    {
        GlobalVar._PlayerManager.OnPlayNextManager();
    }

    public void OnPlayLast()
    {
        GlobalVar._PlayerManager.OnPlayLastManager();
    }
}
