using System.Collections.Generic;
using System.Linq;

namespace Kata
{
    public class GameOfLife
    {
        private const byte DiedCell = 0;
        private const byte ALiveCell = 1;

        public static byte[,] Execute(byte[,] cells)
        {
            var rowCount = cells.GetLength(0);
            var columnCount = cells.GetLength(1);
            var nextGenerationCells = new byte[rowCount, columnCount];
            for (var i = 0; i < rowCount; i++)
                for (var j = 0; j < columnCount; j++)
                {
                    var neighbors = GetNeighbors(cells, i, j);
                    nextGenerationCells[i, j] = DefineCellState(cells[i, j], neighbors);
                }

            return nextGenerationCells;
        }

        private static byte CheckStayAliveAndOverPopulation(byte cell, IEnumerable<byte> neighbors)
        {
            var aliveNeighbors = neighbors.Count(IsAlive);
            return IsAlive(cell) && aliveNeighbors == 2 || aliveNeighbors == 3 ? ALiveCell : DiedCell;
        }

        private static byte CheckUnderPopulation(byte cell, IEnumerable<byte> neighbors)
        {
            var neighborList = neighbors.ToList();
            return IsAlive(cell) && (neighborList.Count(IsAlive) == 1 || neighborList.Count(IsAlive) == 0) ? DiedCell : cell;
        }


        private static byte CheckReanimation(byte cell, IEnumerable<byte> neighbors)
        {
            return !IsAlive(cell) && neighbors.Count(IsAlive) == 3 ? ALiveCell : cell;
        }

        private static bool IsAlive(byte cell)
        {
            return cell == 1;
        }

        private static byte ChangeCellState(byte cell)
        {
            return cell == ALiveCell ? DiedCell : ALiveCell;
        }

        private static byte DefineCellState(byte cell, IEnumerable<byte> neighbors)
        {
            var neighborList = neighbors.ToList();
            if (CheckStayAliveAndOverPopulation(cell, neighborList) != cell)
            {
                return ChangeCellState(cell);
            }

            if (CheckReanimation(cell, neighborList) != cell)
            {
                return ChangeCellState(cell);
            }

            if (CheckUnderPopulation(cell, neighborList) != cell)
            {
                return ChangeCellState(cell);
            }

            return cell;
        }

        private static IEnumerable<byte> GetNeighbors(byte[,] cells, int row, int column)
        {
            var result = new List<byte>();
            var rowMinimum = row - 1 < 0 ? row : row - 1;
            var rowMaximum = row + 1 >= cells.GetLength(0) ? row : row + 1;
            var columnMinimum = column - 1 < 0 ? column : column - 1;
            var columnMaximum = column + 1 >= cells.GetLength(1) ? column : column + 1;

            for (var i = rowMinimum; i <= rowMaximum; i++)
                for (var j = columnMinimum; j <= columnMaximum; j++)
                    if (i != row || j != column)
                        result.Add(cells[i, j]);

            return result;
        }
    }
}
