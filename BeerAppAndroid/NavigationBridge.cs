using System;
namespace BeerAppAndroid
{
    public static class NavigationBridge
    {
        public static Action<object> FinishedNavigating { get; set; }
    }

}
