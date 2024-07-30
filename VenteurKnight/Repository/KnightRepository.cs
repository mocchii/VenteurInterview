using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VenteurKnight.Interfaces;
using VenteurKnight.Models;

namespace VenteurKnight.Repository
{
    public class KnightRepository : IKnightRepository
    {
        private CodingInterviewContext _codingInterviewContext;
        public KnightRepository(CodingInterviewContext codingInterviewContext) {
            _codingInterviewContext = codingInterviewContext;
        }

        public async Task<GenericResponse<Knight>> Create(Knight knight) {
            try {
                knight.CreatedAt = DateTime.UtcNow;
                knight.IsActive = true;
                var result = await _codingInterviewContext.AddAsync(knight);
                await _codingInterviewContext.SaveChangesAsync();
                return new GenericResponse<Knight>()
                {
                    Data = result.Entity,
                    Success = true,
                    Message = "Created data."
                };
            }catch (Exception ex)
            {
                return new GenericResponse<Knight>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "Internal server error."
                };
            }
        }

        public async Task<GenericResponse<Knight>> GetKnightByOperationId(string operationId)
        {
            try
            {
                var result = await _codingInterviewContext.Knights.FromSqlInterpolated($"Select top 1 * From dbo.Knights Where IsActive = 1 AND operationId = {operationId}").FirstOrDefaultAsync();

                return new GenericResponse<Knight>()
                {
                    Data = result,
                    Success = result != null ? true : false,
                    Message = result != null ? "Found data." : "No data.",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Knight>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "Internal server error."
                };
            }
        }
    }
}
