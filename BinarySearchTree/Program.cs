using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            MyBSTree bsTree = new MyBSTree();
            bsTree.Insert(7);
            bsTree.Insert(4);
            bsTree.Insert(9);
            bsTree.Insert(1);
            bsTree.Insert(6);
            bsTree.Insert(8);
            bsTree.Insert(10);

            Console.WriteLine(bsTree.Find(1));
            Console.WriteLine(bsTree.Find(111));

            Console.WriteLine("Pre-Order Traversal:");
            bsTree.TraversePreOrder();

            Console.WriteLine("In-Order Traversal:");
            bsTree.TraverseInOrder();

            Console.WriteLine("Post-Order Traversal:");
            bsTree.TraversePostOrder();

            MyBSTree bsTree2 = new MyBSTree();
            bsTree2.Insert(7);
            bsTree2.Insert(4);
            bsTree2.Insert(9);
            bsTree2.Insert(1);
            bsTree2.Insert(6);
            bsTree2.Insert(8);
            bsTree2.Insert(10);
            bsTree2.Insert(12);

            Console.WriteLine(bsTree.Equals(bsTree2));

            Console.WriteLine(bsTree.Height());

            bsTree2.TraverseLevelOrder();
            
            //Console.WriteLine(bsTree.IsBinarySearchTree());

            Console.ReadKey();
        }
    }
}
