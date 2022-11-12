using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class UIDropableArray : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {

        public bool isEnabled;

        [System.Serializable]
        public class CDropArray
        {
            public string AcceptDragID;
            public UnityEvent DropEventValid;
            public UnityEvent DropEventInvalid;
        }

        [Header("Drag Settings")]
        public List<CDropArray> TargetDragObject;

        [Header("Drop Settings")]
        public bool autoHideObject;

        string lastID;
        int lastIndex;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool isValid(string dragID)
        {
            bool result = false;
            for (int i = 0; i < TargetDragObject.Count; i++)
            {
                if (TargetDragObject[i].AcceptDragID == dragID)
                {
                    lastIndex = i;
                    lastID = dragID;
                    result = true;
                }
            }
            return result;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            UIDraggable d = eventData.pointerDrag.GetComponent<UIDraggable>();
            if (d != null)
            {
                if (isValid(d.dragID))
                {
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
            if (eventData.pointerDrag == null)
            {
                return;
            }

            UIDraggable d = eventData.pointerDrag.GetComponent<UIDraggable>();
            if (d != null)
            {
                if (isValid(d.dragID))
                {
                }
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

            UIDraggable d = eventData.pointerDrag.GetComponent<UIDraggable>();
            if (d != null)
            {
                if (isValid(d.dragID))
                {
                    if (autoHideObject)
                    {
                        d.transform.position = this.transform.position;
                        d.gameObject.SetActive(false);
                    }
                    TargetDragObject[lastIndex].DropEventValid.Invoke();
                }
                else
                {
                    TargetDragObject[lastIndex].DropEventInvalid.Invoke();
                }
            }
        }

        public void DraggedObjectActive(bool active)
        {
            GameObject temp = GameObject.Find(lastID);
            if (temp) temp.SetActive(active);
        }
    }
}
