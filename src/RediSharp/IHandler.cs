using System.Threading.Tasks;
using RediSharp.RedIL.Nodes;
using RediSharp.RedIL;

namespace RediSharp
{
    interface IHandler<TArtifact>
    {
        IHandle<TArtifact, TRes> CreateHandle<TRes>(RedILNode redIL);
    }
}