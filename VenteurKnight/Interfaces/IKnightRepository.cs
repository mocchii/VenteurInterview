using VenteurKnight.Models;

namespace VenteurKnight.Interfaces
{
    public interface IKnightRepository
    {
        Task<GenericResponse<Knight>> Create(Knight knight);
        Task<GenericResponse<Knight>> GetKnightByOperationId(string operationId);
    }
}
