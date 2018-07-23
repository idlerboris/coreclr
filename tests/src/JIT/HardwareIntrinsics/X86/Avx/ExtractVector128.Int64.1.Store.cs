// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void ExtractVector128Int641Store()
        {
            var test = new ExtractStoreTest__ExtractVector128Int641();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates basic functionality works, using Load
                    test.RunBasicScenario_Load();

                    // Validates basic functionality works, using LoadAligned
                    test.RunBasicScenario_LoadAligned();
                }

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario_Load();

                    // Validates calling via reflection works, using LoadAligned
                    test.RunReflectionScenario_LoadAligned();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                if (Avx.IsSupported)
                {
                    // Validates passing a local works, using Load
                    test.RunLclVarScenario_Load();

                    // Validates passing a local works, using LoadAligned
                    test.RunLclVarScenario_LoadAligned();
                }

                // Validates passing the field of a local class works
                test.RunClassLclFldScenario();

                // Validates passing an instance member of a class works
                test.RunClassFldScenario();

                // Validates passing the field of a local struct works
                test.RunStructLclFldScenario();

                // Validates passing an instance member of a struct works
                test.RunStructFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class ExtractStoreTest__ExtractVector128Int641
    {
        private struct TestStruct
        {
            public Vector256<Int64> _fld;

            public static TestStruct Create()
            {
                var testStruct = new TestStruct();
                var random = new Random();

                for (var i = 0; i < Op1ElementCount; i++) { _data[i] = (long)(random.Next(0, int.MaxValue)); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Int64>, byte>(ref testStruct._fld), ref Unsafe.As<Int64, byte>(ref _data[0]), (uint)Unsafe.SizeOf<Vector256<Int64>>());

                return testStruct;
            }

            public void RunStructFldScenario(ExtractStoreTest__ExtractVector128Int641 testClass)
            {
                Avx.ExtractVector128((Int64*)testClass._dataTable.outArrayPtr, _fld, 1);
                testClass.ValidateResult(_fld, testClass._dataTable.outArrayPtr);
            }
        }

        private static readonly int LargestVectorSize = 32;

        private static readonly int Op1ElementCount = Unsafe.SizeOf<Vector256<Int64>>() / sizeof(Int64);
        private static readonly int RetElementCount = Unsafe.SizeOf<Vector128<Int64>>() / sizeof(Int64);

        private static Int64[] _data = new Int64[Op1ElementCount];

        private static Vector256<Int64> _clsVar;

        private Vector256<Int64> _fld;

        private SimpleUnaryOpTest__DataTable<Int64, Int64> _dataTable;

        static ExtractStoreTest__ExtractVector128Int641()
        {
            var random = new Random();

            for (var i = 0; i < Op1ElementCount; i++) { _data[i] = (long)(random.Next(0, int.MaxValue)); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Int64>, byte>(ref _clsVar), ref Unsafe.As<Int64, byte>(ref _data[0]), (uint)Unsafe.SizeOf<Vector256<Int64>>());
        }

        public ExtractStoreTest__ExtractVector128Int641()
        {
            Succeeded = true;

            var random = new Random();

            for (var i = 0; i < Op1ElementCount; i++) { _data[i] = (long)(random.Next(0, int.MaxValue)); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Int64>, byte>(ref _fld), ref Unsafe.As<Int64, byte>(ref _data[0]), (uint)Unsafe.SizeOf<Vector256<Int64>>());

            for (var i = 0; i < Op1ElementCount; i++) { _data[i] = (long)(random.Next(0, int.MaxValue)); }
            _dataTable = new SimpleUnaryOpTest__DataTable<Int64, Int64>(_data, new Int64[RetElementCount], LargestVectorSize);
        }

        public bool IsSupported => Avx.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            Avx.ExtractVector128(
                (Int64*)_dataTable.outArrayPtr,
                Unsafe.Read<Vector256<Int64>>(_dataTable.inArrayPtr),
                1
            );

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            Avx.ExtractVector128(
                (Int64*)_dataTable.outArrayPtr,
                Avx.LoadVector256((Int64*)(_dataTable.inArrayPtr)),
                1
            );

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_LoadAligned()
        {
            Avx.ExtractVector128(
                (Int64*)_dataTable.outArrayPtr,
                Avx.LoadAlignedVector256((Int64*)(_dataTable.inArrayPtr)),
                1
            );

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            typeof(Avx).GetMethod(nameof(Avx.ExtractVector128), new Type[] { typeof(Int64*), typeof(Vector256<Int64>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Pointer.Box(_dataTable.outArrayPtr, typeof(Int64*)),
                                        Unsafe.Read<Vector256<Int64>>(_dataTable.inArrayPtr),
                                        (byte)1
                                     });

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            typeof(Avx).GetMethod(nameof(Avx.ExtractVector128), new Type[] { typeof(Int64*), typeof(Vector256<Int64>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Pointer.Box(_dataTable.outArrayPtr, typeof(Int64*)),
                                        Avx.LoadVector256((Int64*)(_dataTable.inArrayPtr)),
                                        (byte)1
                                     });

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_LoadAligned()
        {
            typeof(Avx).GetMethod(nameof(Avx.ExtractVector128), new Type[] {  typeof(Int64*), typeof(Vector256<Int64>), typeof(byte) })
                                     .Invoke(null, new object[] {
                                        Pointer.Box(_dataTable.outArrayPtr, typeof(Int64*)),
                                        Avx.LoadAlignedVector256((Int64*)(_dataTable.inArrayPtr)),
                                        (byte)1
                                     });

            ValidateResult(_dataTable.inArrayPtr, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            Avx.ExtractVector128(
                (Int64*)_dataTable.outArrayPtr,
                _clsVar,
                1
            );

            ValidateResult(_clsVar, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            var firstOp = Unsafe.Read<Vector256<Int64>>(_dataTable.inArrayPtr);
            Avx.ExtractVector128((Int64*)_dataTable.outArrayPtr, firstOp, 1);
            ValidateResult(firstOp, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_Load()
        {
            var firstOp = Avx.LoadVector256((Int64*)(_dataTable.inArrayPtr));
            Avx.ExtractVector128((Int64*)_dataTable.outArrayPtr, firstOp, 1);
            ValidateResult(firstOp, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            var firstOp = Avx.LoadAlignedVector256((Int64*)(_dataTable.inArrayPtr));
            Avx.ExtractVector128((Int64*)_dataTable.outArrayPtr, firstOp, 1);
            ValidateResult(firstOp, _dataTable.outArrayPtr);
        }

        public void RunClassLclFldScenario()
        {
            var test = new ExtractStoreTest__ExtractVector128Int641();
            Avx.ExtractVector128((Int64*)_dataTable.outArrayPtr, test._fld, 1);
            ValidateResult(test._fld, _dataTable.outArrayPtr);
        }

        public void RunClassFldScenario()
        {
            Avx.ExtractVector128((Int64*)_dataTable.outArrayPtr, _fld, 1);
            ValidateResult(_fld, _dataTable.outArrayPtr);
        }

        public void RunStructLclFldScenario()
        {
            var test = TestStruct.Create();
            Avx.ExtractVector128((Int64*)_dataTable.outArrayPtr, test._fld, 1);
            ValidateResult(test._fld, _dataTable.outArrayPtr);
        }

        public void RunStructFldScenario()
        {
            var test = TestStruct.Create();
            test.RunStructFldScenario(this);
        }

        public void RunUnsupportedScenario()
        {
            Succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                Succeeded = true;
            }
        }

        private void ValidateResult(Vector256<Int64> firstOp, void* result, [CallerMemberName] string method = "")
        {
            Int64[] inArray = new Int64[Op1ElementCount];
            Int64[] outArray = new Int64[RetElementCount];

            Unsafe.WriteUnaligned(ref Unsafe.As<Int64, byte>(ref inArray[0]), firstOp);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int64, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Int64>>());

            ValidateResult(inArray, outArray, method);
        }

        private void ValidateResult(void* firstOp, void* result, [CallerMemberName] string method = "")
        {
            Int64[] inArray = new Int64[Op1ElementCount];
            Int64[] outArray = new Int64[RetElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int64, byte>(ref inArray[0]), ref Unsafe.AsRef<byte>(firstOp), (uint)Unsafe.SizeOf<Vector256<Int64>>());
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int64, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), (uint)Unsafe.SizeOf<Vector128<Int64>>());

            ValidateResult(inArray, outArray, method);
        }

        private void ValidateResult(Int64[] firstOp, Int64[] result, [CallerMemberName] string method = "")
        {
            if (result[0] != firstOp[2])
            {
                Succeeded = false;
            }
            else
            {
                for (var i = 1; i < RetElementCount; i++)
                {
                    if ((result[i] != firstOp[i + 2]))
                    {
                        Succeeded = false;
                        break;
                    }
                }
            }

            if (!Succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"{nameof(Avx)}.{nameof(Avx.ExtractVector128)}<Int64>(Vector256<Int64><9>): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"  firstOp: ({string.Join(", ", firstOp)})");
                TestLibrary.TestFramework.LogInformation($"   result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);
            }
        }
    }
}