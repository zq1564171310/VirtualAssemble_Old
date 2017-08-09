using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceClick : MonoBehaviour
{
    #region 字段

    private string lastclip = "open";

    private Animation anim;
    public Button next, back;

    public Button testclose, testopen;

    #endregion


    #region 属性
    #endregion

    #region Unity回调
    private void Start()
    {
        anim = GetComponent<Animation>();
        next.onClick.AddListener(() => { VoiceCommand("next"); });
        back.onClick.AddListener(() => { VoiceCommand("back"); });

        testclose.onClick.AddListener(() => { VoiceCommand("close"); });
        testopen.onClick.AddListener(() => { VoiceCommand("open"); });
    }
    #endregion


    #region 事件回调
    #endregion


    #region 帮助方法
    public void VoiceCommand(string command)
    {
        switch (command)
        {
            case "next":
                GlobalVar._PlayerManager.OnPlayNextManager();
                break;
            case "back":
                GlobalVar._PlayerManager.OnPlayLastManager();
                break;
            case "close":
            case "open":
                PlayAnimation(command);
                //anim.Play("Open");
                break;
            default:
                break;
        }
    }



    void PlayAnimation(string command)
    {
        if (lastclip != command)
        {
            anim.Play(command);
            lastclip = command;
        }
    }

    #endregion


    #region 接口实现
    #endregion

}
