using System.Text;
using VenteurKnight.Interfaces;
using VenteurKnight.Models;
using VenteurKnight.Repository;

namespace VenteurKnight.Service
{
    public class KnightService : IKnightService
    {
        private IKnightRepository _knightRepository;

        const string LETTERS = "ABCDEFGH";
        const string NUMBERS = "12345678";
        public KnightService(IKnightRepository knightRepository) {
            _knightRepository = knightRepository;
        }
        public async Task<GenericResponse<string>> CreateKnight(string source, string target)
        {
            try {
                //Validate
                if (source.Length != 2 || target.Length != 2 || !LETTERS.Contains(source[0]) || !NUMBERS.Contains(source[1]) ||
                    !LETTERS.Contains(target[0]) || !NUMBERS.Contains(target[1]))
                {
                    throw new ArgumentException("Input must be in A1 to H8 format");
                }
                Knight knight = new Knight()
                {
                    OperationId = Guid.NewGuid().ToString(),
                    Starting = source,
                    Ending = target,
                };
                
                var resultKnight = await CalculatePath(knight);
                if (resultKnight == null) {
                    throw new Exception("Could not find solution");
                }
                var result = await _knightRepository.Create(resultKnight);
                var resultMsg = $"Operation Id {knight.OperationId} was created. Please query it to find your results.";
                return new GenericResponse<string>()
                {
                    Data = resultMsg,
                    Success = true,
                    Message = "Data created."
                };
            }
            catch (ArgumentException ex)
            {
                return new GenericResponse<string>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<string>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "Internal server error."
                };
            }
        }

        public async Task<GenericResponse<Knight>> GetKnightPath(string guid)
        {
            return await _knightRepository.GetKnightByOperationId(guid);
        }

        private Task<Knight> CalculatePath(Knight knight) {
            int[] x = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
            int[] y = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
            KnightPath knightPath = new KnightPath(knight);
            int endX = (knight.Ending[0] - 'A');
            int endY = knight.Ending[1] - '0' - 1;
            Queue<KnightPath> queue = new Queue<KnightPath>();
            queue.Enqueue(knightPath);
            bool[][] visited = new bool[8][];
            for (int v = 0; v < visited.Length; v++)
            {
                visited[v] = new bool[8];
            }
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.CurrentX == endX && current.CurrentY == endY)
                {
                    current.ShortestPath = string.Join(",", current.PathList);
                    return Task.FromResult<Knight>(current);
                }
                for (int i = 0; i < x.Length; i++)
                {
                    var move = (KnightPath)current.Clone();
                    move.CurrentX += x[i];
                    move.CurrentY += y[i];
                    move.PathList = current.PathList.ToList();
                    move.NumberOfMoves++;
                    if (ValidBoard(move) && visited[move.CurrentX][move.CurrentY] == false)
                    {
                        visited[move.CurrentX][move.CurrentY] = true;
                        move.PathList.Add(ConvertToBoard(move.CurrentX, move.CurrentY));
                        queue.Enqueue(move);
                    }
                }
            }
            return null;
        }

        private bool ValidBoard(KnightPath move) {
            return move.CurrentX >= 0 && move.CurrentX < 8 && move.CurrentY >= 0 && move.CurrentY < 8;
        }

        private string ConvertToBoard(int x, int y)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)(x + 'A'));
            sb.Append(y+1);
            return sb.ToString();
        }
    }
}
