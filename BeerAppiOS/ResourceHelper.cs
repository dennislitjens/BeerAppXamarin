using System;
using System.Threading.Tasks;
using UIKit;
using System.Net.Http;
using Foundation;

namespace BeerAppiOS
{
    public static class ResourceHelper
    {
        public static async Task<UIImage> LoadImage(string imageUrl)
        {
            var httpClient = new HttpClient();

            Task<byte[]> contentsTask = httpClient.GetByteArrayAsync(imageUrl);

            var contents = await contentsTask;

            // load from bytes
            return UIImage.LoadFromData(NSData.FromArray(contents));
        }
    }
}
