using UnityEngine;

public class SoundPlayer : AudioPlayer
{
	public string playerPrefKey = "isSoundActive";

	public static SoundPlayer Instance { get; private set; }

	public void Awake ()
	{
		//override
		audioPlayerStatusPlayerPrefKey = playerPrefKey;
		base.Awake ();
	}

	public override void AudioPlayerStatusChangedToActive()
	{
		//override
	}

	public override void AudioPlayerStatusChangedToDeActive ()
	{
		//override
	}
}
