using UnityEngine;
using System.Collections;

public class CardSprites : MonoBehaviour {
	//For all cards
	static	Sprite[] 	sSpritesFront;
	static	Sprite 		sSpritesBack;

	static	CardSprites	sCR;

	public	class CardSprite : MonoBehaviour {
		public	Card			mCard;
		public	SpriteRenderer	mSR;
		public	bool	Show {
			get {
				return	mSR.sprite != sSpritesBack;
			}
			set {
				mSR.sprite = (value) ? sSpritesFront [mCard.ID] : sSpritesBack;
			}
		}
	}

	void	Awake() {		//make Singleton
		if (sCR == null) {
			sCR = this;
			DontDestroyOnLoad (gameObject);
			if (sSpritesFront == null) {
				sSpritesFront=Resources.LoadAll<Sprite>("CardDeckSpriteSheet");
			}
			if (sSpritesBack == null) {
				sSpritesBack=Resources.Load<Sprite>("HiResBack");
			}
		} else if (sCR != this) {
			Destroy (gameObject);
		}
	}

	CardSprite MakeCardPrefab(int vID) {
		vID %= Card.Count;		//clamp
		GameObject	tGO = new GameObject ();	//Make new game Object
		CardSprite	tCR=tGO.AddComponent<CardSprite> ();			//Add Card to it and store in array
		BoxCollider2D tBC=tGO.AddComponent<BoxCollider2D> ();
		tCR.mSR = tGO.gameObject.AddComponent<SpriteRenderer> ();
		tCR.mSR.sprite = sSpritesBack;		//Initally show back
		tBC.size=tCR.mSR.sprite.bounds.size;
		tCR.mCard=new Card(vID);		//Make new card up
		tCR.Show = false;				//Show back of card
		tGO.name=tCR.mCard.ToString();	//Name GameObject
		return	tCR;
	}

	static	public	CardSprite	InstantiateCard(int vID) {
		return	sCR.MakeCardPrefab(vID);	//Make a card prefab
	}
}
