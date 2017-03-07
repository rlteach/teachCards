using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Player : NetworkBehaviour {

	SyncListInt	mPlayerDeck=new SyncListInt ();		//Needs to be done here or there are init errors

	public override void OnStartClient() {
		name = "Remote";
	}

	public override void OnStartLocalPlayer () {
		base.OnStartLocalPlayer ();
		name = "Local";
		mPlayerDeck.Callback = OnInventoryChanged;
		GM.LocalPlayer = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(isLocalPlayer) {
			ClickFlip ();
		}
	}

	void	ClickFlip() {
		if (Input.GetMouseButtonDown (0)) {
			Ray tRay = Camera.main.ScreenPointToRay (Input.mousePosition);				//Make a ray from screen position into the game scene
			RaycastHit2D	tHit = Physics2D.Raycast (tRay.origin, tRay.direction);		//Cast ray, if it hits a game object we will know, NB only first collision is reported
			if (tHit.collider != null) {
				CardSprites.CardSprite	tCS = tHit.collider.gameObject.GetComponent<CardSprites.CardSprite> ();
				if (tCS != null) {
					tCS.Show = !tCS.Show;
					CmdReturnCard (tCS.mCard.ID);
					Destroy (tHit.collider.gameObject);		//Remove local image of card, note this does not use Networked GO, all local
				}
			}
		}
	}

	[Command]		//Run this on server only, as only server has access to dealer deck which only lives on server
	public	void	CmdDeal() {
		if (GM.ServerDealer != null) {
			int tCard=GM.ServerDealer.DrawCard ();
			if (tCard >= 0) {
				mPlayerDeck.Add (tCard);	//Add Drawn card to player
			}
		}
	}

	[Command]
	public	void	CmdReturnCard(int vID) {		//Player gives card back to dealer
		mPlayerDeck.Remove (vID);		//Player removes from deck
		GM.ServerDealer.ReturnCard (vID);	//Deal adds to their deck
	}


	//Called when Player hand changes
	private void OnInventoryChanged(SyncListInt.Operation op, int index) {
		switch (op) {
		case	SyncList<int>.Operation.OP_ADD: {
				CardSprites.CardSprite tSprite=CardSprites.InstantiateCard (mPlayerDeck [index]);
				tSprite.transform.position = new Vector2 ((float)index/6f-5f, 0);
		}
		break;
		}
		Debug.Log("Items changed " + op);
	}
}
