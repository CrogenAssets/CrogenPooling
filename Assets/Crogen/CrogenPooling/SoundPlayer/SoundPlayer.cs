using UnityEngine;
using Crogen.CrogenPooling;

public class SoundPlayer : MonoBehaviour, IPoolingObject
{
	public string OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public void OnPop()
	{
	}

	public void OnPush()
	{
	}
}
