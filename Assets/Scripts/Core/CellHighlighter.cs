namespace Core
{
    public class CellHighlighter 
    {
        public void SwitchCellHighlight(Cell targetCell, bool enable)
        {
            if (enable)
            {
                targetCell.HandlerAvailableWayCell.Invoke(); 
            }
            else
            {
                targetCell.HandlerDefaultWayCell.Invoke();
            }
        }
    }
}
