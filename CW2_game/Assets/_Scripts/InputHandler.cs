using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VS.CW2RTS.Units.Player;


namespace VS.CW2RTS.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;

        private RaycastHit hit; // what we hit with our ray

        private List<Transform> selectedUnits = new List<Transform>();

        private bool isDragging = false;

        private Vector3 mousePos;

        void Start()
        {
            instance = this;
        }

        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePos, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.blue);
            }
        }

        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: // units layer
                            //do something
                            SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                            break;
                        default:
                            //if none of the above happens, do something
                            isDragging = true;
                            DeselectUnits();
                            break;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach(Transform unit in child)
                    {
                        if (isWithinSelectionBounds(unit))
                        {
                            SelectUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }

            if (Input.GetMouseButtonDown(1) && HaveSelectedUnits())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: // units layer
                            //do something
                            break;

                        case 9:
                            //enemy units attack or set target
                            break;
                        default:
                            //if none of the above happens, do something
                            foreach(Transform unit in selectedUnits)
                            {
                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.MoveUnit(hit.point);
                            }
                            break;
                    }
                }
            }
        }

        private void SelectUnit(Transform unit, bool canMultiselect = false)
        {
            if (!canMultiselect)
            {
                DeselectUnits();
            }
            selectedUnits.Add(unit);
            // set an obj on the unit called highlight
            unit.Find("Highlight").gameObject.SetActive(true);
        }

        private void DeselectUnits()
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].Find("Highlight").gameObject.SetActive(false);
            }
            selectedUnits.Clear();
        }

        private bool isWithinSelectionBounds(Transform tf)
        {
            if (!isDragging)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds vpBounds = MultiSelect.GetVPBounds(cam, mousePos, Input.mousePosition);
            return vpBounds.Contains(cam.WorldToViewportPoint(tf.position));
        }

        private bool HaveSelectedUnits()
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


