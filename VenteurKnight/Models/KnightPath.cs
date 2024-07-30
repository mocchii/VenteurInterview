using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace VenteurKnight.Models;

public class KnightPath : Knight, ICloneable
{
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public List<string> PathList { get; set; }

    public KnightPath(Knight knight) {
        this.Starting = knight.Starting;
        this.Ending = knight.Ending;
        this.OperationId = knight.OperationId;
        this.CurrentX = (knight.Starting[0] - 'A');
        this.CurrentY = knight.Starting[1] - '0' - 1;
        this.PathList = new List<string>() { knight.Starting };
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
