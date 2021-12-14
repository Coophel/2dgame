using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
	public static class MapGenerator
	{
		private static MapConfig config;
		private static readonly List<NodeType> RandomNodes = new List<NodeType> {NodeType.Enemy,
			NodeType.EliteEnemy, NodeType.Mystery, NodeType.RestPlace, NodeType.RestPlace, NodeType.Boss};

		private static List<List<Point>> paths;

		// distance list  .... why it needs???
		// 선택지에 대한 거리감을 구하기 위해서 필요
		private static List<float> layerDistances;
		private static readonly List<List<Node>> nodes = new List<List<Node>>();

#region Public Functions
		public static Map GetMap(MapConfig conf)
		{
			if (conf == null)
			{
				Debug.LogWarning("Config is null \n By : MapGenerator.");
				return null;
			}

			config = conf;
			nodes.Clear();

			GenerateLayerDistances();

			for (var i = 0; i < conf.layers.Count; i++)
				PlaceLayer(i);

			GeneratePaths();

			RandomizeNodePositions();

			SetUpConnections();

			RemoveCrossConnections();

			// select all the nodes with connections
			var nodeList = nodes.SelectMany(n => n).Where(n => n.income.Count > 0 || n.outcome.Count > 0).ToList();

			// pick a random name of the boss for this map
			// var bossNodeName = config.nodeblueprints.Where(b => b.nodeType == NodeType.Boss).ToList().Random().name;
			
			return new Map(conf.name, nodeList, new List<Point>());
		}
#endregion

#region Private Functions
		// By Layer distances map
		private static void GenerateLayerDistances()
		{
			// 거리 저장 리스트 생성
			layerDistances = new List<float>();

			// 
			foreach(var layer in config.layers)
				layerDistances.Add(layer.distanceFromPreviousLayer.GetValue());
		}

		//
		private static float GetDistanceToLayer(int layerIndex)
        {
			// if (OutOfRange)
            if (layerIndex < 0 || layerIndex > layerDistances.Count)
				return 0f;

            return layerDistances.Take(layerIndex + 1).Sum();
        }

		//
		private static void PlaceLayer(int layerIndex)
        {
            var layer = config.layers[layerIndex];
            var nodesOnThisLayer = new List<Node>();

            // offset of this layer to make all the nodes centered:
            var offset = layer.nodesApartDistance * config.GridWidth / 2f;

            for (var i = 0; i < config.GridWidth; i++)
            {
				var nodeType = Random.Range(0f, 1f) < layer.randomizeNodes ? GetRandomNode() : layer.nodeType;

				// MyUitll Ramdom<T> 로 무작위 가저오기.
				var blueprintName = config.nodeblueprints.Where(b => b.nodeType == nodeType).ToList().Ramdom().name;

				var node = new Node(nodeType, blueprintName, new Point(i, layerIndex))
                {
                    position = new Vector2(-offset + i * layer.nodesApartDistance, GetDistanceToLayer(layerIndex))
                };
                
				nodesOnThisLayer.Add(node);
            }

            nodes.Add(nodesOnThisLayer);
        }

		//
		private static void GeneratePaths()
		{
			var finalNode = GetFinalNode();
			paths = new List<List<Point>>();

			var numOfStartingNodes = config.numOfStartingNodes.GetValue();
			var numOfPreBossNodes = config.numOfPreBossNodes.GetValue();

			var candidateXs = new List<int>();
			for (var i = 0; i < config.GridWidth; i++)
				candidateXs.Add(i);

			candidateXs.Shuffle();
			var preBossXs = candidateXs.Take(numOfPreBossNodes);
			var preBossPoints = (from x in preBossXs select new Point(x, finalNode.y - 1)).ToList();
			var attempts = 0;

			foreach (var point in preBossPoints)
			{
				var path = Path(point, 0, config.GridWidth);
				path.Insert(0, finalNode);
				paths.Add(path);
				attempts++;
			}

			while (!PathsLeadToAtLeastNDifferentPoints(paths, numOfStartingNodes) && attempts < 100)
            {
                var randomPreBossPoint = preBossPoints[UnityEngine.Random.Range(0, preBossPoints.Count)];
                var path = Path(randomPreBossPoint, 0, config.GridWidth);
                path.Insert(0, finalNode);
                paths.Add(path);
                attempts++;
            }

            Debug.Log("Attempts to generate paths: " + attempts);
		}

		//
		private static void RandomizeNodePositions()
		{
			for (var index = 0; index < nodes.Count; index++)
			{
				var list = nodes[index];
				var layer = config.layers[index];
				float distToNextLayer;
				if (index + 1 >= layerDistances.Count)
					distToNextLayer = 0f;
				else
					distToNextLayer = layerDistances[index + 1];
				var distToPreviousLayer = layerDistances[index];

				foreach (var node in list)
				{
					var xRnd = Random.Range(-1f, 1f);
					var yRnd = Random.Range(-1f, 1f);

					var x = xRnd * layer.nodesApartDistance / 2f;
					var y = yRnd < 0 ? distToPreviousLayer * yRnd / 2f : distToNextLayer * yRnd / 2f;

					node.position += new Vector2(x, y) * layer.randomizePostion;
				}
			}
		}

		// GetNode not in Map GetNode
		// Make connection by point List of the Map;
		private static void SetUpConnections()
        {
            foreach (var path in paths)
            {
                for (var i = 0; i < path.Count; i++)
                {
                    var node = GetNode(path[i]);

                    if (i > 0)
                    {
                        // previous because the path is flipped
                        var nextNode = GetNode(path[i - 1]);
                        nextNode.AddIncome(node.point);
                        node.AddOutcome(nextNode.point);
                    }

                    if (i < path.Count - 1)
                    {
                        var previousNode = GetNode(path[i + 1]);
                        previousNode.AddOutcome(node.point);
                        node.AddIncome(previousNode.point);
                    }
                }
            }
        }

		//
		private static void RemoveCrossConnections()
        {
            for (var i = 0; i < config.GridWidth - 1; i++)
                for (var j = 0; j < config.layers.Count - 1; j++)
                {
                    var node = GetNode(new Point(i, j));
                    if (node == null || node.NoConnections()) continue;
                    var right = GetNode(new Point(i + 1, j));
                    if (right == null || right.NoConnections()) continue;
                    var top = GetNode(new Point(i, j + 1));
                    if (top == null || top.NoConnections()) continue;
                    var topRight = GetNode(new Point(i + 1, j + 1));
                    if (topRight == null || topRight.NoConnections()) continue;

                    // Debug.Log("Inspecting node for connections: " + node.point);
                    if (!node.outcome.Any(element => element.Equals(topRight.point))) continue;
                    if (!right.outcome.Any(element => element.Equals(top.point))) continue;

                    // Debug.Log("Found a cross node: " + node.point);

                    // we managed to find a cross node:
                    // 1) add direct connections:
                    node.AddOutcome(top.point);
                    top.AddIncome(node.point);

                    right.AddOutcome(topRight.point);
                    topRight.AddIncome(right.point);

                    var rnd = Random.Range(0f, 1f);
                    if (rnd < 0.2f)
                    {
                        // remove both cross connections:
                        // a) 
                        node.RemoveOutcome(topRight.point);
                        topRight.RemoveIncome(node.point);
                        // b) 
                        right.RemoveOutcome(top.point);
                        top.RemoveIncome(right.point);
                    }
                    else if (rnd < 0.6f)
                    {
                        // a) 
                        node.RemoveOutcome(topRight.point);
                        topRight.RemoveIncome(node.point);
                    }
                    else
                    {
                        // b) 
                        right.RemoveOutcome(top.point);
                        top.RemoveIncome(right.point);
                    }
                }
        }

		//
		private static List<Point> Path(Point from, int toY, int width, bool firstStepUnconstrained = false)
        {
            if (from.y == toY)
            {
                Debug.LogError("Points are on same layers, return");
                return null;
            }

            // making one y step in this direction with each move
            var direction = from.y > toY ? -1 : 1;

            var path = new List<Point> { from };
            while (path[path.Count - 1].y != toY)
            {
                var lastPoint = path[path.Count - 1];
                var candidateXs = new List<int>();
                if (firstStepUnconstrained && lastPoint.Equals(from))
                {
                    for (var i = 0; i < width; i++)
                        candidateXs.Add(i);
                }
                else
                {
                    // forward
                    candidateXs.Add(lastPoint.x);
                    // left
                    if (lastPoint.x - 1 >= 0) candidateXs.Add(lastPoint.x - 1);
                    // right
                    if (lastPoint.x + 1 < width) candidateXs.Add(lastPoint.x + 1);
                }

                var nextPoint = new Point(candidateXs[Random.Range(0, candidateXs.Count)], lastPoint.y + direction);
                path.Add(nextPoint);
            }

            return path;
        }

		//
		private static bool PathsLeadToAtLeastNDifferentPoints(IEnumerable<List<Point>> paths, int n)
        {
            return (from path in paths select path[path.Count - 1].x).Distinct().Count() >= n;
        }

		private static Point GetFinalNode()
		{
			var y = config.layers.Count - 1;

			if (config.GridWidth % 2 == 1)
				return new Point(config.GridWidth / 2, y);
			
			if (Random.Range(0, 2) == 0)
				return new Point(config.GridWidth / 2, y);
			else
				return new Point(config.GridWidth / 2 - 1, y);
		}

		// point 위치에 있는 노드 받아오기
		private static Node GetNode(Point p)
		{
			Debug.Log ("in GetNode : " + p.y + " " + p.x);
			if (p.y >= nodes.Count || p.y < 0)
				return null;
			if (p.x >= nodes[p.y].Count || p.x < 0)
				return null;

			return nodes[p.y][p.x];
		}

		// 무작위 노드 골라주는 함수
		private static NodeType GetRandomNode()
		{
			return RandomNodes[Random.Range(0, RandomNodes.Count)];
		}
#endregion
	}
}
