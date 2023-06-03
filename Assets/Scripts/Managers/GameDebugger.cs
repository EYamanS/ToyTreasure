using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OyuncakHazinesiNerede.Debugger
{
    public class GameDebugger : MonoBehaviour
    {
        [SerializeField] bool DoLogPageDiscover;

        private void Awake()
        {
            GameEvents.PageDiscovered += OnPageDiscover;
        }

        private void OnPageDiscover(Page page)
        {
            Debug.Log("New page discovered: [" + page.name + "]");
        }
    }
}
