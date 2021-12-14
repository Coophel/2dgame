using UnityEngine;

namespace Map
{
	[System.Serializable]
	public class MapLayer
	{
		public NodeType nodeType;

		// 전 노드와의 거리 설정
		public FloatMinMax distanceFromPreviousLayer;
		
		[Tooltip("Distance between nodes")]
		public float nodesApartDistance;


		[Tooltip("0 -> straight / 1 -> more random position")]
		[Range(0f, 1f)]
		public float randomizePostion;

		[Tooltip("Chance to get more random nodes then default")]
		[Range(0f, 1f)]
		public float randomizeNodes;
	}
}
