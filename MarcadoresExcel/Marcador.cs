using System;
using System.Collections.Generic;
using System.Text;

namespace MarcadoresExcel
{
    class Marcador
    {
        public string Name { get; set; }
        public string Start { get; set; }
        public string Duration { get; set; }
        public string TimeFormat { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    class Marcador2 : Marcador
    {
        public string Minute { get; set; }
        public string Second { get; set; }
        public string Millisecond { get; set; }
    }
}
