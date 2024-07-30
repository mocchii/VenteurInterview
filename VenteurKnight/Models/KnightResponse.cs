using VenteurKnight.Interfaces;

namespace VenteurKnight.Models
{
    public class KnightResponse
    {
        public string starting { get; set; }
        public string ending { get; set; }
        public string shortestPath { get; set; }
        public int numberOfMoves { get; set; }
        public string operationId { get; set; }

    }
}
