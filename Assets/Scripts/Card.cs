using System.Collections;


//C# platorm independant card class

public class Card  {

	public	enum Suites	{
		Spades=0
		,Diamonds
		,Hearts
		,Clubs
		,Count
	}

	public	enum Ranks {
		Ace
		,Two
		,Three
		,Four
		,Five
		,Six
		,Seven
		,Eight
		,Nine
		,Ten
		,Jack
		,Queen
		,King
		,Count
	}

	static	public	int	RankCount {
		get {
			return	(int)Ranks.Count;
		}
	}

	static	public	int	SuitCount {
		get {
			return	(int)Suites.Count;
		}
	}

	static	public	int Count {
		get {
			return	RankCount * SuitCount;
		}
	}

	public	Ranks	Rank {
		get {
			return	(Ranks)(mID % RankCount);
		}
	}

	public	Suites	Suit {
		get {
			return(Suites)((mID / RankCount) % SuitCount);
		}
	}

	static	public	string	 ToString(int vCard) {
		return	string.Format ("{0} of {1}", (Ranks)(vCard % RankCount), (Suites)((vCard / RankCount) % SuitCount));
	}

	//This card
	int	mID;

	public	int	ID {
		get {
			return	mID;
		}
		set {
			mID = value%(RankCount*SuitCount);		//Clamp
		}
	}

	public override string ToString () {
		return string.Format ("[Card:ID={2} {0} of {1}]", Rank, Suit, ID);
	}

	public	Card(int vID) {
		ID = vID;
	}

	public	Card(Ranks vRank, Suites vSuit) {
		ID = ((int)vRank % RankCount) + (((int)vSuit % SuitCount) * RankCount);
	}
}
