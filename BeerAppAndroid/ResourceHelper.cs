using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Android.Graphics;

namespace BeerAppAndroid
{
    public static class ResourceHelper
    {
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                if (url != ""){
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }else {
                    imageBitmap = null;
                }

            }

            return imageBitmap;
        }
    }
}
