#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
227,
239,
240,
241,
242,
243,
244,
245,
246,
247,
250,
251,
252,
421,
422,
423,
453,
454,
455,
475,
476,
477,
478,
595,
596,
597,
600,
645,
646,
648,
650,
652,
654,
659,
667,
668,
669,
670,
671,
672,
673,
674,
675,
676,
677,
678,
679,
680,
681,
682,
683,
685,
686,
687,
688,
689,
690,
691,
781,
782,
783,
784,
785,
786,
787,
788,
789,
790,
791,
792,
793,
794,
795,
796,
797,
799,
800,
801,
802,
803,
804,
805,
872,
873,
940,
946,
949,
951,
956,
957,
959,
960,
964,
966,
967,
969,
971,
972,
975,
976,
977,
980,
982,
985,
987,
989,
998,
1066,
1068,
1070,
1080,
1081,
1082,
1083,
1085,
1092,
1093,
1094,
1095,
1096,
1104,
1105,
1106,
1110,
1111,
1113,
1117,
1118,
1119,
1398,
1588,
1589,
8322,
8323,
8325,
8326,
8327,
8328,
8329,
8331,
8333,
8335,
8336,
8337,
8348,
8350,
8357,
8359,
8361,
8363,
8412,
8418,
8419,
8421,
8422,
8423,
8424,
8425,
8427,
8429,
9454,
9458,
9460,
9461,
9462,
9463,
9718,
9719,
9720,
9721,
9741,
9742,
9743,
9745,
9747,
9800,
9886,
9888,
9890,
9899,
9900,
9901,
9902,
10367,
10368,
10372,
10373,
10407,
10442,
10449,
10456,
10467,
10471,
10494,
10577,
10579,
10589,
10591,
10592,
10593,
10600,
10615,
10635,
10636,
10644,
10646,
10653,
10654,
10657,
10659,
10664,
10670,
10671,
10678,
10680,
10692,
10695,
10696,
10697,
10708,
10717,
10723,
10724,
10725,
10727,
10728,
10745,
10747,
10761,
10781,
10782,
10809,
10839,
10840,
11436,
11455,
11549,
11550,
11773,
11774,
11784,
11785,
11786,
11792,
11887,
12458,
12459,
13022,
13027,
13037,
14401,
14422,
14424,
14426,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_InitializeInternal_raw (int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
void ves_icall_System_Enum_InternalBoxEnum_raw (int,int,int64_t,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Acos (double);
double ves_icall_System_Math_Acosh (double);
double ves_icall_System_Math_Asin (double);
double ves_icall_System_Math_Asinh (double);
double ves_icall_System_Math_Atan (double);
double ves_icall_System_Math_Atan2 (double,double);
double ves_icall_System_Math_Atanh (double);
double ves_icall_System_Math_Cbrt (double);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Cosh (double);
double ves_icall_System_Math_Exp (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sinh (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_Tanh (double);
double ves_icall_System_Math_FusedMultiplyAdd (double,double,double);
double ves_icall_System_Math_Log2 (double);
double ves_icall_System_Math_ModF (double,int);
float ves_icall_System_MathF_Acos (float);
float ves_icall_System_MathF_Acosh (float);
float ves_icall_System_MathF_Asin (float);
float ves_icall_System_MathF_Asinh (float);
float ves_icall_System_MathF_Atan (float);
float ves_icall_System_MathF_Atan2 (float,float);
float ves_icall_System_MathF_Atanh (float);
float ves_icall_System_MathF_Cbrt (float);
float ves_icall_System_MathF_Ceiling (float);
float ves_icall_System_MathF_Cos (float);
float ves_icall_System_MathF_Cosh (float);
float ves_icall_System_MathF_Exp (float);
float ves_icall_System_MathF_Floor (float);
float ves_icall_System_MathF_Log (float);
float ves_icall_System_MathF_Log10 (float);
float ves_icall_System_MathF_Pow (float,float);
float ves_icall_System_MathF_Sin (float);
float ves_icall_System_MathF_Sinh (float);
float ves_icall_System_MathF_Sqrt (float);
float ves_icall_System_MathF_Tan (float);
float ves_icall_System_MathF_Tanh (float);
float ves_icall_System_MathF_FusedMultiplyAdd (float,float,float);
float ves_icall_System_MathF_Log2 (float);
float ves_icall_System_MathF_ModF (float,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
void ves_icall_RuntimeType_GetInterfaceMapData_raw (int,int,int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_System_RuntimeType_AllocateValueType_raw (int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsComObject_raw (int,int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_String_InternalIsInterned_raw (int,int);
int ves_icall_System_String_InternalIntern_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Read_Long (int);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_StartInternal_raw (int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
void ves_icall_System_Runtime_InteropServices_Marshal_GetDelegateForFunctionPointerInternal_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw (int,int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_CustomAttributeBuilder_GetBlob_raw (int,int,int,int,int,int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
void ves_icall_System_Diagnostics_Debugger_Log (int,int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 227,
ves_icall_System_Array_InternalCreate,
// token 239,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 240,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 241,
ves_icall_System_Array_CanChangePrimitive,
// token 242,
ves_icall_System_Array_FastCopy,
// token 243,
ves_icall_System_Array_GetLengthInternal_raw,
// token 244,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 245,
ves_icall_System_Array_GetGenericValue_icall,
// token 246,
ves_icall_System_Array_GetValueImpl_raw,
// token 247,
ves_icall_System_Array_SetGenericValue_icall,
// token 250,
ves_icall_System_Array_SetValueImpl_raw,
// token 251,
ves_icall_System_Array_InitializeInternal_raw,
// token 252,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 421,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 422,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 423,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 453,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 454,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 455,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 475,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 476,
ves_icall_System_Enum_InternalBoxEnum_raw,
// token 477,
ves_icall_System_Enum_InternalGetCorElementType,
// token 478,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 595,
ves_icall_System_Environment_get_ProcessorCount,
// token 596,
ves_icall_System_Environment_get_TickCount,
// token 597,
ves_icall_System_Environment_get_TickCount64,
// token 600,
ves_icall_System_Environment_FailFast_raw,
// token 645,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 646,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 648,
ves_icall_System_GC_SuppressFinalize_raw,
// token 650,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 652,
ves_icall_System_GC_GetGCMemoryInfo,
// token 654,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 659,
ves_icall_System_Object_MemberwiseClone_raw,
// token 667,
ves_icall_System_Math_Acos,
// token 668,
ves_icall_System_Math_Acosh,
// token 669,
ves_icall_System_Math_Asin,
// token 670,
ves_icall_System_Math_Asinh,
// token 671,
ves_icall_System_Math_Atan,
// token 672,
ves_icall_System_Math_Atan2,
// token 673,
ves_icall_System_Math_Atanh,
// token 674,
ves_icall_System_Math_Cbrt,
// token 675,
ves_icall_System_Math_Ceiling,
// token 676,
ves_icall_System_Math_Cos,
// token 677,
ves_icall_System_Math_Cosh,
// token 678,
ves_icall_System_Math_Exp,
// token 679,
ves_icall_System_Math_Floor,
// token 680,
ves_icall_System_Math_Log,
// token 681,
ves_icall_System_Math_Log10,
// token 682,
ves_icall_System_Math_Pow,
// token 683,
ves_icall_System_Math_Sin,
// token 685,
ves_icall_System_Math_Sinh,
// token 686,
ves_icall_System_Math_Sqrt,
// token 687,
ves_icall_System_Math_Tan,
// token 688,
ves_icall_System_Math_Tanh,
// token 689,
ves_icall_System_Math_FusedMultiplyAdd,
// token 690,
ves_icall_System_Math_Log2,
// token 691,
ves_icall_System_Math_ModF,
// token 781,
ves_icall_System_MathF_Acos,
// token 782,
ves_icall_System_MathF_Acosh,
// token 783,
ves_icall_System_MathF_Asin,
// token 784,
ves_icall_System_MathF_Asinh,
// token 785,
ves_icall_System_MathF_Atan,
// token 786,
ves_icall_System_MathF_Atan2,
// token 787,
ves_icall_System_MathF_Atanh,
// token 788,
ves_icall_System_MathF_Cbrt,
// token 789,
ves_icall_System_MathF_Ceiling,
// token 790,
ves_icall_System_MathF_Cos,
// token 791,
ves_icall_System_MathF_Cosh,
// token 792,
ves_icall_System_MathF_Exp,
// token 793,
ves_icall_System_MathF_Floor,
// token 794,
ves_icall_System_MathF_Log,
// token 795,
ves_icall_System_MathF_Log10,
// token 796,
ves_icall_System_MathF_Pow,
// token 797,
ves_icall_System_MathF_Sin,
// token 799,
ves_icall_System_MathF_Sinh,
// token 800,
ves_icall_System_MathF_Sqrt,
// token 801,
ves_icall_System_MathF_Tan,
// token 802,
ves_icall_System_MathF_Tanh,
// token 803,
ves_icall_System_MathF_FusedMultiplyAdd,
// token 804,
ves_icall_System_MathF_Log2,
// token 805,
ves_icall_System_MathF_ModF,
// token 872,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 873,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 940,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 946,
ves_icall_RuntimeType_make_array_type_raw,
// token 949,
ves_icall_RuntimeType_make_byref_type_raw,
// token 951,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 956,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 957,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 959,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 960,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 964,
ves_icall_RuntimeType_GetInterfaceMapData_raw,
// token 966,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 967,
ves_icall_System_RuntimeType_AllocateValueType_raw,
// token 969,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 971,
ves_icall_System_RuntimeType_getFullName_raw,
// token 972,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 975,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 976,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 977,
ves_icall_RuntimeType_GetFields_native_raw,
// token 980,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 982,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 985,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 987,
ves_icall_RuntimeType_GetName_raw,
// token 989,
ves_icall_RuntimeType_GetNamespace_raw,
// token 998,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 1066,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 1068,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 1070,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 1080,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 1081,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 1082,
ves_icall_RuntimeTypeHandle_IsComObject_raw,
// token 1083,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 1085,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 1092,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 1093,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 1094,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 1095,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 1096,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 1104,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 1105,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 1106,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 1110,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 1111,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 1113,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 1117,
ves_icall_System_String_FastAllocateString_raw,
// token 1118,
ves_icall_System_String_InternalIsInterned_raw,
// token 1119,
ves_icall_System_String_InternalIntern_raw,
// token 1398,
ves_icall_System_Type_internal_from_handle_raw,
// token 1588,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1589,
ves_icall_System_ValueType_Equals_raw,
// token 8322,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 8323,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 8325,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 8326,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 8327,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 8328,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 8329,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 8331,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 8333,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 8335,
ves_icall_System_Threading_Interlocked_Read_Long,
// token 8336,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 8337,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 8348,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 8350,
mono_monitor_exit_icall_raw,
// token 8357,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 8359,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 8361,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 8363,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 8412,
ves_icall_System_Threading_Thread_StartInternal_raw,
// token 8418,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 8419,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 8421,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 8422,
ves_icall_System_Threading_Thread_GetState_raw,
// token 8423,
ves_icall_System_Threading_Thread_SetState_raw,
// token 8424,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 8425,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 8427,
ves_icall_System_Threading_Thread_YieldInternal,
// token 8429,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 9454,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 9458,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 9460,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 9461,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 9462,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 9463,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 9718,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 9719,
ves_icall_System_GCHandle_InternalFree_raw,
// token 9720,
ves_icall_System_GCHandle_InternalGet_raw,
// token 9721,
ves_icall_System_GCHandle_InternalSet_raw,
// token 9741,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 9742,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 9743,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 9745,
ves_icall_System_Runtime_InteropServices_Marshal_GetDelegateForFunctionPointerInternal_raw,
// token 9747,
ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw,
// token 9800,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 9886,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 9888,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw,
// token 9890,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 9899,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 9900,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 9901,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 9902,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 10367,
ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw,
// token 10368,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 10372,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 10373,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 10407,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 10442,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 10449,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 10456,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 10467,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 10471,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 10494,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 10577,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 10579,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 10589,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 10591,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 10592,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 10593,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 10600,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 10615,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 10635,
ves_icall_reflection_get_token_raw,
// token 10636,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 10644,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 10646,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 10653,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 10654,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 10657,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 10659,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 10664,
ves_icall_reflection_get_token_raw,
// token 10670,
ves_icall_get_method_info_raw,
// token 10671,
ves_icall_get_method_attributes,
// token 10678,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 10680,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 10692,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 10695,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 10696,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 10697,
ves_icall_reflection_get_token_raw,
// token 10708,
ves_icall_InternalInvoke_raw,
// token 10717,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 10723,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 10724,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 10725,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 10727,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 10728,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 10745,
ves_icall_InvokeClassConstructor_raw,
// token 10747,
ves_icall_InternalInvoke_raw,
// token 10761,
ves_icall_reflection_get_token_raw,
// token 10781,
ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw,
// token 10782,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 10809,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 10839,
ves_icall_reflection_get_token_raw,
// token 10840,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 11436,
ves_icall_CustomAttributeBuilder_GetBlob_raw,
// token 11455,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 11549,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 11550,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 11773,
ves_icall_ModuleBuilder_basic_init_raw,
// token 11774,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 11784,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 11785,
ves_icall_ModuleBuilder_getToken_raw,
// token 11786,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 11792,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 11887,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 12458,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 12459,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 13022,
ves_icall_System_Diagnostics_Debugger_Log,
// token 13027,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 13037,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 14401,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 14422,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 14424,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 14426,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
};
