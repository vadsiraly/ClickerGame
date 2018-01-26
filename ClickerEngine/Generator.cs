using System;
using System.Collections.Generic;
using System.Text;

namespace ClickerEngine
{
    public class Generator
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }

        public Value ValuePerSecond { get; set; }
        public Value ValuePerClick { get; set; }
    }
}
