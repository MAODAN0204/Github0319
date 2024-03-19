package crc6467faeb065a29e63f;


public class DataSetValueFormatterExport
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.github.mikephil.charting.formatter.IValueFormatter
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getFormattedValue:(FLcom/github/mikephil/charting/data/Entry;ILcom/github/mikephil/charting/utils/ViewPortHandler;)Ljava/lang/String;:GetGetFormattedValue_FLcom_github_mikephil_charting_data_Entry_ILcom_github_mikephil_charting_utils_ViewPortHandler_Handler:MikePhil.Charting.Formatter.IValueFormatterInvoker, MPAndroidChart\n" +
			"";
		mono.android.Runtime.register ("UltimateXF.Droid.Renderers.Exporters.DataSetValueFormatterExport, UltimateXF.Droid", DataSetValueFormatterExport.class, __md_methods);
	}


	public DataSetValueFormatterExport ()
	{
		super ();
		if (getClass () == DataSetValueFormatterExport.class) {
			mono.android.TypeManager.Activate ("UltimateXF.Droid.Renderers.Exporters.DataSetValueFormatterExport, UltimateXF.Droid", "", this, new java.lang.Object[] {  });
		}
	}


	public java.lang.String getFormattedValue (float p0, com.github.mikephil.charting.data.Entry p1, int p2, com.github.mikephil.charting.utils.ViewPortHandler p3)
	{
		return n_getFormattedValue (p0, p1, p2, p3);
	}

	private native java.lang.String n_getFormattedValue (float p0, com.github.mikephil.charting.data.Entry p1, int p2, com.github.mikephil.charting.utils.ViewPortHandler p3);

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
