using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class WayBuilder
    {
        private CellConfig[,] _cellsConfig;
        private List<CellConfig> _availableCells = new();
        private bool _isWayBuilded;
        private int _rowUpperBound;
        private int _columnUpperBound;
        private readonly List<CellConfig> _correctWay = new();
        private readonly CellCreator _cellCreator;

        public WayBuilder(CellCreator cellCreator)
        {
            _cellCreator = cellCreator;
        }
        
        public List<CellConfig> GetCorrectWay()
        {
            CellConfig[] correctWay = new CellConfig[_correctWay.Count];
            _correctWay.CopyTo(correctWay);
            return correctWay.ToList();
        }

        public void BuildWay()
        {
            _cellsConfig = _cellCreator.GetCellsConfig();
            _availableCells = _cellCreator.GetAvailableCellConfig();
            CellConfig previousCell;
            GetBoundOfTable();
            var startPosition =  Random.Range(0, _columnUpperBound);
            Vector2Int currentCellCoord = new Vector2Int(0, startPosition);
            CellConfig currentCell = _cellsConfig[currentCellCoord.x, currentCellCoord.y];
            _correctWay.Add(currentCell);
            
            while (!_isWayBuilded)
            {
                previousCell = currentCell;
                // way shouldn`t return back
                RemoveLesserCellNumber(currentCell);
                currentCell = GetNewCell(currentCell);
                _availableCells = _availableCells.Except(previousCell.NearbyCellsConfig).ToList();
                if (_availableCells.Contains(previousCell))
                {
                    _availableCells.Remove(previousCell);
                }
                _correctWay.Add(currentCell);
                if (currentCell.CellCoordinateInArray.x == _rowUpperBound)
                {
                    _isWayBuilded = true;
                }
            }
        }
        
        private void GetBoundOfTable()
        {
            _columnUpperBound = _cellCreator.ColumnUpperBound;
            _rowUpperBound = _cellCreator.RowUpperBound;
        }
        
        private void RemoveLesserCellNumber(CellConfig cellConfig)
        {
            if (cellConfig.NearbyCellsConfig.Count <= 1)
            {
                return;
            }
            int minCell = cellConfig.NearbyCellsConfig.Min(x => x.NumberCell);
            CellConfig cellForRemove = cellConfig.NearbyCellsConfig.Find(x => x.NumberCell == minCell);
            cellConfig.NearbyCellsConfig.Remove(cellForRemove);
        }
        
        private CellConfig GetNewCell(CellConfig cell)
        {
            CellConfig newCell = new CellConfig();
            List<CellConfig> availableNearbyCells = GetIntersectCells(cell);
        
            if (availableNearbyCells.Count > 0)
            {
                int nearbyCellsCount = availableNearbyCells.Count; 
                int random = Random.Range(0, nearbyCellsCount); 
                newCell = availableNearbyCells[random];
            }

            return newCell;
        }

        private List<CellConfig> GetIntersectCells(CellConfig currentCell)
        {
            List<CellConfig> availableNearbyCell = new List<CellConfig>();
            
            foreach (var c in _availableCells)
            {
                foreach (var v in currentCell.NearbyCellsConfig)
                {
                    if (v.NumberCell == c.NumberCell)
                    {
                        availableNearbyCell.Add(v);
                    }
                }
            }
            return availableNearbyCell;
        }
    }
}
