using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class GraphInput
    {
        [Required]
        [Range(0, 8, ErrorMessage = "Invalid node number.")]
        public int StartNode { get; set; }
        [Required]
        [Range(0, 8, ErrorMessage = "Invalid node number.")]
        public int TargetNode { get; set; }
        public bool IsDFS { get; set; }

        public GraphInput(int startNode, int targetNode, bool isDFS) {
            StartNode = startNode;
            TargetNode = targetNode;
            IsDFS = isDFS;
        }
    }
}
