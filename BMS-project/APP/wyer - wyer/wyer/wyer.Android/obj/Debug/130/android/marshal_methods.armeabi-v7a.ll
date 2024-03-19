; ModuleID = 'obj\Debug\130\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Debug\130\android\marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [126 x i32] [
	i32 23291659, ; 0: MPAndroidChart => 0x163670b => 9
	i32 34715100, ; 1: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 51
	i32 39109920, ; 2: Newtonsoft.Json.dll => 0x254c520 => 11
	i32 57263871, ; 3: Xamarin.Forms.Core.dll => 0x369c6ff => 46
	i32 134690465, ; 4: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 55
	i32 182336117, ; 5: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 42
	i32 209399409, ; 6: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 27
	i32 312642273, ; 7: UltimateXF.Droid => 0x12a28ae1 => 22
	i32 318968648, ; 8: Xamarin.AndroidX.Activity.dll => 0x13031348 => 24
	i32 321597661, ; 9: System.Numerics => 0x132b30dd => 18
	i32 342366114, ; 10: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 36
	i32 442521989, ; 11: Xamarin.Essentials => 0x1a605985 => 45
	i32 450948140, ; 12: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 34
	i32 465846621, ; 13: mscorlib => 0x1bc4415d => 10
	i32 469710990, ; 14: System.dll => 0x1bff388e => 17
	i32 525008092, ; 15: SkiaSharp.dll => 0x1f4afcdc => 13
	i32 527452488, ; 16: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 55
	i32 551860129, ; 17: wyer.dll => 0x20e4b7a1 => 23
	i32 627609679, ; 18: Xamarin.AndroidX.CustomView => 0x2568904f => 32
	i32 690569205, ; 19: System.Xml.Linq.dll => 0x29293ff5 => 20
	i32 691348768, ; 20: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 57
	i32 700284507, ; 21: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 52
	i32 720511267, ; 22: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 56
	i32 809851609, ; 23: System.Drawing.Common.dll => 0x30455ad9 => 61
	i32 886248193, ; 24: Microcharts.Droid => 0x34d31301 => 6
	i32 902159924, ; 25: Rg.Plugins.Popup => 0x35c5de34 => 12
	i32 928116545, ; 26: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 51
	i32 955402788, ; 27: Newtonsoft.Json => 0x38f24a24 => 11
	i32 956575887, ; 28: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 56
	i32 967690846, ; 29: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 36
	i32 974778368, ; 30: FormsViewGroup.dll => 0x3a19f000 => 3
	i32 1012816738, ; 31: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 41
	i32 1035644815, ; 32: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 26
	i32 1042160112, ; 33: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 48
	i32 1052210849, ; 34: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 38
	i32 1084122840, ; 35: Xamarin.Kotlin.StdLib => 0x409e66d8 => 54
	i32 1098259244, ; 36: System => 0x41761b2c => 17
	i32 1275534314, ; 37: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 57
	i32 1293217323, ; 38: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 33
	i32 1365406463, ; 39: System.ServiceModel.Internals.dll => 0x516272ff => 59
	i32 1376866003, ; 40: Xamarin.AndroidX.SavedState => 0x52114ed3 => 41
	i32 1406073936, ; 41: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 29
	i32 1460219004, ; 42: Xamarin.Forms.Xaml => 0x57092c7c => 49
	i32 1469204771, ; 43: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 25
	i32 1582526884, ; 44: Microcharts.Forms.dll => 0x5e5371a4 => 7
	i32 1592978981, ; 45: System.Runtime.Serialization.dll => 0x5ef2ee25 => 2
	i32 1622152042, ; 46: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 39
	i32 1636350590, ; 47: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 31
	i32 1639515021, ; 48: System.Net.Http.dll => 0x61b9038d => 1
	i32 1658251792, ; 49: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 50
	i32 1698840827, ; 50: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 53
	i32 1722051300, ; 51: SkiaSharp.Views.Forms => 0x66a46ae4 => 15
	i32 1729485958, ; 52: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 28
	i32 1766324549, ; 53: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 42
	i32 1776026572, ; 54: System.Core.dll => 0x69dc03cc => 16
	i32 1788241197, ; 55: Xamarin.AndroidX.Fragment => 0x6a96652d => 34
	i32 1806518557, ; 56: wyer => 0x6bad491d => 23
	i32 1808609942, ; 57: Xamarin.AndroidX.Loader => 0x6bcd3296 => 39
	i32 1813058853, ; 58: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 54
	i32 1813201214, ; 59: Xamarin.Google.Android.Material => 0x6c13413e => 50
	i32 1867746548, ; 60: Xamarin.Essentials.dll => 0x6f538cf4 => 45
	i32 1878053835, ; 61: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 49
	i32 1983156543, ; 62: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 53
	i32 2019465201, ; 63: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 38
	i32 2055257422, ; 64: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 37
	i32 2097448633, ; 65: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 35
	i32 2126786730, ; 66: Xamarin.Forms.Platform.Android => 0x7ec430aa => 47
	i32 2201107256, ; 67: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 58
	i32 2201231467, ; 68: System.Net.Http => 0x8334206b => 1
	i32 2279755925, ; 69: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 40
	i32 2475788418, ; 70: Java.Interop.dll => 0x93918882 => 4
	i32 2605712449, ; 71: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 58
	i32 2620871830, ; 72: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 31
	i32 2732626843, ; 73: Xamarin.AndroidX.Activity => 0xa2e0939b => 24
	i32 2737747696, ; 74: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 25
	i32 2766581644, ; 75: Xamarin.Forms.Core => 0xa4e6af8c => 46
	i32 2770495804, ; 76: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 52
	i32 2778768386, ; 77: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 43
	i32 2795602088, ; 78: SkiaSharp.Views.Android.dll => 0xa6a180a8 => 14
	i32 2810250172, ; 79: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 29
	i32 2819470561, ; 80: System.Xml.dll => 0xa80db4e1 => 19
	i32 2853208004, ; 81: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 43
	i32 2861816565, ; 82: Rg.Plugins.Popup.dll => 0xaa93daf5 => 12
	i32 2905242038, ; 83: mscorlib.dll => 0xad2a79b6 => 10
	i32 2912489636, ; 84: SkiaSharp.Views.Android => 0xad9910a4 => 14
	i32 2974793899, ; 85: SkiaSharp.Views.Forms.dll => 0xb14fc0ab => 15
	i32 2978675010, ; 86: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 33
	i32 3036068679, ; 87: Microcharts.Droid.dll => 0xb4f6bb47 => 6
	i32 3044182254, ; 88: FormsViewGroup => 0xb57288ee => 3
	i32 3111772706, ; 89: System.Runtime.Serialization => 0xb979e222 => 2
	i32 3145916505, ; 90: MPAndroidChart.dll => 0xbb82e059 => 9
	i32 3204380047, ; 91: System.Data.dll => 0xbefef58f => 60
	i32 3247949154, ; 92: Mono.Security => 0xc197c562 => 62
	i32 3258312781, ; 93: Xamarin.AndroidX.CardView => 0xc235e84d => 28
	i32 3290302411, ; 94: wyer.Android.dll => 0xc41e07cb => 0
	i32 3310323537, ; 95: UltimateXF.dll => 0xc54f8751 => 21
	i32 3317135071, ; 96: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 32
	i32 3317144872, ; 97: System.Data => 0xc5b79d28 => 60
	i32 3340387945, ; 98: SkiaSharp => 0xc71a4669 => 13
	i32 3353484488, ; 99: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 35
	i32 3353544232, ; 100: Xamarin.CommunityToolkit.dll => 0xc7e30628 => 44
	i32 3362522851, ; 101: Xamarin.AndroidX.Core => 0xc86c06e3 => 30
	i32 3366347497, ; 102: Java.Interop => 0xc8a662e9 => 4
	i32 3374999561, ; 103: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 40
	i32 3404865022, ; 104: System.ServiceModel.Internals => 0xcaf21dfe => 59
	i32 3407215217, ; 105: Xamarin.CommunityToolkit => 0xcb15fa71 => 44
	i32 3429136800, ; 106: System.Xml => 0xcc6479a0 => 19
	i32 3455791806, ; 107: Microcharts => 0xcdfb32be => 5
	i32 3476120550, ; 108: Mono.Android => 0xcf3163e6 => 8
	i32 3509114376, ; 109: System.Xml.Linq => 0xd128d608 => 20
	i32 3536029504, ; 110: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 47
	i32 3632359727, ; 111: Xamarin.Forms.Platform => 0xd881692f => 48
	i32 3641597786, ; 112: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 37
	i32 3668042751, ; 113: Microcharts.dll => 0xdaa1e3ff => 5
	i32 3672681054, ; 114: Mono.Android.dll => 0xdae8aa5e => 8
	i32 3682565725, ; 115: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 27
	i32 3689375977, ; 116: System.Drawing.Common => 0xdbe768e9 => 61
	i32 3700871608, ; 117: UltimateXF => 0xdc96d1b8 => 21
	i32 3718709988, ; 118: wyer.Android => 0xdda702e4 => 0
	i32 3829621856, ; 119: System.Numerics.dll => 0xe4436460 => 18
	i32 3896760992, ; 120: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 30
	i32 3903721208, ; 121: Microcharts.Forms => 0xe8ae0ef8 => 7
	i32 3955647286, ; 122: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 26
	i32 4086946572, ; 123: UltimateXF.Droid.dll => 0xf399db0c => 22
	i32 4105002889, ; 124: Mono.Security.dll => 0xf4ad5f89 => 62
	i32 4151237749 ; 125: System.Core => 0xf76edc75 => 16
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [126 x i32] [
	i32 9, i32 51, i32 11, i32 46, i32 55, i32 42, i32 27, i32 22, ; 0..7
	i32 24, i32 18, i32 36, i32 45, i32 34, i32 10, i32 17, i32 13, ; 8..15
	i32 55, i32 23, i32 32, i32 20, i32 57, i32 52, i32 56, i32 61, ; 16..23
	i32 6, i32 12, i32 51, i32 11, i32 56, i32 36, i32 3, i32 41, ; 24..31
	i32 26, i32 48, i32 38, i32 54, i32 17, i32 57, i32 33, i32 59, ; 32..39
	i32 41, i32 29, i32 49, i32 25, i32 7, i32 2, i32 39, i32 31, ; 40..47
	i32 1, i32 50, i32 53, i32 15, i32 28, i32 42, i32 16, i32 34, ; 48..55
	i32 23, i32 39, i32 54, i32 50, i32 45, i32 49, i32 53, i32 38, ; 56..63
	i32 37, i32 35, i32 47, i32 58, i32 1, i32 40, i32 4, i32 58, ; 64..71
	i32 31, i32 24, i32 25, i32 46, i32 52, i32 43, i32 14, i32 29, ; 72..79
	i32 19, i32 43, i32 12, i32 10, i32 14, i32 15, i32 33, i32 6, ; 80..87
	i32 3, i32 2, i32 9, i32 60, i32 62, i32 28, i32 0, i32 21, ; 88..95
	i32 32, i32 60, i32 13, i32 35, i32 44, i32 30, i32 4, i32 40, ; 96..103
	i32 59, i32 44, i32 19, i32 5, i32 8, i32 20, i32 47, i32 48, ; 104..111
	i32 37, i32 5, i32 8, i32 27, i32 61, i32 21, i32 0, i32 18, ; 112..119
	i32 30, i32 7, i32 26, i32 22, i32 62, i32 16 ; 120..125
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="all" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
