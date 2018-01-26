using System;
using SQLite;

namespace BeersLibrary
{
    public class Beer
    {
        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String Photo
        {
            get;
            set;
        }

        public String Description
        {
            get;
            set;
        }

        public double AlcoholPercentage
        {
            get;
            set;
        }
    }
}
