using System.Collections.Generic;
using Malee.List;
using UnityEngine;

namespace Map
{
	// Map 구성틀 만들어 주기.
	[CreateAssetMenu]
	public class MapConfig : ScriptableObject
	{
		public List<Nodeblueprint> nodeblueprints;
		public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);

		[Header("Befor Home Nodes Count")]
		public IntMinMax numOfPreBossNodes;
		[Header("Starting Node Count")]
		public IntMinMax numOfStartingNodes;
		
		
		
		// Malee.List 에서 받아서 사용.
		// inspector 에서 좀더 편하고 깔끔하게 조정 가능하게 해주는 함수

		[Reorderable]
		public ListOfMapLayers layers;

		[System.Serializable]
		public class ListOfMapLayers : ReorderableArray<MapLayer>
		{}
	}
}