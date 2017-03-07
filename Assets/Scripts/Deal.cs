using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Deal : NetworkBehaviour {


	//Cards are just integers from 0-51
	List<int>	mDealerDeck = new List<int> ();

	public	List<int>	DealerDeck {
		get {
			return	mDealerDeck;
		}
	}

	//When server starts make a deck of 52 cards
	public override void OnStartServer () {
		base.OnStartServer ();
		for (int i = 0; i < Card.Count; i++) {
			mDealerDeck.Add (i);	//Make Deck of 52 cards
		}
		GM.ServerDealer = this;
	}

	public	int	DrawCard() {		//Gets a random card
		if (mDealerDeck.Count > 0) {
			int	tIndex = Random.Range (0, mDealerDeck.Count);
			int	tCard=mDealerDeck[tIndex];
			mDealerDeck.RemoveAt (tIndex);		//Remove drawn card from deck
			return	tCard;
		}
		return	-1;		//No cards left
	}

	public	void	ReturnCard(int vID) {		//Return card to dealer
		mDealerDeck.Add (vID);
	}
}
