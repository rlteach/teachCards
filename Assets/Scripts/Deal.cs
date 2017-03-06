using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Deal : NetworkBehaviour {

	List<int>	mDealerDeck = new List<int> ();

	public override void OnStartServer () {
		base.OnStartServer ();
		for (int i = 0; i < Card.Count; i++) {
			mDealerDeck.Add (i);	//Make Deck of 52 cards
		}
		GM.ServerDealer = this;
	}

	public	int	GetCard() {		//Gets a random card
		if (mDealerDeck.Count > 0) {
			int	tIndex = Random.Range (0, mDealerDeck.Count);
			int	tCard=mDealerDeck[tIndex];
			mDealerDeck.RemoveAt (tIndex);
			return	tCard;
		}
		return	-1;		//No cards left
	}
}
