using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RediSharp.UnitTests
{
    [TestClass]
    public class BasicCommandsTests
    {
        private Client<ICursor> _client;
        
        [TestInitialize]
        public void Setup()
        {
            _client = new Client<ICursor>(null);
        }
        
        [TestMethod]
        public void ShouldCompileSetCommand()
        {
            var handle = _client.GetLuaHandle((cursor, args, keys) => { return cursor.Set(keys[0], args[0]); });
        }
    }
}