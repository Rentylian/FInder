using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class CellCreator
    {
        public int RowUpperBound => _rowUpperBound;
        public int ColumnUpperBound => _columnUpperBound;
        
        private Cell[,] _cellTable;
        private CellConfig[,] _cellsConfig;
        private List<CellConfig> _availableCells = new();
        private int _columnCount;
        private int _rowCount;
        private int _rowUpperBound;
        private int _columnUpperBound;
        
        private readonly bool _isSquareField;
        private readonly int _startValueRow;
        private readonly int _maxValueRow;
        private readonly int _startValueColumn;
        private readonly int _maxValueColumn;
        private bool _isConfigCreated;
        
        public CellCreator(bool isSquare, int startValueRow, int maxValueRow, int startValueColumn, int maxValueColumn)
        {
            _isSquareField = isSquare;
            _startValueRow = startValueRow;
            _maxValueRow = maxValueRow;
            _startValueColumn = startValueColumn;
            _maxValueColumn = maxValueColumn;
        }
        
        public Cell[,] GetCells()
        {
            return _cellTable;
        }
        
        public CellConfig[,] GetCellsConfig()
        {
            return _cellsConfig;
        }

        public List<CellConfig> GetAvailableCellConfig()
        {
            return _availableCells;
        }
        
        public void CreateCellsConfig()
        {
            if (_isConfigCreated)
                return;
            // max is exclusive, so plus 1
            _rowCount = Random.Range(_startValueRow, _maxValueRow + 1); 
            _columnCount = Random.Range(_startValueColumn, _maxValueColumn + 1);
            if (_isSquareField)
            {
                _rowCount = _columnCount;    
            }
            _cellsConfig = new CellConfig[_rowCount, _columnCount];
            var currentCellNumber = 0;
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    currentCellNumber++;
                    _cellsConfig[i, j] = new CellConfig();
                    CellConfig cell = _cellsConfig[i, j];
                    cell.NumberCell =  currentCellNumber;
                    _availableCells.Add(cell);
                    cell.CellCoordinateInArray = new Vector2Int(i, j);
                }
            }
        
            _columnUpperBound = _cellsConfig.GetUpperBound(1);
            _rowUpperBound = _cellsConfig.GetUpperBound(0);
            FindCloseCells();
            CreateCells();
        }
        
        private void FindCloseCells() 
        {
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    CellConfig currentCell = _cellsConfig[i, j];
                    FindNearbyCellByIndex(i, j, currentCell);
                }
            }
        }
        
        private void FindNearbyCellByIndex(int rowNumber, int columnNumber, CellConfig cellConfig)
        {
            if (rowNumber < _rowUpperBound)
            {
                // cell below
                AddNearbyCell(cellConfig, rowNumber + 1, columnNumber);
                // first cell has only one available cell
                if (rowNumber == 0)
                {
                    return;
                }
            }
            
            if (rowNumber > 0)
            {
                // cell above
                AddNearbyCell(cellConfig, rowNumber - 1, columnNumber);
            }
        
            if (columnNumber < _columnUpperBound)
            {
                // cell on the right
                AddNearbyCell(cellConfig, rowNumber, columnNumber + 1);
            }
        
            if (columnNumber > 0)
            {
                // cell on the left
                AddNearbyCell(cellConfig, rowNumber, columnNumber - 1);
            }
        }
        
        private void AddNearbyCell(CellConfig cellConfig, int rowIndex, int columnIndex)
        {
            cellConfig.NearbyCellsConfig.Add(_cellsConfig[rowIndex, columnIndex]);
            Debug.Log($"{cellConfig.NumberCell} has nearby {rowIndex};{columnIndex}");
        }
        
        private void CreateCells()
        {
            _cellTable = new Cell[_rowCount, _columnCount];
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    Cell cell = new Cell(_cellsConfig[i, j]);
                    _cellTable[i, j] = cell;
                }
            }
        }
    }
}