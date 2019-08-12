using System;
using System.Collections.Concurrent;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RedSharper.CSharp;
using RedSharper.Contracts;
using RedSharper.RedIL;
using System.Diagnostics;
using System.Reflection.Emit;
using RedSharper.Lua;

namespace RedSharper
{
    public class Client
    {
        private ActionDecompiler _decompiler;

        private CSharpCompiler _csharpCompiler;

        private LuaHandler _luaHandler;

        private ConcurrentDictionary<object, Lazy<RedILNode>> _redILCache;

        public Client(IDatabase db)
        {
            _decompiler = new ActionDecompiler();
            _csharpCompiler = new CSharpCompiler();
            _luaHandler = new LuaHandler(db);
        }

        public async Task<TRes> Execute<TRes>(Func<Cursor, RedisValue[], RedisKey[], TRes> action, RedisValue[] arguments = null, RedisKey[] keys = null)
            where TRes : RedResult
        {
            var handle = await GetInitializedHandle(action);
            var res = await handle.Execute<TRes>(arguments, keys);

            return res;
        }

        public IHandle GetHandle<TRes>(Func<Cursor, RedisValue[], RedisKey[], TRes> action)
            where TRes : RedResult
        {
            var decompilation = _decompiler.Decompile(action);
            var redIL = _csharpCompiler.Compile(decompilation);

            var handle = _luaHandler.CreateHandle(redIL);

            return handle;
        }

        public IHandle<TArtifact> GetHandleWithArtifact<TRes, TArtifact>(Func<Cursor, RedisValue[], RedisKey[], TRes> action)
            where TRes : RedResult
        {
            var decompilation = _decompiler.Decompile(action);
            var redIL = _csharpCompiler.Compile(decompilation);

            var handler = SelectHandler<TArtifact>();

            return handler.CreateHandle(redIL);
        }

        public async Task<IHandle> GetInitializedHandle<TRes>(Func<Cursor, RedisValue[], RedisKey[], TRes> action)
            where TRes : RedResult
        {
            var handle = GetHandle(action);
            await handle.Init();

            return handle;
        }

        private IHandler<TArtifact> SelectHandler<TArtifact>()
        {
            //For now only Lua
            return _luaHandler as IHandler<TArtifact>;
        }
    }
}