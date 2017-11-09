using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour 
{
	public AudioClip[] audioClips;
	public GameObject audioPlayerUIObject;
	private bool isAudioPlayerActive;
	private AudioSource audioSource;
	[HideInInspector]
	public string audioPlayerStatusPlayerPrefKey;
	private bool audioPlayerInitialized;

	public void Awake()
	{
		InitializeAudioPlayer ();
	}

	public void PlayAudioClip(int audioClipIndex,bool loopAudioClip,int audioClipVolume)
	{
		if(audioPlayerInitialized)
		{
			if(isAudioPlayerActive)
			{
				if(audioClips.Length <= audioClipIndex)//Checks is the audio clip index is exists in the array.
				{//Error
					Debug.Log("Error: There is no audio clip for the '"+audioClipIndex+"' index.");
				}
				else
				{
					if (loopAudioClip)
						audioSource.loop = true;
					else
						audioSource.loop = false;

					audioSource.volume = audioClipVolume;
					audioSource.clip = audioClips [audioClipIndex];
					audioSource.Play();
				}
			}
		}
  	}
	public void StopAudioClip()
	{
		if (audioPlayerInitialized) 
		{
			audioSource.Stop ();
			audioSource.clip = null;
		}
	}

	//Changes the status of the audio player.
	//Called by the audio player ui object.
	public void OnAudioPlayerStatusChange ()
	{
		if (audioPlayerInitialized) 
		{
			if (isAudioPlayerActive) 
			{
				//deactivate
				isAudioPlayerActive = false;
				PlayerPrefs.SetInt (audioPlayerStatusPlayerPrefKey, 0);
				AudioPlayerStatusChangedToDeActive ();
			} 
			else
			{
				//activate
				isAudioPlayerActive = true;
				PlayerPrefs.SetInt (audioPlayerStatusPlayerPrefKey, 1);
				AudioPlayerStatusChangedToActive ();
			}
		}
  	}

	//Sets the latest audio player status from the player pref.
	private void InitializeAudioPlayer()
	{
		if(IsAudioPlayerPropertiesSet())
		{

			if(PlayerPrefs.GetInt(audioPlayerStatusPlayerPrefKey,1) == 1)
			{
				isAudioPlayerActive = true;
			}
			else
			{
				isAudioPlayerActive = false;
				audioPlayerUIObject.GetComponent<Toggle> ().isOn = false;
			}

			audioPlayerInitialized = true;
		}
      }

	//Checks the audio player properties.
	private bool IsAudioPlayerPropertiesSet()
	{
		if(GetComponent<AudioSource> ())
		{
			audioSource = GetComponent<AudioSource> ();
		}
		else
		{
			audioPlayerUIObject.GetComponent<Toggle> ().interactable = false;
			audioPlayerInitialized = false;
			Debug.Log("Error: There is no Audio Source atteched to game object. Audio Player disabled.");
			return false;
		}

		if(audioPlayerUIObject)
		{
			//Sets the event which will be triggered when the audio player status changed.
			audioPlayerUIObject.GetComponent<Toggle> ().onValueChanged.AddListener((bool value) => OnAudioPlayerStatusChange());
		}
		else
		{
			audioPlayerUIObject.GetComponent<Toggle> ().interactable = false;
			audioPlayerInitialized = false;
			Debug.Log("Error: Audio player ui object is not assigned. Audio Player disabled.");
			return false;
		}

		return true;
	}

	//This function is called when audio player status changed to active.
	//Override it for trigger events.
	public virtual void AudioPlayerStatusChangedToActive()
	{}

	//This function is called when player status changed to deactive;
	//Override it for trigger events.
	public virtual void AudioPlayerStatusChangedToDeActive()
	{}
}
