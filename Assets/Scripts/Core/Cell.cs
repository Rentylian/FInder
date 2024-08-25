using System;

namespace Core
{
    public class Cell 
    {
        public CellConfig CellConfig { get; }
    
        public Action<Cell> CellTrigger;
        public Action HandlerCorrectWayCell;
        public Action HandlerWrongWayCell;
        public Action HandlerAvailableWayCell;
        public Action HandlerDefaultWayCell;

        private bool _isCellSelected;

        public Cell(CellConfig cellConfig)
        {
            CellConfig = cellConfig;
        }
    }
}

