using System.Collections;
using System.Collections.Generic;
using Crogen.CrogenPooling;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Singleton
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    Debug.LogError("No SoundManager found");
                }
            }
            return _instance;
        } 
    }
    
    public List<SoundDataSO> soundDataSOList;
    public List<SoundData> soundDataList;
    private Dictionary<string, SoundData> _soundDataDictionary;

    private SoundPlayer _curBGMPlayer;
    private SoundPlayer _oldBGMPlayer;
    
    private void Awake()
    {
        _soundDataDictionary = new Dictionary<string, SoundData>();
        foreach (var soundDataSO in soundDataSOList)
            foreach (var soundData in soundDataSO.soundDataList)
                _soundDataDictionary.Add(soundData.name, soundData);
    }

    public void PlayBGM(string soundName)
    {
        if (_soundDataDictionary.TryGetValue(soundName, out SoundData sd))
        {
            if (sd.type != SoundType.BGM) return;
            
            //사운드 재생
            _oldBGMPlayer = _curBGMPlayer;
            _curBGMPlayer = gameObject.Pop(SoundPoolType.SoundPlayer, Vector3.zero, Quaternion.identity) as SoundPlayer;
            _curBGMPlayer.audioSource.loop = true;
            _curBGMPlayer.SetAudioResource(sd.clip);
            
            StartCoroutine(CoroutineFadeBGM(_oldBGMPlayer, _curBGMPlayer));
        }
    }

    private IEnumerator CoroutineFadeBGM(SoundPlayer oldSoundPlayer, SoundPlayer curSoundPlayer)
    {
        if(oldSoundPlayer != null)
            oldSoundPlayer.audioSource.volume = 1;
        curSoundPlayer.audioSource.volume = 0;
        
        float percent = 0;
        float curTime = 0;
        float duration = 1f;
        
        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / duration;

            if(oldSoundPlayer != null)
                oldSoundPlayer.audioSource.volume = Mathf.Lerp(1, 0, percent);
            curSoundPlayer.audioSource.volume = Mathf.Lerp(0, 1, percent);
            yield return null;
        }
        
        oldSoundPlayer?.Push();
    }
    
    public void PlaySFX(string soundName, Vector3 position = default)
    {
        if (_soundDataDictionary.TryGetValue(soundName, out SoundData sd))
        {
            if (sd.type != SoundType.SFX) return;
            
            //사운드 재생
            SoundPlayer soundPlayer = gameObject.Pop(SoundPoolType.SoundPlayer, position, Quaternion.identity) as SoundPlayer;
            _curBGMPlayer.audioSource.loop = false;
            soundPlayer.SetAudioResource(sd.clip);
        }
    }
}
