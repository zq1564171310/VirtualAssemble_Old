using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 零件模型控制类
/// </summary>
public class PlayerControl : MonoBehaviour
{
    public void OnPlyerControl(int index, PlayState onPlayState)
    {
        GlobalVar._PlayerCommon.OnPlyerCommon(index, onPlayState);
    }

    public void OnPlayNext()
    {
        GlobalVar._PlayerCommon.OnPlayNext();
    }

    public void OnPlayLast()
    {
        GlobalVar._PlayerCommon.OnPlayLast();
    }
}
