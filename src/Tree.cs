using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace S3TreeParser
{   
    public class Tree
    {
        // root node
        public Node root { get; set; }

        /// <summary>
        /// Creates a tree.
        /// </summary>
        /// <param name="s3FileDirectories">List of strings that contains the keys, example: 'folder1/folder2/file.txt'</param>
        /// <returns></returns>
        public Tree CreateTree(List<string> s3FileDirectories)
        {
            Tree tree = new Tree();

            // instantiate the root node
            tree.root = new Node() { id = "root", text = "root", children = new List<Node>(), expanded = true, leaf = false };

            foreach (var s3FileDirectory in s3FileDirectories)
            {
                CreateTreePath(@s3FileDirectory, tree.root);
            }

            return tree;
        }

        /// <summary>
        /// Searches by name (text) for a node in a subtree, if it don't find it a new node is created.
        /// </summary>
        /// <param name="text">Name of node</param>
        /// <param name="subtree">Subtree to find the node</param>
        /// <returns></returns>
        public Node FindOrCreateNode(string text, Node subtree)
        {
            Node nodeFound = subtree.children.Where(x => x.text == text).FirstOrDefault();
            if (nodeFound == null)
            {
                Node node = new Node();
                node.text = text;
                node.id = text;
                node.expanded = false;
                node.leaf = false;
                node.children = new List<Node>();
                subtree.children.Add(node);
                return node;
            }
            return nodeFound;
        }

        /// <summary>
        /// Creates a path of a tree (subtree) for given directory.
        /// </summary>
        /// <param name="s3Directory">Example: 'folder1/folder2/file.txt'</param>
        /// <param name="node">Node where the subtree will start</param>
        public void CreateTreePath(string s3Directory, Node node)
        {
            string[] split = s3Directory.Split('/');

            int loopLength = split.Length - 1;

            for (var i = 0; i <= loopLength; i++)
            {
                if (i == loopLength)
                {
                    Node nodeFile = new Node();
                    nodeFile.leaf = true;
                    nodeFile.id = s3Directory;
                    nodeFile.text = split[i];
                    node.children.Add(nodeFile);                    
                }
                else
                {
                    node = FindOrCreateNode(split[i], node);
                }
            }
        }        
    }
}
