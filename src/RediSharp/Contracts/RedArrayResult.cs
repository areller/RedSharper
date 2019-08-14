using System;
using RediSharp.Contracts.Enums;
using StackExchange.Redis;

namespace RediSharp.Contracts
{ 
    public class RedArrayResult : RedResult
    {
        internal override bool IsNull => _value == null;
        private readonly RedResult[] _value;

        internal override RedResultType Type => RedResultType.MultiBulk;
        public RedArrayResult(RedResult[] value)
        {
            _value = value;
        }

        public override string ToString() => _value == null ? "(nil)" : (_value.Length + " element(s)");
        
        #region Implicit Conversions

        public static implicit operator RedResult[] (RedArrayResult result) => result._value;

        #endregion
    }
}