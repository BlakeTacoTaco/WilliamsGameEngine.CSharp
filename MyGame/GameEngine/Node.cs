using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine
{
    internal class Node
    {
        private bool _isDead;

        // Using a set prevents duplicates.
        private readonly HashSet<string> _tags = new HashSet<string>();

        //these are all of the nodes that fall under it in a sort of hierarchy system that all stems from the root node
        internal readonly List<Node> _children = new List<Node>();

        //the node this node is a child of
        private Node _parent;

        //default constructor
        public Node() { }

        //adds a child node to this node
        public void AddChild(Node child)
        {
            _children.Add(child);
            child.SetParent(this);
        }

        //sets this nodes parent to the input
        public void SetParent(Node parent)
        {
            _parent = parent;
        }

        // "Dead" nodes will be removed from the scene.
        public bool IsDead()
        {
            return _isDead;
        }

        public void MakeDead()
        {
            _isDead = true;
        }

        //removes all its dead children
        //and then tells all its children to remove their children and so on
        //(kinda sounds bad when you put it that way.)
        public void RemoveDead()
        {
            Predicate<Node> die = child => child.IsDead();
            _children.RemoveAll(die);
            foreach(Node child in _children)
            {
                child.RemoveDead();
            }
        }

        // Update is called every frame.
        // Use this to update this node's children
        public virtual void Update(Time elapsed)
        {
            UpdateSelf(elapsed);
            foreach (Node child in _children)
            {
                child.Update(elapsed);
            }
        }
        //so this node can update itself
        public virtual void UpdateSelf(Time elapsed) { }

        // Draw is called once per frame.
        // Use this to draw this node's children
        // passes down the camera that it's drawn with to all it's children
        // in the case of a camera object it passes down itself instead
        public virtual void Draw(Camera camera) 
        {
            DrawSelf(camera);
            foreach (Node child in _children)
            {
                child.Draw(camera);
            }
        }
        //this is so the node can draw itself
        public virtual void DrawSelf(Camera camera) { }

        // Tags annotate your objects so you can identify them later
        public void AssignTag(string tag)
        {
            _tags.Add(tag);
        }

        public bool HasTag(string tag)
        {
            return _tags.Contains(tag);
        }
    }
}
