using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using BeersLibrary;

namespace BeerAppAndroid
{
    public class BeersListManagerAdapter : BaseAdapter<Beer>
    {
        Context context;
        int layoutResouceId;
        BeersManager beersManager;


        public BeersListManagerAdapter(Context context,
                                       int layoutResouceId, BeersManager beersManager)
        {
            this.context = context;
            this.layoutResouceId = layoutResouceId;
            this.beersManager = beersManager;
        }


        public override Beer this[int position]
        {
            get
            {
                beersManager.MoveTo(position);
                return beersManager.Current;
            }
        }

        public override int Count
        {
            get { return beersManager.Length; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                LayoutInflater inflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(layoutResouceId, null);
            }

            TextView textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            textView.Text = this[position].Name + " (" + this[position].AlcoholPercentage + "%)";
            ImageView img = view.FindViewById<ImageView>(Android.Resource.Id.Icon);
            img.SetImageBitmap(ResourceHelper.GetImageBitmapFromUrl(this[position].Photo));

            return view;
        }
    }
}
