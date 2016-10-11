using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram_WinProg2016.Model
{
    class Class
    {
        string ClassName;
        List<string> Fields;
        List<string> Methods;

        double Height { get; set; }
        int Number { get; }
        double Width { get; }
        double X { get; }
        double Y { get; }
    }
}
