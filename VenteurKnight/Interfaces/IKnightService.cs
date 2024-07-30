using VenteurKnight.Models;

namespace VenteurKnight.Interfaces
{
    public interface IKnightService
    {
        public Task<GenericResponse<string>> CreateKnight(string start, string end);
        public Task<GenericResponse<Knight>> GetKnightPath(string guid);
    }
}
