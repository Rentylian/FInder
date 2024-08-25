using Core;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private Material _defaultCell;
    [SerializeField] private Material _availableCell;
    [SerializeField] private Material _correctCell;
    [SerializeField] private Renderer _cellRenderer;
    
    private Cell _cell;
    
    public void TriggerCell()
    {
        _cell.CellTrigger?.Invoke(_cell);
    }

    public void ResetCellView()
    {
        gameObject.SetActive(true);
    }
    
    public void Initialize(Cell cell)
    {
        _cell = cell;
        _cell.HandlerCorrectWayCell += HandleCorrectWayCell;
        _cell.HandlerWrongWayCell += HandleWrongWayCell;
        _cell.HandlerAvailableWayCell += HandleAvailableWayCell;
        _cell.HandlerDefaultWayCell += SetDefaultWayMaterial;
    }

    private void SetMaterial(Material material)
    {
        _cellRenderer.material = material;
    }

    private void HandleCorrectWayCell()
    {
        SetMaterial(_correctCell);
    }
    
    private void HandleAvailableWayCell()
    {
        SetMaterial(_availableCell);
    }
    
    private void HandleWrongWayCell()
    {
        gameObject.SetActive(false);
    }
    
    private void SetDefaultWayMaterial()
    {
        SetMaterial(_defaultCell);
    }
}
