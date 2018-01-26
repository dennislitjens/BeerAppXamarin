package md5170289e10c6841dc6a631d3a1b869eff;


public class BeerActivity
	extends android.support.v4.app.FragmentActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("BeerAppAndroid.BeerActivity, BeerAppAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BeerActivity.class, __md_methods);
	}


	public BeerActivity ()
	{
		super ();
		if (getClass () == BeerActivity.class)
			mono.android.TypeManager.Activate ("BeerAppAndroid.BeerActivity, BeerAppAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
