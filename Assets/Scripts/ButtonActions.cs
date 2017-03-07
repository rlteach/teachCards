using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {

	public	void	Deal() {
		if (GM.LocalPlayer != null) {
			GM.LocalPlayer.CmdDeal ();
		}
	}

	public	void	TestPress() {
		Debug.Log ("Pressed:"+name);
	}

}
