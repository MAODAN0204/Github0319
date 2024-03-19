package crc641a0511e99377ff3e;


public class SupportBubbleChartExtendedRenderer
	extends crc641a0511e99377ff3e.SupportBarLineChartBaseExtendedRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("UltimateXF.Droid.Renderers.Extendeds.SupportBubbleChartExtendedRenderer, UltimateXF.Droid", SupportBubbleChartExtendedRenderer.class, __md_methods);
	}


	public SupportBubbleChartExtendedRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == SupportBubbleChartExtendedRenderer.class) {
			mono.android.TypeManager.Activate ("UltimateXF.Droid.Renderers.Extendeds.SupportBubbleChartExtendedRenderer, UltimateXF.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public SupportBubbleChartExtendedRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == SupportBubbleChartExtendedRenderer.class) {
			mono.android.TypeManager.Activate ("UltimateXF.Droid.Renderers.Extendeds.SupportBubbleChartExtendedRenderer, UltimateXF.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public SupportBubbleChartExtendedRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == SupportBubbleChartExtendedRenderer.class) {
			mono.android.TypeManager.Activate ("UltimateXF.Droid.Renderers.Extendeds.SupportBubbleChartExtendedRenderer, UltimateXF.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
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
