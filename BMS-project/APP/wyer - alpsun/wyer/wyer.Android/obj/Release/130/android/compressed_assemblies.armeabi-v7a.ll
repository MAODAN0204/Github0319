; ModuleID = 'obj\Release\130\android\compressed_assemblies.armeabi-v7a.ll'
source_filename = "obj\Release\130\android\compressed_assemblies.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


%struct.CompressedAssemblyDescriptor = type {
	i32,; uint32_t uncompressed_file_size
	i8,; bool loaded
	i8*; uint8_t* data
}

%struct.CompressedAssemblies = type {
	i32,; uint32_t count
	%struct.CompressedAssemblyDescriptor*; CompressedAssemblyDescriptor* descriptors
}
@__CompressedAssemblyDescriptor_data_0 = internal global [15872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_1 = internal global [168960 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_2 = internal global [1422336 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_3 = internal global [5632 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_4 = internal global [6144 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_5 = internal global [39936 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_6 = internal global [2721792 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_7 = internal global [121856 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_8 = internal global [690176 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_9 = internal global [218112 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_10 = internal global [49504 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_11 = internal global [155488 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_12 = internal global [405872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_13 = internal global [392192 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_14 = internal global [747520 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_15 = internal global [34304 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_16 = internal global [218624 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_17 = internal global [38912 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_18 = internal global [419328 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_19 = internal global [55808 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_20 = internal global [65024 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_21 = internal global [1397760 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_22 = internal global [897536 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_23 = internal global [130048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_24 = internal global [58880 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_25 = internal global [64000 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_26 = internal global [16896 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_27 = internal global [530432 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_28 = internal global [30720 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_29 = internal global [17920 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_30 = internal global [79360 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_31 = internal global [635904 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_32 = internal global [25600 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_33 = internal global [9728 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_34 = internal global [44032 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_35 = internal global [201216 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_36 = internal global [15872 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_37 = internal global [16896 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_38 = internal global [16896 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_39 = internal global [20480 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_40 = internal global [37376 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_41 = internal global [424448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_42 = internal global [14336 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_43 = internal global [40448 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_44 = internal global [58368 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_45 = internal global [479744 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_46 = internal global [30208 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_47 = internal global [1218560 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_48 = internal global [1046528 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_49 = internal global [349232 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_50 = internal global [103424 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_51 = internal global [258048 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_52 = internal global [23432 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_53 = internal global [152984 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_54 = internal global [15256 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_55 = internal global [15256 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_56 = internal global [15240 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_57 = internal global [2214288 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_58 = internal global [26512 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_59 = internal global [317320 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_60 = internal global [671232 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_61 = internal global [275456 x i8] zeroinitializer, align 1
@__CompressedAssemblyDescriptor_data_62 = internal global [2116096 x i8] zeroinitializer, align 1


; Compressed assembly data storage
@compressed_assembly_descriptors = internal global [63 x %struct.CompressedAssemblyDescriptor] [
	; 0
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_0, i32 0, i32 0); data
	}, 
	; 1
	%struct.CompressedAssemblyDescriptor {
		i32 168960, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([168960 x i8], [168960 x i8]* @__CompressedAssemblyDescriptor_data_1, i32 0, i32 0); data
	}, 
	; 2
	%struct.CompressedAssemblyDescriptor {
		i32 1422336, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1422336 x i8], [1422336 x i8]* @__CompressedAssemblyDescriptor_data_2, i32 0, i32 0); data
	}, 
	; 3
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([5632 x i8], [5632 x i8]* @__CompressedAssemblyDescriptor_data_3, i32 0, i32 0); data
	}, 
	; 4
	%struct.CompressedAssemblyDescriptor {
		i32 6144, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([6144 x i8], [6144 x i8]* @__CompressedAssemblyDescriptor_data_4, i32 0, i32 0); data
	}, 
	; 5
	%struct.CompressedAssemblyDescriptor {
		i32 39936, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([39936 x i8], [39936 x i8]* @__CompressedAssemblyDescriptor_data_5, i32 0, i32 0); data
	}, 
	; 6
	%struct.CompressedAssemblyDescriptor {
		i32 2721792, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2721792 x i8], [2721792 x i8]* @__CompressedAssemblyDescriptor_data_6, i32 0, i32 0); data
	}, 
	; 7
	%struct.CompressedAssemblyDescriptor {
		i32 121856, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([121856 x i8], [121856 x i8]* @__CompressedAssemblyDescriptor_data_7, i32 0, i32 0); data
	}, 
	; 8
	%struct.CompressedAssemblyDescriptor {
		i32 690176, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([690176 x i8], [690176 x i8]* @__CompressedAssemblyDescriptor_data_8, i32 0, i32 0); data
	}, 
	; 9
	%struct.CompressedAssemblyDescriptor {
		i32 218112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([218112 x i8], [218112 x i8]* @__CompressedAssemblyDescriptor_data_9, i32 0, i32 0); data
	}, 
	; 10
	%struct.CompressedAssemblyDescriptor {
		i32 49504, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([49504 x i8], [49504 x i8]* @__CompressedAssemblyDescriptor_data_10, i32 0, i32 0); data
	}, 
	; 11
	%struct.CompressedAssemblyDescriptor {
		i32 155488, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([155488 x i8], [155488 x i8]* @__CompressedAssemblyDescriptor_data_11, i32 0, i32 0); data
	}, 
	; 12
	%struct.CompressedAssemblyDescriptor {
		i32 405872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([405872 x i8], [405872 x i8]* @__CompressedAssemblyDescriptor_data_12, i32 0, i32 0); data
	}, 
	; 13
	%struct.CompressedAssemblyDescriptor {
		i32 392192, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([392192 x i8], [392192 x i8]* @__CompressedAssemblyDescriptor_data_13, i32 0, i32 0); data
	}, 
	; 14
	%struct.CompressedAssemblyDescriptor {
		i32 747520, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([747520 x i8], [747520 x i8]* @__CompressedAssemblyDescriptor_data_14, i32 0, i32 0); data
	}, 
	; 15
	%struct.CompressedAssemblyDescriptor {
		i32 34304, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([34304 x i8], [34304 x i8]* @__CompressedAssemblyDescriptor_data_15, i32 0, i32 0); data
	}, 
	; 16
	%struct.CompressedAssemblyDescriptor {
		i32 218624, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([218624 x i8], [218624 x i8]* @__CompressedAssemblyDescriptor_data_16, i32 0, i32 0); data
	}, 
	; 17
	%struct.CompressedAssemblyDescriptor {
		i32 38912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([38912 x i8], [38912 x i8]* @__CompressedAssemblyDescriptor_data_17, i32 0, i32 0); data
	}, 
	; 18
	%struct.CompressedAssemblyDescriptor {
		i32 419328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([419328 x i8], [419328 x i8]* @__CompressedAssemblyDescriptor_data_18, i32 0, i32 0); data
	}, 
	; 19
	%struct.CompressedAssemblyDescriptor {
		i32 55808, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([55808 x i8], [55808 x i8]* @__CompressedAssemblyDescriptor_data_19, i32 0, i32 0); data
	}, 
	; 20
	%struct.CompressedAssemblyDescriptor {
		i32 65024, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([65024 x i8], [65024 x i8]* @__CompressedAssemblyDescriptor_data_20, i32 0, i32 0); data
	}, 
	; 21
	%struct.CompressedAssemblyDescriptor {
		i32 1397760, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1397760 x i8], [1397760 x i8]* @__CompressedAssemblyDescriptor_data_21, i32 0, i32 0); data
	}, 
	; 22
	%struct.CompressedAssemblyDescriptor {
		i32 897536, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([897536 x i8], [897536 x i8]* @__CompressedAssemblyDescriptor_data_22, i32 0, i32 0); data
	}, 
	; 23
	%struct.CompressedAssemblyDescriptor {
		i32 130048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([130048 x i8], [130048 x i8]* @__CompressedAssemblyDescriptor_data_23, i32 0, i32 0); data
	}, 
	; 24
	%struct.CompressedAssemblyDescriptor {
		i32 58880, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([58880 x i8], [58880 x i8]* @__CompressedAssemblyDescriptor_data_24, i32 0, i32 0); data
	}, 
	; 25
	%struct.CompressedAssemblyDescriptor {
		i32 64000, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([64000 x i8], [64000 x i8]* @__CompressedAssemblyDescriptor_data_25, i32 0, i32 0); data
	}, 
	; 26
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16896 x i8], [16896 x i8]* @__CompressedAssemblyDescriptor_data_26, i32 0, i32 0); data
	}, 
	; 27
	%struct.CompressedAssemblyDescriptor {
		i32 530432, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([530432 x i8], [530432 x i8]* @__CompressedAssemblyDescriptor_data_27, i32 0, i32 0); data
	}, 
	; 28
	%struct.CompressedAssemblyDescriptor {
		i32 30720, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([30720 x i8], [30720 x i8]* @__CompressedAssemblyDescriptor_data_28, i32 0, i32 0); data
	}, 
	; 29
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17920 x i8], [17920 x i8]* @__CompressedAssemblyDescriptor_data_29, i32 0, i32 0); data
	}, 
	; 30
	%struct.CompressedAssemblyDescriptor {
		i32 79360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([79360 x i8], [79360 x i8]* @__CompressedAssemblyDescriptor_data_30, i32 0, i32 0); data
	}, 
	; 31
	%struct.CompressedAssemblyDescriptor {
		i32 635904, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([635904 x i8], [635904 x i8]* @__CompressedAssemblyDescriptor_data_31, i32 0, i32 0); data
	}, 
	; 32
	%struct.CompressedAssemblyDescriptor {
		i32 25600, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([25600 x i8], [25600 x i8]* @__CompressedAssemblyDescriptor_data_32, i32 0, i32 0); data
	}, 
	; 33
	%struct.CompressedAssemblyDescriptor {
		i32 9728, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([9728 x i8], [9728 x i8]* @__CompressedAssemblyDescriptor_data_33, i32 0, i32 0); data
	}, 
	; 34
	%struct.CompressedAssemblyDescriptor {
		i32 44032, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([44032 x i8], [44032 x i8]* @__CompressedAssemblyDescriptor_data_34, i32 0, i32 0); data
	}, 
	; 35
	%struct.CompressedAssemblyDescriptor {
		i32 201216, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([201216 x i8], [201216 x i8]* @__CompressedAssemblyDescriptor_data_35, i32 0, i32 0); data
	}, 
	; 36
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_36, i32 0, i32 0); data
	}, 
	; 37
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16896 x i8], [16896 x i8]* @__CompressedAssemblyDescriptor_data_37, i32 0, i32 0); data
	}, 
	; 38
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16896 x i8], [16896 x i8]* @__CompressedAssemblyDescriptor_data_38, i32 0, i32 0); data
	}, 
	; 39
	%struct.CompressedAssemblyDescriptor {
		i32 20480, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([20480 x i8], [20480 x i8]* @__CompressedAssemblyDescriptor_data_39, i32 0, i32 0); data
	}, 
	; 40
	%struct.CompressedAssemblyDescriptor {
		i32 37376, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([37376 x i8], [37376 x i8]* @__CompressedAssemblyDescriptor_data_40, i32 0, i32 0); data
	}, 
	; 41
	%struct.CompressedAssemblyDescriptor {
		i32 424448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([424448 x i8], [424448 x i8]* @__CompressedAssemblyDescriptor_data_41, i32 0, i32 0); data
	}, 
	; 42
	%struct.CompressedAssemblyDescriptor {
		i32 14336, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14336 x i8], [14336 x i8]* @__CompressedAssemblyDescriptor_data_42, i32 0, i32 0); data
	}, 
	; 43
	%struct.CompressedAssemblyDescriptor {
		i32 40448, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([40448 x i8], [40448 x i8]* @__CompressedAssemblyDescriptor_data_43, i32 0, i32 0); data
	}, 
	; 44
	%struct.CompressedAssemblyDescriptor {
		i32 58368, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([58368 x i8], [58368 x i8]* @__CompressedAssemblyDescriptor_data_44, i32 0, i32 0); data
	}, 
	; 45
	%struct.CompressedAssemblyDescriptor {
		i32 479744, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([479744 x i8], [479744 x i8]* @__CompressedAssemblyDescriptor_data_45, i32 0, i32 0); data
	}, 
	; 46
	%struct.CompressedAssemblyDescriptor {
		i32 30208, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([30208 x i8], [30208 x i8]* @__CompressedAssemblyDescriptor_data_46, i32 0, i32 0); data
	}, 
	; 47
	%struct.CompressedAssemblyDescriptor {
		i32 1218560, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1218560 x i8], [1218560 x i8]* @__CompressedAssemblyDescriptor_data_47, i32 0, i32 0); data
	}, 
	; 48
	%struct.CompressedAssemblyDescriptor {
		i32 1046528, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1046528 x i8], [1046528 x i8]* @__CompressedAssemblyDescriptor_data_48, i32 0, i32 0); data
	}, 
	; 49
	%struct.CompressedAssemblyDescriptor {
		i32 349232, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([349232 x i8], [349232 x i8]* @__CompressedAssemblyDescriptor_data_49, i32 0, i32 0); data
	}, 
	; 50
	%struct.CompressedAssemblyDescriptor {
		i32 103424, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([103424 x i8], [103424 x i8]* @__CompressedAssemblyDescriptor_data_50, i32 0, i32 0); data
	}, 
	; 51
	%struct.CompressedAssemblyDescriptor {
		i32 258048, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([258048 x i8], [258048 x i8]* @__CompressedAssemblyDescriptor_data_51, i32 0, i32 0); data
	}, 
	; 52
	%struct.CompressedAssemblyDescriptor {
		i32 23432, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([23432 x i8], [23432 x i8]* @__CompressedAssemblyDescriptor_data_52, i32 0, i32 0); data
	}, 
	; 53
	%struct.CompressedAssemblyDescriptor {
		i32 152984, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([152984 x i8], [152984 x i8]* @__CompressedAssemblyDescriptor_data_53, i32 0, i32 0); data
	}, 
	; 54
	%struct.CompressedAssemblyDescriptor {
		i32 15256, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15256 x i8], [15256 x i8]* @__CompressedAssemblyDescriptor_data_54, i32 0, i32 0); data
	}, 
	; 55
	%struct.CompressedAssemblyDescriptor {
		i32 15256, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15256 x i8], [15256 x i8]* @__CompressedAssemblyDescriptor_data_55, i32 0, i32 0); data
	}, 
	; 56
	%struct.CompressedAssemblyDescriptor {
		i32 15240, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15240 x i8], [15240 x i8]* @__CompressedAssemblyDescriptor_data_56, i32 0, i32 0); data
	}, 
	; 57
	%struct.CompressedAssemblyDescriptor {
		i32 2214288, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2214288 x i8], [2214288 x i8]* @__CompressedAssemblyDescriptor_data_57, i32 0, i32 0); data
	}, 
	; 58
	%struct.CompressedAssemblyDescriptor {
		i32 26512, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([26512 x i8], [26512 x i8]* @__CompressedAssemblyDescriptor_data_58, i32 0, i32 0); data
	}, 
	; 59
	%struct.CompressedAssemblyDescriptor {
		i32 317320, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([317320 x i8], [317320 x i8]* @__CompressedAssemblyDescriptor_data_59, i32 0, i32 0); data
	}, 
	; 60
	%struct.CompressedAssemblyDescriptor {
		i32 671232, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([671232 x i8], [671232 x i8]* @__CompressedAssemblyDescriptor_data_60, i32 0, i32 0); data
	}, 
	; 61
	%struct.CompressedAssemblyDescriptor {
		i32 275456, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([275456 x i8], [275456 x i8]* @__CompressedAssemblyDescriptor_data_61, i32 0, i32 0); data
	}, 
	; 62
	%struct.CompressedAssemblyDescriptor {
		i32 2116096, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2116096 x i8], [2116096 x i8]* @__CompressedAssemblyDescriptor_data_62, i32 0, i32 0); data
	}
], align 4; end of 'compressed_assembly_descriptors' array


; compressed_assemblies
@compressed_assemblies = local_unnamed_addr global %struct.CompressedAssemblies {
	i32 63, ; count
	%struct.CompressedAssemblyDescriptor* getelementptr inbounds ([63 x %struct.CompressedAssemblyDescriptor], [63 x %struct.CompressedAssemblyDescriptor]* @compressed_assembly_descriptors, i32 0, i32 0); descriptors
}, align 4


!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
