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
					CmdDelete (tCS.mCard.ID);
					Destroy (tHit.collider.gameObject);
				}
			}
		}
	}

	[Command]
	public	void	CmdDeal() {
		if (GM.ServerDealer != null) {
			int tCard=GM.ServerDealer.DrawCard ();
			if (tCard >= 0) {
				mPlayerDeck.Add (tCard);
			}
		}
	}

	[Command]
	public	void	CmdDelete(int vID) {
		mPlayerDeck.Remove (vID);
		GM.ServerDealer.ReturnCard (vID);
	}

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
