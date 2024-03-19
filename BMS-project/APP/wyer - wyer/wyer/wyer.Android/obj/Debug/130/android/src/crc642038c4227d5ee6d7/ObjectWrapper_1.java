package crc642038c4227d5ee6d7;


public class ObjectWrapper_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("UltimateXF.Droid.ObjectWrapper`1, UltimateXF.Droid", ObjectWrapper_1.class, __md_methods);
	}


	public ObjectWrapper_1 ()
	{
		super ();
		if (getClass () == ObjectWrapper_1.class) {
			mono.android.TypeManager.Activate ("UltimateXF.Droid.ObjectWrapper`1, UltimateXF.Droid", "", this, new java.lang.Object[] {  });
		}
	}

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
