package md5170289e10c6841dc6a631d3a1b869eff;


public class BeerPagerAdapter
	extends android.support.v4.app.FragmentStatePagerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItem:(I)Landroid/support/v4/app/Fragment;:GetGetItem_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"";
		mono.android.Runtime.register ("BeerAppAndroid.BeerPagerAdapter, BeerAppAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BeerPagerAdapter.class, __md_methods);
	}


	public BeerPagerAdapter (android.support.v4.app.FragmentManager p0)
	{
		super (p0);
		if (getClass () == BeerPagerAdapter.class)
			mono.android.TypeManager.Activate ("BeerAppAndroid.BeerPagerAdapter, BeerAppAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Support.V4.App.FragmentManager, Xamarin.Android.Support.v4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=71f3e3261ac778b5", this, new java.lang.Object[] { p0 });
	}


	public android.support.v4.app.Fragment getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native android.support.v4.app.Fragment n_getItem (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
