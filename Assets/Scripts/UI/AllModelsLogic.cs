using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

/// <summary>
/// 所有模型零件的UI逻辑部分
/// </summary>
public class AllModelsLogic : MonoBehaviour
{
    private Transform[] FatherModelObject;                                      //发电机所在的物体（父物体）

    private static List<ModelClass> ModelList = new List<ModelClass>();

    public static List<string> GroupName = new List<string>();                                //组名称集合

    public static List<string> NameDisplayModelList = new List<string>();                //名称显示方式集合
    public static List<string> MoveModelList = new List<string>();                       //渐入渐出方式集合

    void Start()
    {
        //找到父物体
        FatherModelObject = GlobalVar._FatherGameObject.GetComponentsInChildren<Transform>();
        GetMedolName();
    }

    /// <summary>
    /// 获取所有模型的个数
    /// </summary>
    /// <returns></returns>
    public int GetModelNum()
    {
        return ModelList.Count;
    }

    /// <summary>
    /// 获取组的个数，一共有多少组
    /// </summary>
    /// <returns></returns>
    public int GetGroupNum()
    {
        int groupNum = 1;
        groupNum = GroupName.Count;
        return groupNum;
    }

    /// <summary>
    /// 获取所有模型集合
    /// </summary>
    /// <returns></returns>
    public List<ModelClass> GetModelList()
    {
        return ModelList;
    }

    /// <summary>
    /// 初始化零件模型集合
    /// </summary>
    public void GetMedolName()
    {
        List<ModelClass> modelList = DataHelper.ReadData();
        foreach (Transform child in FatherModelObject)
        {
            if (null != child.GetComponent<MeshRenderer>())
            //if (child.name.StartsWith("NONE"))
            {
                child.gameObject.AddComponent<IndividualModelLogic>();
                //有数据缓存
                if (modelList.Count > 0)
                {
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        if (modelList[i].Name == child.gameObject.name)
                        {
                            ModelList.Add(new ModelClass(child.gameObject.name, child.gameObject, modelList[i].PlayOrderId, modelList[i].MoveModel, modelList[i].NameDisplay, modelList[i].GroupNum, child.gameObject.GetComponent<IndividualModelLogic>()._Model.StartPos, modelList[i].EndPos, PlayState.None));
                        }
                    }
                }
                else
                {
                    ModelList.Add(new ModelClass(child.gameObject.name, child.gameObject, 0, 0, 0, 0, child.gameObject.transform.position, new Vector3(0, 0, 0), PlayState.None));
                }
            }
        }
        ModelList.Sort();//升序
    }

    /// <summary>
    /// 程序退出，保存数据OnDisable和OnDistroy获取不到游戏物体
    /// </summary>
    void OnApplicationQuit()
    {
        foreach (Transform child in FatherModelObject)
        {
            if (null != child.GetComponent<MeshRenderer>())
            {
                for (int i = 0; i < ModelList.Count; i++)
                {
                    if (ModelList[i].Name == child.gameObject.name)
                    {
                        ModelList[i].EndPos = child.GetComponent<IndividualModelLogic>()._Model.EndPos;
                        ModelList[i].IsMove = child.GetComponent<IndividualModelLogic>()._Model.IsMove;
                        ModelList[i].MechanicalSoundID = child.GetComponent<IndividualModelLogic>()._Model.MechanicalSoundID;
                        ModelList[i].IntroductionSpeechID = child.GetComponent<IndividualModelLogic>()._Model.IntroductionSpeechID;
                    }
                }
            }
        }
        DataHelper.SaveData(ModelList);
    }
}

