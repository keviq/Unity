using UnityEngine;

public class MusicPlayer : AudioPlayer 
{
	public string playerPrefKey = "isMusicActive";

	public static MusicPlayer Instance { get; private set; }

	public void Awake ()
	{
		//override
		audioPlayerStatusPlayerPrefKey = playerPrefKey;
		base.Awake ();
	}

	public override void AudioPlayerStatusChangedToActive()
	{
		//e.g.
		//Play the game music when audio player becomes active.
	}

	public override void AudioPlayerStatusChangedToDeActive ()
	{
		//e.g.
		//Stop the game music when audio player becomes deactive.
	}
}
