using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class MyBSTree
    {
        public class Node
        {
            public int value { set; get; }
            public Node leftChild { set; get; }
            public Node rightChild { set; get; }

            public Node(int value)
            {
                this.value = value;
            }

            //to debug easy and see the values:
             public override string ToString()
            {
                return "Node = " + value;
            }
        }
        public Node Root {get; set; }

        #region Insert
        public void Insert(int value)
        {
            var node = new Node(value);

            //two cases: if the tree is empty or is not
            
            if (Root == null)  //empty tree
            {
                Root = node;
                return;
            }

            //tree is NOT empty:
            var current = Root;
            while (true)
            {
                if (value < current.value)    //we should go to the left subtree
                {
                    if (current.leftChild == null)  //leaf, and we are inserting our new node here
                    {
                        current.leftChild = node;
                        break;
                    }
                    current = current.leftChild; //we go down
                }
                else  //we are going to the right subtree
                {
                    if (current.rightChild == null) //leaf, and we are inserting our new node here
                    {
                        current.rightChild = node;
                        break;
                    }
                    current = current.rightChild; //going down
                }
            }
        }
        #endregion

        #region Find
        public bool Find(int value)
        {
            var current = Root;

            while(current != null)
            {
                if (value < current.value) //we must go down ti the left subtree
                    current = current.leftChild;
                else if (value > current.value)  //we must go down ti the right subtree
                    current = current.rightChild;
                else
                    return true;     // when value == current.value          
            }

            return false; //node wasn't found
        }
        #endregion

        #region TraversePreOrder
        //Root-Left-Right
        public void TraversePreOrder()
        {
            TraversePreOrder(Root);
        }
        
        private void TraversePreOrder(Node root)
        {
            if (root == null)
                return;

            Console.WriteLine(root.value);
            TraversePreOrder(root.leftChild);
            TraversePreOrder(root.rightChild);
        }
        #endregion

        #region TraverseInOrder
        //Left-Root-Right
        public void TraverseInOrder()
        {
            TraverseInOrder(Root);
        }

        private void TraverseInOrder(Node root)
        {
            if (root == null)
                return;

            TraverseInOrder(root.leftChild);
            Console.WriteLine(root.value);
            TraverseInOrder(root.rightChild);
        }
        #endregion

        #region TraversePostOrder
        //Left-Right-Root
        public void TraversePostOrder()
        {
            TraversePostOrder(Root);
        }

        private void TraversePostOrder(Node root)
        {
            if (root == null)
                return;

            TraversePostOrder(root.leftChild);
            TraversePostOrder(root.rightChild);
            Console.WriteLine(root.value);
        }
        #endregion

        #region EqualityChecking
        public bool Equals(MyBSTree other)
        {
            if (other == null)
                return false;

            return Equals(Root, other.Root);
        }
        private bool Equals(Node first, Node second)
        {
            if (first == null && second == null)
                return true;

            if (first != null && second != null)
                return first.value == second.value
                    && Equals(first.leftChild, second.leftChild)
                    && Equals(first.rightChild, second.rightChild);

            return false;
        }
        #endregion

        #region Height 
        //this is the the bigest number of edges,
        //going from the leaf to the root of the tree
        //we can recursively calculate the height of
        //the left and the right subtrees, found the biggest
        //and add 1 for the current node

        public int Height()
        {
            return Height(Root);
        }

        private int Height(Node root)
        {
            if (root == null)  //if the tree is empty
                return -1;

            if (root.leftChild == null && root.rightChild == null) //base condition -> bottom of the recursion -> leaf
                return 0;

            return 1 + Math.Max(   //the biggest between the height of the left subtree or the right subtree + 1 (for the current node)
                Height(root.leftChild), 
                Height(root.rightChild));
        }
        #endregion

        #region DepthToNode -> Get Nodes At Distance
        public List<int> GetNodesAtDistance(int distance)
        {
            var list = new List<int>();
            GetNodesAtDistance(Root, distance, list);
            return list;
        }

        private void GetNodesAtDistance(Node root, int distance, List<int>list)
        {
            if (root == null) //base case -> leaf
                return;

            if(distance == 0) //functionality -> calculated distance
            {
                list.Add(root.value);
                return;
            }

            GetNodesAtDistance(root.leftChild, distance - 1,list);
            GetNodesAtDistance(root.rightChild, distance - 1, list);
        }

        #endregion        

        #region LevelOrderTraversal       

        public void TraverseLevelOrder()
        {
            for (int i = 0; i < Height(); i++)
            {
                var list = GetNodesAtDistance(i);
                foreach (var item in GetNodesAtDistance(i))
                {
                    Console.WriteLine(item);
                }
            }
        }
        #endregion

        #region SizeOfTheTree

        public int Size()
        {
            return Size(Root);
        }

        private int Size(Node root)
        {
            if (root == null)
                return 0;

            if (IsLeaf(root))   // base condition - bottom of the recursion
                return 1;

            return 1 + Size(root.leftChild) + Size(root.rightChild);
        }

        private bool IsLeaf(Node node)
        {
            return node.leftChild == null && node.rightChild == null;
        }

        #endregion

        #region CountOfTheLeaves
        public int CountLeaves()
        {
            return CountLeaves(Root);
        }

        private int CountLeaves(Node root)
        {
            if (root == null)
                return 0;

            if (IsLeaf(root))
                return 1;

            return CountLeaves(root.leftChild) + CountLeaves(root.rightChild);
        }

        #endregion

        #region IsBalanced

        public bool IsBalanced()
        {
            return IsBalanced(Root);
        }

        private bool IsBalanced(Node root)
        {
            if (root == null)
                return true;

            var coef = Height(root.leftChild) - Height(root.rightChild);

            return Math.Abs(coef) <= 1 &&
                    IsBalanced(root.leftChild) &&
                    IsBalanced(root.rightChild);
        }

        #endregion

        #region ValidatingBST
        // we should check it a bynatry tree is a bynary Search tree
        // 1st  way - using recursion - for every node we visit it's subtrees ->
        // if all the values are in the correct intervals, we are going to the next subtree
        // 1st way is slow, because we are visiting every node multiply times

        //2nd way - to traverse the tree and for every node we checks if the value of this node is in the correct interval

        public bool IsBinarySearchTree()
        {
            return IsBinarySearchTree(Root, Int32.MinValue, Int32.MaxValue);
        }

        private bool IsBinarySearchTree(Node root, int min, int max)
        {
            if (root == null)
                return true;

            if (root.value < min || root.value > max)
                return false;

            return
                IsBinarySearchTree(Root.leftChild, min, Root.value - 1)
                && IsBinarySearchTree(Root.rightChild, Root.value + 1, max);
        }
        
        #endregion
    }
}
