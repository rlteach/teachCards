using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour {


	//Static game manager, exposes core gameobjects in static form, for ease of use

	static	GM	sGM;


	Player	mLocalPlayer;
	public	static	Player	LocalPlayer {
		get {
			return	sGM.mLocalPlayer;
		}
		set	{
			sGM.mLocalPlayer = value;
		}
	}

	Deal	mDealer;
	public static	Deal	ServerDealer {
		get {
			return	sGM.mDealer;
		}
		set {
			sGM.mDealer = value;
		}
	}

	void Awake () {
		if (sGM == null) {
			sGM = this;
			DontDestroyOnLoad (gameObject);
		} else if (sGM != this) {
			Destroy (gameObject);
		}
	}
}
