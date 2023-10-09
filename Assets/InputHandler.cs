using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectDefend.Units.Player;

namespace ProjectDefend.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;
        private List<Transform> selectedUnits = new List<Transform>();

        private bool isDragging = false;

        private Vector3 mousePosition;


        private void Start()
        {
            instance = this;
        }
        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePosition, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 1f, 0f, 0.10f));
                MultiSelect.DrawScreenRectBorder(rect, 1.5f, new Color(0f, 0f, 1f, 0.20f));
            }
        }
        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8:
                            Debug.Log("Player unit hit");
                            SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                            break;
                        default:
                            isDragging = true;
                            DeselectUnits();
                            break;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (Player.PlayerManager.instance.playerUnits.childCount == 0)
                {
                    isDragging = false;
                    return;
                }  

                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (isWithinSelectionBounds(unit))
                        {
                            SelectUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: // player unit
                            break;
                        case 9: // enemy unit
                            break;
                        default: // move
                            foreach (Transform unit in selectedUnits)
                            {
                                unit.GetComponent<PlayerUnit>().MoveUnit(hit.point);
                            }
                            break;
                    }
                }
            }
        }

        public void SelectUnit(Transform unit, bool canMultiSelect = false)
        {
            if (!canMultiSelect)
            {
                DeselectUnits();
            }
            selectedUnits.Add(unit);
            unit.Find("Highlight").gameObject.SetActive(true);
        }

        public void DeselectUnits()
        {
            foreach (Transform unit in selectedUnits)
            {
                unit.Find("Highlight").gameObject.SetActive(false);
            }
            selectedUnits.Clear();
        }

        private bool isWithinSelectionBounds(Transform transform)
        {
            if (!isDragging)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds viewPort = MultiSelect.GetViewportBounds(cam,mousePosition,Input.mousePosition);
            return viewPort.Contains(cam.WorldToViewportPoint(transform.position));
        }

        private bool HaveSelevtedUnits() => selectedUnits.Count > 0 ? true : false;
    }
}

