using System;
using System.Collections.Generic;

namespace BeersLibrary
{
    public class BeerManagerSerializableAdapter
    {
        public List<Beer> Beers  { get; set; }
        public int CurrentPosition { get; set; }
    }
}
