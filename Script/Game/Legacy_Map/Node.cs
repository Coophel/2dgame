using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
	public class Node
	{
		public readonly Point point;
		public readonly List<Point> income = new List<Point>();
		public readonly List<Point> outcome = new List<Point>();

		// [JsonConverter(typeof(StringEnumConverter))]
		public readonly NodeType nodeType;
		public readonly string blueprintName;
		public Vector2 position;

#region Public Funtions
		public Node(NodeType nodeType, string blueprintName, Point point)
		{
			this.nodeType = nodeType;
			this.blueprintName = blueprintName;
			this.point = point;
		}

		public void AddIncome(Point p)
		{
			if (income.Any(elemnet => elemnet.Equals(p)))
				return ;
			
			income.Add(p);
		}

		public void AddOutcome(Point p)
		{
			if (outcome.Any(elemnet => elemnet.Equals(p)))
				return ;
			
			outcome.Add(p);
		}

		public void RemoveIncome(Point p)
		{
			income.RemoveAll(element => element.Equals(p));
		}

		public void RemoveOutcome(Point p)
		{
			outcome.RemoveAll(element => element.Equals(p));
		}

		public bool NoConnections()
		{
			if (income.Count == 0 && outcome.Count == 0)
				return true;
			else
				return false;
		}
#endregion
	}

}
