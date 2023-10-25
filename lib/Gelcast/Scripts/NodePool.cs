using Godot;
using System.Collections.Generic;

namespace Gelcast
{
	public class NodePool
	{
		private PackedScene template;
		private List<Node> pool = new List<Node>();

		/// <summary>
		/// Required to set up the object
		/// </summary>
		public void SetTemplate(PackedScene node)
		{
			if (template != null)
				GD.PrintErr("Attempting to set a template that already exists in " + template.ResourceName + " pool.");
			template = node;
		}

		/// <summary>
		/// Creates X amount of objects for the pool
		/// </summary>
		public void Generate(int amount = 1)
		{
			for (int i = 0; i < amount; i++)
			{
				Node node = template.Instantiate();
				node.Name = template.ResourceName;
				pool.Add(node);
			}
		}

		/// <summary>
		/// Pulls a new object out of the pool.
		/// </summary>
		public Node Draw()
		{
			foreach (Node node in pool)
			{
				if (node.ProcessMode == Node.ProcessModeEnum.Disabled)
				{
					node.ProcessMode = Node.ProcessModeEnum.Inherit;
					return node;
				}
			}

			//Pool's run dry, let's fetch a new one
			int poolSize = pool.Count;
			Generate();
			return pool[poolSize];
		}

		/// <summary>
		/// Pulls a list of objects out of the pool
		/// </summary>
		/// <param name="amount">How many Nodes are returned</param>
		/// <returns>List of Nodes</returns>
		public List<Node> Draw(int amount)
		{
			int remainder = amount;
			List<Node> ret = new List<Node>();
			foreach (Node node in pool)
			{
				if (node.ProcessMode == Node.ProcessModeEnum.Disabled)
				{
					node.ProcessMode = Node.ProcessModeEnum.Inherit;
					ret.Add(node);
					remainder--;
				}
			}

			//Pool's run dry, let's add more
			int poolSize = pool.Count;
			Generate(remainder);
			for (int i = poolSize; i < remainder; i++)
				ret.Add(pool[i]);
			return ret;
		}

		/// <summary>
		/// Returns an object to the pool for later use
		/// </summary>
		/// <param name="node"></param>
		public void Return(Node node)
		{
			node.ProcessMode = Node.ProcessModeEnum.Disabled;
		}

		/// <summary>
		/// Returns a list of objects to the pool for later use
		/// </summary>
		/// <param name="nodes"></param>
		public void Return(List<Node> nodes)
		{
			foreach (Node node in nodes)
				node.ProcessMode = Node.ProcessModeEnum.Disabled;
		}

		/// <summary>
		/// Removes all of the currently inactive objects from the pool and destroys them
		/// </summary>
		/// <param name="minimum">Optional minimum keeps X amount of objects in the pool</param>
		public void Trim(int minimum = 0)
		{
			int remaining = pool.Count;
			List<Node> removal = new List<Node>();
			foreach (Node node in pool)
			{
				if (node.ProcessMode == Node.ProcessModeEnum.Disabled)
				{
					removal.Add(node);
					remaining--;

					if (remaining <= minimum)
						break;
				}
			}

			foreach (Node node in removal)
			{
				pool.Remove(node);
				node.QueueFree();
			}
		}

		/// <summary>
		/// Adds an outside object to the pool
		/// </summary>
		/// <param name="node"></param>
		public void Add(Node node)
		{
			pool.Add(node);
		}

		/// <summary>
		/// Completely wipes the pool and destroys everything inside it
		/// </summary>
		public void Clear() //This is probably a bad idea
		{
			foreach (Node node in pool)
				node.QueueFree();
			pool.Clear();
		}
	}
}