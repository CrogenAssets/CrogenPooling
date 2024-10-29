using System;
using System.Collections;
using UnityEngine;
using Crogen.CrogenPooling;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundPlayer : MonoBehaviour, IPoolingObject
{
	[HideInInspector] public AudioSource audioSource;
	
	public string OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public void SetAudioResource(AudioResource audioResource, bool loop = false)
	{
		audioSource.resource = audioResource;
		audioSource.Play();
		if(loop == false)
			StartCoroutine(CoroutineOnPlay());
	}
	
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void OnPop()
	{
	}

	public void OnPush()
	{
		StopAllCoroutines();
	}

	private IEnumerator CoroutineOnPlay()
	{
		yield return new WaitWhile(()=>audioSource.isPlaying);
		this.Push();
	}
}
