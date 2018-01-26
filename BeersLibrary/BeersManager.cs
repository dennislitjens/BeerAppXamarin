using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace BeersLibrary
{
    public class BeersManager
    {
        public BeersManager()
        {
        }

        public List<Beer> Beers
        {
            get;
            set;
        }

        public int LastIndex { get; set; }

        public void InitLastIndex()
        {
            if (Beers != null){
                LastIndex = Beers.Count() - 1;
            }else {
                LastIndex = 0;
            }
            
        }

        //public void InitBeers()
        //{
        //        var initBeers = new Beer[] {
        //            new Beer()
        //                {
        //                Name = "Duvel",
        //                Description = "This is duvel.",
        //                Photo = "https://img.saveur-biere.com/img/p/107-15017-v4_product_img.jpg",
        //                AlcoholPercentage = 3.4
        //                },
        //            new Beer()
        //                {
        //                Name = "Leffe",
        //                Description = "This is leffe.",
        //                Photo = "https://img.saveur-biere.com/img/p/107-15017-v4_product_img.jpg",
        //                AlcoholPercentage = 2.9
        //                },
        //            new Beer()
        //                {
        //                Name = "Grimbergen",
        //                Description = "This is Grimbergen.",
        //                Photo = "https://img.saveur-biere.com/img/p/107-15017-v4_product_img.jpg",
        //                AlcoholPercentage = 6.2
        //                },
        //            new Beer()
        //                {
        //                Name = "Jupiler",
        //                Description = "This is Jupiler.",
        //                Photo = "https://img.saveur-biere.com/img/p/107-15017-v4_product_img.jpg",
        //                AlcoholPercentage = 2.9
        //                },
        //    };
        //    Beers = initBeers;
        //}

        //public async Task<List<Beer>> GetBeersAsync(string searchText)
        //{
        //    var response = await client.GetStringAsync("http://api.brewerydb.com/v2/search?q=" + searchText + "&type=beer&key=ea3f42048aa2b2e591a2be6861ca2f26");
        //    JObject jsonBeers = JObject.Parse(response);
        //    List<Beer> fetchedBeers = new List<Beer>();

        //    foreach (var x in jsonBeers["data"])
        //    {
        //        Beer beer = new Beer();
        //        beer.Name = (string)x["name"];
        //        fetchedBeers.Add(beer);
        //    }

        //    return fetchedBeers;
        //}

        public int Length
        {
            get { return Beers.Count(); }
            set { Length = value; }
        }

        public void MoveFirst()
        {
            CurrentPosition = 0;
        }

        public void MovePrev()
        {
            if (CurrentPosition > 0)
                --CurrentPosition;
        }

        public void MoveNext()
        {
            if (CurrentPosition < LastIndex)
                ++CurrentPosition;
        }

        public void MoveTo(int position)
        {
            if (position >= 0 && position <= LastIndex)
                CurrentPosition = position;
            else
                throw new IndexOutOfRangeException(
                    String.Format("{0} is an invalid position. Must be between 0 and {1}",
                                  position, LastIndex));
        }

        public Beer Current
        {
            get { return Beers.ElementAt(CurrentPosition); }
        }

        public int CurrentPosition
        {
            get;
            set;
        }

        public Boolean CanMovePrev
        {
            get { return CurrentPosition > 0; }
        }

        public Boolean CanMoveNext
        {
            get { return CurrentPosition < LastIndex; }
        }
    }
}
