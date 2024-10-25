using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundDataSO", menuName = "SO/SoundDataSO")]
public class SoundDataSO : ScriptableObject
{
	[SerializeField] private AudioResource clip;
	public AudioResource GetClip() => clip;
}
