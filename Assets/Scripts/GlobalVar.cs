using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局变量
/// </summary>
public class GlobalVar
{
    //脚本实例话
    public static AllModelsLogic _AllModelsLogic = GameObject.Find("Models").GetComponent<AllModelsLogic>(); //全部模型管理类的实例
    public static IndividualModelManager _IndividualModelManager = GameObject.Find("PlayerPrefab").GetComponent<IndividualModelManager>(); //全部模型管理类的实例
    public static PlayerCommon _PlayerCommon = GameObject.Find("PlayerPrefab").GetComponent<PlayerCommon>();
    public static PlayerControl _PlayerControl = GameObject.Find("PlayerPrefab").GetComponent<PlayerControl>();
    public static PlayerManager _PlayerManager = GameObject.Find("PlayerPrefab").GetComponent<PlayerManager>();

    //物体
    public static GameObject _FatherGameObject = GameObject.Find("Models");                                  //父物体对象
    public static GameObject _AutoLable = GameObject.Find("AutoLabel");

    //音频
    public static AudioSource _AudioSource = _FatherGameObject.GetComponent<AudioSource>();                  //音频源

    //材质/贴图
    public static Texture2D CustomInspectorButPic1 = (Texture2D)Resources.Load("1");                         //单个零件播放移动按钮材质
    public static Texture2D CustomInspectorButPic2 = (Texture2D)Resources.Load("2");                         //单个零件停止移动按钮材质
    public static Material HighlightMat = (Material)Resources.Load("HighlightMat");                          //高亮材质
    public static Texture2D NameLableBackGroundPic = (Texture2D)Resources.Load("frameAndBorder02");          //显示框背景图片
}
