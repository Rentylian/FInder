using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    public Camera cameraMain;
    private bool _isClick;
    
    private void Update()
    {
        if (!_isClick && Input.GetMouseButton(0))
        {
            _isClick = true;
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out CellView cellView))
                {
                    cellView.TriggerCell();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isClick = false;
        }
    }
}
