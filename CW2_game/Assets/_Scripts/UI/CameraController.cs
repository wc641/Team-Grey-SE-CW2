using UnityEngine;


namespace VS.CW2RTS.UI
{
    public class CameraController : MonoBehaviour
    {
        public Transform cameraTransform;
        public float movementSpeed;
        public float movementTime;
        public Vector3 zoomAmount;


        public Vector3 newPosition;
        public Vector3 newZoom;
        private float minZoomY = 10f;
        private float maxZoomY = 38f;
        private float minZoomZ = -8f;
        private float maxZoomZ = 20f;

        public Vector3 dragStartPosition;
        public Vector3 dragCurrentPosition;


        // Start is called before the first frame update
        void Start()
        {
            newPosition = transform.position;
            newZoom = cameraTransform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if(!PauseMenu.isPaused)
            {
                HandleMouseInput();
                HandleMovementInput();
            }
        }

        void ClampZoom()
        {
            newZoom.y = Mathf.Clamp(newZoom.y, minZoomY, maxZoomY);
            newZoom.z = Mathf.Clamp(newZoom.z, minZoomZ, maxZoomZ);
        }
        void HandleMouseInput()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
                ClampZoom();
            }

            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragStartPosition = ray.GetPoint(entry);
                }
            }
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPosition = ray.GetPoint(entry);

                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }
        }

        void HandleMovementInput()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += (transform.forward * movementSpeed);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition += (transform.forward * -movementSpeed);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += (transform.right * movementSpeed);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition += (transform.right * -movementSpeed);
            }

            if (Input.GetKey(KeyCode.R))
            {
                newZoom += zoomAmount;
                ClampZoom();
            }
            if (Input.GetKey(KeyCode.F))
            {
                newZoom -= zoomAmount;
                ClampZoom();
            }

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime + movementSpeed);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime + movementTime);
        }
    }

}
