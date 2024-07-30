using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace VenteurKnight.Models;

public partial class Knight
{
    public int Id { get; set; }

    public string OperationId { get; set; }

    public string Starting { get; set; }

    public string Ending { get; set; }

    public string ShortestPath { get; set; }

    public int NumberOfMoves { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }
}
