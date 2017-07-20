using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 零件模型类
/// </summary>
public class ModelClass : IComparable<ModelClass>
{
    public string Name;                             //模型的名字
    public GameObject GameObj;                      //该零件对应的物体
    public int PlayOrderId;                         //组内播放序列号
    public int MoveModel;                           //渐入渐出方式
    public int NameDisplay;                         //名称显示方式
    public int GroupNum;                            //所在组
    public Vector3 EndPos;                          //目标位置
    public Vector3 StartPos;                        //源位置 
    public Texture2D PlayBtnImage;                  //播放按钮状态图片
    public PlayState OnPlayState;                   //播放状态
    public float OnPlayTime;                        //播放时间
    public float OnPlayTotalTime = 3;                   //播放总时间
    public bool IsMove;                             //是否需要移动
    public int MechanicalSoundID;                   //拆卸或组装发出声音的音频Id，不发声音，id=0
    public int IntroductionSpeechID;                //语音介绍对应的语音ID，没有语音，id =0

    public ModelClass()
    {

    }

    /// <summary>
    /// 总控制面板需要配置的参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="playOrderId"></param>
    /// <param name="moveModel"></param>
    /// <param name="nameDisplay"></param>
    /// <param name="groupNum"></param>
    public ModelClass(string name, GameObject gameObj, int playOrderId, int moveModel, int nameDisplay, int groupNum, Vector3 startPos, Vector3 endPos, PlayState onPlayState)
    {
        Name = name;
        GameObj = gameObj;
        PlayOrderId = playOrderId;
        MoveModel = moveModel;
        NameDisplay = nameDisplay;
        GroupNum = groupNum;
        StartPos = startPos;
        EndPos = endPos;
        OnPlayState = onPlayState;
    }

    /// <summary>
    /// 用于保存所有数据
    /// </summary>
    /// <param name="name"></param>
    /// <param name="playOrderId"></param>
    /// <param name="moveModel"></param>
    /// <param name="nameDisplay"></param>
    /// <param name="groupNum"></param>
    /// <param name="endPos"></param>
    /// <param name="startPos"></param>
    /// <param name="onPlayState"></param>
    /// <param name="isMove"></param>
    /// <param name="mechanicalSoundID"></param>
    /// <param name="introductionSpeechID"></param>
    public ModelClass(string name, int playOrderId, int moveModel, int nameDisplay, int groupNum, Vector3 endPos, Vector3 startPos, PlayState onPlayState, bool isMove, int mechanicalSoundID, int introductionSpeechID)
    {
        Name = name;
        PlayOrderId = playOrderId;
        MoveModel = moveModel;
        NameDisplay = nameDisplay;
        GroupNum = groupNum;
        EndPos = endPos;
        StartPos = startPos;
        OnPlayState = onPlayState;
        IsMove = isMove;
        MechanicalSoundID = mechanicalSoundID;
        IntroductionSpeechID = introductionSpeechID;
    }


    int IComparable<ModelClass>.CompareTo(ModelClass other)
    {
        if (null == other)
        {
            return 1;//空值比较大，返回1
        }

        //等于返回0
        int re = this.GroupNum.CompareTo(other.GroupNum);
        if (0 == re)
        {
            //id相同再比较Name
            return this.PlayOrderId.CompareTo(other.PlayOrderId);
        }
        return re;
    }
}

/// <summary>
/// 零件播放状态
/// </summary>
public enum PlayState
{
    None,                   //默认状态，没有零件播放
    Playing,                //零件正在移动
    FallBack,               //零件回退
    Paused,                 //零件在移动过程中被点击而停止在该位置
    Stop,                   //零件停止结束移动
}