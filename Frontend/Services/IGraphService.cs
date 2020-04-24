using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forward.Services
{
    public interface IGraphService
    {
        Task<List<int>> GetPathFromBFS(int startPoint, int endPoint);
        Task<List<int>> GetPathFromDFS(int startPoint, int endPoint);

    }
}
