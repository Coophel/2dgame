using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
	public class Map
	{
		public List<Node> nodes;
		public List<Point> path;
		public string configName;

#region Public Functions
		public Map(string configName, List<Node> nodes, List<Point> path)
		{
			this.configName = configName;
			this.nodes = nodes;
			this.path = path;
		}

		public Node GetBossNode()
		{
			return nodes.FirstOrDefault(n => n.nodeType == NodeType.Boss);
		}

		public float DistanceBetweenFirstAndLastLayers()
		{
			var bossNode = GetBossNode();
			var firstLayerNode = nodes.FirstOrDefault(n => n.point.y == 0);

			if (bossNode == null || firstLayerNode == null)
				return 0f;

			return bossNode.position.y - firstLayerNode.position.y;
		}

		public Node GetNode(Point p)
		{
			return nodes.FirstOrDefault(n => n.point.Equals(p));
		}

		public string ToJson()
		{
			// make SerializeObject of this
			var json = JsonUtility.ToJson(this, true);
			return json;
		}
#endregion
	}
}
