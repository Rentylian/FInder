using UnityEngine;

namespace Core
{
    public class CellViewController
    {
        private CellView[,] _cellsView;

        private readonly CellCreator _cellCreator;
        private readonly CellView _cellViewPrefab;
        private readonly CameraMover _cameraMover;
        private readonly Transform _cellViewContainer;
        private readonly float _distanceBetweenCells;
    
        public CellViewController(CellCreator cellCreator, 
            CellView cellViewPrefab, 
            CameraMover cameraMover,
            Transform cellViewContainer,
            float distanceBetweenCells)
        {
            _cellCreator = cellCreator;
            _cellViewPrefab = cellViewPrefab;
            _cellViewContainer = cellViewContainer;
            _distanceBetweenCells = distanceBetweenCells;
            _cameraMover = cameraMover;
        }
    
        public void CreateCellsView()
        {
            Vector3 startPosition = Vector3.zero;
            Cell[,] cells = _cellCreator.GetCells();
            int rowOfCellsView = cells.GetUpperBound(0) + 1;
            int columnOfCellsView = cells.GetUpperBound(1) + 1;
            _cellsView = new CellView[rowOfCellsView, columnOfCellsView];
        
            for (int i = 0; i < rowOfCellsView; i++)
            {
                for (int j = 0; j < columnOfCellsView; j++)
                {
                    _cellsView[i, j] = GameObject.Instantiate(_cellViewPrefab,
                        startPosition + new Vector3(j * _distanceBetweenCells,0, i * _distanceBetweenCells),
                        Quaternion.identity, _cellViewContainer);
                    _cellsView[i, j].name = cells[i, j].CellConfig.NumberCell.ToString();
                    _cellsView[i, j].Initialize(cells[i, j]);
                }
            }

            Vector3 camPosition = new Vector3((columnOfCellsView * _distanceBetweenCells / 2) - 1, 
                rowOfCellsView * 2, 
                ((rowOfCellsView * _distanceBetweenCells) / 2 ) - 1);
            _cameraMover.SetCameraPosition(camPosition);
        }

        public void EnableAllCellsView()
        {
            foreach (var cellView in _cellsView)
            {
                cellView.ResetCellView();
            }
        }
    }
}
