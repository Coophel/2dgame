using UnityEngine;

namespace Map
{
	public enum NodeType
		{
			Enemy,
			EliteEnemy,
			RestPlace,
			Store,
			Mystery,
			Boss
		}

	[CreateAssetMenu]
	public class Nodeblueprint : ScriptableObject
	{
		public Sprite sprite;
		public NodeType nodeType;
	}
}
