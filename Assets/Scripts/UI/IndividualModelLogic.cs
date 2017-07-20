using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 单独每个模型的管理属性
/// </summary>
public class IndividualModelLogic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ModelClass _Model = new ModelClass();
    private bool InitFlag;

    public List<string> MechanicalSoundNameList = new List<string>();
    public List<string> IntroductionSpeechNameList = new List<string>();

    private Coroutine materialTransformCoroutine;
    private Material originalMat;

    void Start()
    {
        List<ModelClass> engineList = DataHelper.ReadData();
        MechanicalSoundNameList = GlobalVar._IndividualModelManager.GetMechanicalSoundNameList();
        IntroductionSpeechNameList = GlobalVar._IndividualModelManager.GetIntroductionSpeechNameList();
        if (false == InitFlag)
        {
            if (engineList.Count > 0)
            {
                for (int i = 0; i < engineList.Count; i++)
                {
                    if (gameObject.name == engineList[i].Name)
                    {
                        _Model.Name = engineList[i].Name;
                        _Model.EndPos = engineList[i].EndPos;
                        _Model.IsMove = engineList[i].IsMove;
                        _Model.MechanicalSoundID = engineList[i].MechanicalSoundID;
                        _Model.IntroductionSpeechID = engineList[i].IntroductionSpeechID;
                        _Model.MoveModel = engineList[i].MoveModel;
                        _Model.GameObj = this.gameObject;
                    }
                }
            }
            else
            {
                _Model.Name = gameObject.name;
                _Model.EndPos = new Vector3(0, 0, 0);
                _Model.IsMove = false;
                _Model.MechanicalSoundID = 0;
                _Model.IntroductionSpeechID = 0;
                _Model.MoveModel = 0;
                _Model.GameObj = this.gameObject;
            }

            _Model.StartPos = gameObject.transform.position;
            InitFlag = true;
            GlobalVar._IndividualModelManager.OnPlay(this.gameObject);
            GlobalVar._IndividualModelManager.OnPlayMechanicalSound(this.gameObject);
            originalMat = new Material(this.GetComponent<MeshRenderer>().material);
        }
    }


    /// <summary>
    /// 鼠标进入零件模型
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        TransformIntoMaterial(GlobalVar.HighlightMat);
    }

    /// <summary>
    /// 鼠标离开零件模型
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        TransformIntoMaterial(this.originalMat);
    }

    private void TransformIntoMaterial(Material targetMat)
    {
        if (this.materialTransformCoroutine != null)
        {
            StopCoroutine(this.materialTransformCoroutine);
        }
        this.materialTransformCoroutine = StartCoroutine(TransformMaterialCoroutine(targetMat));
    }

    private IEnumerator TransformMaterialCoroutine(Material targetMat)
    {
        var material = this.GetComponent<MeshRenderer>().material;
        var startTime = Time.time;
        float deltaTime = 0;
        while (deltaTime < 1.1f)
        {
            material.Lerp(material, targetMat, deltaTime);
            yield return new WaitForEndOfFrame();
            deltaTime += Time.deltaTime * 3.0f;     // Transition speed
        }
        this.GetComponent<MeshRenderer>().material = targetMat;
    }

    /// <summary>
    /// 鼠标点击零件模型
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //var labeler = Instantiate(GlobalVar._AutoLable, this.transform.parent);
        //labeler.GetComponent<AutoLabel>().SetTextInfo(Engine.Name, "该零件暂无介绍");
        //var centerPoint = this.GetComponent<Renderer>().bounds.center - gameObject.transform.position;
        //labeler.transform.position = centerPoint;

        //var dir = centerPoint - (this.transform.position + 1f * Vector3.down);
        //var middlePoint = RayCastToPlaceHolder(centerPoint, dir);
        ////var middlePoint = centerPoint + dir.normalized * 0.1f;
        //var end = middlePoint + Vector3.up * 2f;

        ////this.controller.PlayLabelingAudio();
        //labeler.GetComponent<AutoLabel>().Label(centerPoint, middlePoint, end);

        //if (PlayState.Playing == OnPlayLogicState || PlayState.FallBack == OnPlayLogicState)
        //{
        //    OnPlayLogicState = PlayState.Paused;
        //}
    }

    private Vector3 RayCastToPlaceHolder(Vector3 centerPoint, Vector3 dir)
    {
        RaycastHit raycastHit;
        var ray = new Ray(centerPoint, dir);
        if (Physics.Raycast(ray, out raycastHit, 10.0f, 0x100))
        {
            return raycastHit.point;
        }
        Debug.Log("Missed raycast");
        return centerPoint + dir.normalized * 0.2f;
    }
}

