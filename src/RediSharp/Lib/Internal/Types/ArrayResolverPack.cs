using System;
using System.Collections.Generic;
using RediSharp.Lua;
using RediSharp.RedIL.Enums;
using RediSharp.RedIL.Resolving.Attributes;
using RediSharp.RedIL.Resolving.CommonResolvers;

namespace RediSharp.Lib.Internal.Types
{
    class ArrayResolverPacks
    {
        [RedILDataType(DataValueType.Array)]
        class ArrayProxy
        {
            [RedILResolve(typeof(CallLuaBuiltinMemberResolver), LuaBuiltinMethod.TableGetN)]
            public int Length { get; }
        }

        public static Dictionary<Type, Type> GetMapToProxy()
        {
            return new Dictionary<Type, Type>()
            {
                { typeof(Array), typeof(ArrayProxy) }
            };
        }
    }
}