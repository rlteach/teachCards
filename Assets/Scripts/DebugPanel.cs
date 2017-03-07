using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

	//Show Debug info

	Text	mDebugText;

	// Use this for initialization
	void Start () {
		mDebugText = GetComponentInChildren<Text> ();
		StartCoroutine (ShowDebug (0.5f));
	}

	IEnumerator	ShowDebug(float vTime) {
		do {
			mDebugText.text=DealerCards();
			yield	return	new WaitForSeconds(vTime);
		} while (true);
	}

	string	DealerCards() {
		if (GM.ServerDealer != null) {
			StringBuilder	tSB = new StringBuilder ();
			foreach (int tCard in GM.ServerDealer.DealerDeck) {
				tSB.AppendLine (Card.ToString (tCard));
			}
			return		tSB.ToString ();
		}
		return	"Only availiable on server";
	}

}
