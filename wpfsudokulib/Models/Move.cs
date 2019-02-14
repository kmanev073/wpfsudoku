using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfsudokulib.Models
{
    public class Move
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public byte? Value { get; set; }
    }
}
