using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualModelManager : MonoBehaviour
{
    public List<string> MechanicalSoundNameList = new List<string>();
    public List<AudioClip> MechanicalSoundList = new List<AudioClip>();

    public List<string> IntroductionSpeechNameList = new List<string>();
    public List<AudioClip> IntroductionSpeechList = new List<AudioClip>();

    void Start()
    {
        Object[] mechanicalSoundObjs = Resources.LoadAll("MechanicalSound");
        for (int i = 0; i < mechanicalSoundObjs.Length; i++)
        {
            MechanicalSoundList.Add((AudioClip)mechanicalSoundObjs[i]);
            MechanicalSoundNameList.Add(mechanicalSoundObjs[i].name);
        }

        Object[] IntroductionSpeechObjs = Resources.LoadAll("IntroductionSpeech");
        for (int i = 0; i < IntroductionSpeechObjs.Length; i++)
        {
            IntroductionSpeechList.Add((AudioClip)IntroductionSpeechObjs[i]);
            IntroductionSpeechNameList.Add(IntroductionSpeechObjs[i].name);
        }
    }

    public List<AudioClip> GetIntroductionSpeechList()
    {
        return IntroductionSpeechList;
    }

    public List<string> GetIntroductionSpeechNameList()
    {
        return IntroductionSpeechNameList;
    }

    public List<AudioClip> GetMechanicalSoundList()
    {
        return MechanicalSoundList;
    }

    public List<string> GetMechanicalSoundNameList()
    {
        return MechanicalSoundNameList;
    }

    /// <summary>
    /// 播放协程实现方法
    /// </summary>
    /// <param name="_GameObject"></param>
    /// <returns></returns>
    public Coroutine OnPlay(GameObject _GameObject)
    {
        return StartCoroutine(OnPlayIEnumerator(_GameObject));
    }

    public Coroutine OnPlayMechanicalSound(GameObject _GameObject)
    {
        return StartCoroutine(OnPlayMechanicalSoundIEnumerator(_GameObject));
    }

    /// <summary>
    /// 播放零件模型协程
    /// </summary>
    /// <returns></returns>
    IEnumerator OnPlayIEnumerator(GameObject _GameObject)
    {
        IndividualModelLogic _IndividualModelLogic = _GameObject.GetComponent<IndividualModelLogic>();
        while (true)
        {
            if (PlayState.Playing == _IndividualModelLogic._Model.OnPlayState)
            {
                if (_IndividualModelLogic._Model.OnPlayTime >= _IndividualModelLogic._Model.OnPlayTotalTime)
                {
                    _IndividualModelLogic._Model.OnPlayState = PlayState.Stop;
                    _IndividualModelLogic._Model.OnPlayTime = _IndividualModelLogic._Model.OnPlayTotalTime;
                }
                else
                {
                    _IndividualModelLogic._Model.OnPlayTime += Time.deltaTime;
                }
                _IndividualModelLogic._Model.GameObj.transform.position = Vector3.Lerp(_IndividualModelLogic._Model.StartPos, _IndividualModelLogic._Model.EndPos, _IndividualModelLogic._Model.OnPlayTime / _IndividualModelLogic._Model.OnPlayTotalTime);
            }
            else if (PlayState.FallBack == _IndividualModelLogic._Model.OnPlayState)
            {
                if (_IndividualModelLogic._Model.OnPlayTime <= 0)
                {
                    _IndividualModelLogic._Model.OnPlayState = PlayState.None;
                    _IndividualModelLogic._Model.OnPlayTime = 0;
                }
                else
                {
                    _IndividualModelLogic._Model.OnPlayTime -= Time.deltaTime;
                }
                _IndividualModelLogic._Model.GameObj.transform.position = Vector3.Lerp(_IndividualModelLogic._Model.StartPos, _IndividualModelLogic._Model.EndPos, _IndividualModelLogic._Model.OnPlayTime / _IndividualModelLogic._Model.OnPlayTotalTime);
            }
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator OnPlayMechanicalSoundIEnumerator(GameObject _GameObject)
    {
        IndividualModelLogic _IndividualModelLogic = _GameObject.GetComponent<IndividualModelLogic>();
        bool playSound = false;
        while (true)
        {
            if (PlayState.Playing == _IndividualModelLogic._Model.OnPlayState)
            {
                if (false == playSound)
                {
                    PlayMechanicalSound(IntroductionSpeechList[_IndividualModelLogic._Model.MechanicalSoundID]);
                    playSound = true;
                }
            }
            else if (PlayState.FallBack == _IndividualModelLogic._Model.OnPlayState)
            {
                if (false == playSound)
                {
                    PlayMechanicalSound(IntroductionSpeechList[_IndividualModelLogic._Model.MechanicalSoundID]);
                    playSound = true;
                }
            }
            else
            {
                playSound = false;
            }
            yield return new WaitForSeconds(0);
        }
    }

    /// <summary>
    /// 拆装或者组装声音
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayMechanicalSound(AudioClip audioClipMechanicalSound)
    {
        GlobalVar._AudioSource.clip = audioClipMechanicalSound;
        GlobalVar._AudioSource.loop = false;
        GlobalVar._AudioSource.Play();
    }
}
