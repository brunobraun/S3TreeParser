using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace S3TreeParser
{
    /// <summary>
    /// Node class using the format of ExtJS Tree.
    /// </summary>
    public class Node
    {
        public string id { get; set; } // id/key of folder of file
        public string text { get; set; } // name of folder or file
        public bool expanded { get; set; }
        public List<Node> children { get; set; }
        public bool leaf { get; set; }
    }
}
