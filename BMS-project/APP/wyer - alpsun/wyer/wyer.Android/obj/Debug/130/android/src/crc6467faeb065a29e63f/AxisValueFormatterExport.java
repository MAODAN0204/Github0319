package crc6467faeb065a29e63f;


public class AxisValueFormatterExport
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.github.mikephil.charting.formatter.IAxisValueFormatter
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getFormattedValue:(FLcom/github/mikephil/charting/components/AxisBase;)Ljava/lang/String;:GetGetFormattedValue_FLcom_github_mikephil_charting_components_AxisBase_Handler:MikePhil.Charting.Formatter.IAxisValueFormatterInvoker, MPAndroidChart\n" +
			"";
		mono.android.Runtime.register ("UltimateXF.Droid.Renderers.Exporters.AxisValueFormatterExport, UltimateXF.Droid", AxisValueFormatterExport.class, __md_methods);
	}


	public AxisValueFormatterExport ()
	{
		super ();
		if (getClass () == AxisValueFormatterExport.class) {
			mono.android.TypeManager.Activate ("UltimateXF.Droid.Renderers.Exporters.AxisValueFormatterExport, UltimateXF.Droid", "", this, new java.lang.Object[] {  });
		}
	}


	public java.lang.String getFormattedValue (float p0, com.github.mikephil.charting.components.AxisBase p1)
	{
		return n_getFormattedValue (p0, p1);
	}

	private native java.lang.String n_getFormattedValue (float p0, com.github.mikephil.charting.components.AxisBase p1);

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
