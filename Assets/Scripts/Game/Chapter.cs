using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

namespace OyuncakHazinesiNerede.Game
{
    public class Chapter : MonoBehaviour
    {
        [SerializeField] Page[] _pages;
         public int _currentPage;

        private void Awake()
        {
            _pages = GetComponentsInChildren<Page>(true);
            Debug.Log("Chapter: " + this.name + " - Pages: " + _pages.Length);
            ReloadPages();
        }

        private void ReloadPages()
        {
            for (int i = 0; i < _pages.Length; i++)
            {
                if (i != _currentPage)
                    _pages[i].gameObject.SetActive(false);
                else
                    _pages[i].gameObject.SetActive(true);
            }
        }

        // DO NOT CALL THIS
        public void loadChapter()
        {
            Debug.Log("Loading Chapter: " + name);
            gameObject.SetActive(true);
            _pages[0].LoadPage();
        }

        public void unLoadChapter()
        { 
            gameObject.SetActive(false);
            foreach (Page page in _pages)
            { 
                page.unLoadPage();
            }
        }

        public Page GetCurrentPage()
        {
            return _pages[_currentPage];
        }

        public bool AdvancePage()
        {
            if (_currentPage + 1 < _pages.Length)
            {
                _pages[_currentPage].unLoadPage();
                _currentPage += 1;
                ReloadPages();
                _pages[_currentPage].LoadPage();
                return true;
            }
            else return false;
        }

        public bool PreviousPage()
        {
            if (_currentPage != 0)
            {
                _pages[_currentPage].unLoadPage();
                _currentPage--;
                ReloadPages();
                _pages[_currentPage].LoadPage();
                return true;
            }
            else
            { 
                return false;
            }
        }
    }
}
