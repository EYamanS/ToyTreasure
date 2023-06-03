using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OyuncakHazinesiNerede.Game;
using System;
using Unity.VisualScripting;

namespace OyuncakHazinesiNerede.Managers
{
    public class GameManager : MonoBehaviour
    {


        #region -Chapter Settings-
        public int currentChapter = 0;
        [SerializeField] private Chapter[] _chapters;
        [SerializeField] int StartChapter = 0;
        #endregion


        #region -References-
        UIManager _uiManager;
        GoalTracker _goalTracker;
        #endregion


        #region -Player Settings-
        public string playerName;
        #endregion


        private void Awake()
        {
            GameEvents.PageDiscovered += OnPageDiscover;
            GameEvents.GoalUpdate += RefreshGoals;
            GameEvents.RestartLevel += RestartLevel;

            GameFunctions.GetCurrentPage += GetCurrentTotalPage;

            _uiManager = FindObjectOfType<UIManager>();
            _goalTracker = FindAnyObjectByType<GoalTracker>();
        }
        private void OnPageDiscover(Page page)
        {
            RefreshGoals();
            foreach (GameObject gj in page.DiscoverReveals)
            { 
                gj.SetActive(true);
            }
        }

        private void RefreshGoals()
        {
            _goalTracker.UpdateCurrentGoals(_chapters[currentChapter].GetCurrentPage());
            _uiManager.AllowNext(_goalTracker.IsPageComplete());
        }

        private int GetCurrentTotalPage() { return (_chapters[currentChapter]._currentPage + 1) * (currentChapter + 1); }

        private void Start()
        {
            if (_uiManager == null) { throw new System.Exception("UIManager not present in scene. Please add it."); }
            if (_goalTracker == null) { throw new System.Exception("GoalTracker not present in scene. Please add it."); }

            // for starting at specific chapter. Like a cheat code
            _chapters[StartChapter].gameObject.SetActive(true);
            _chapters[StartChapter].loadChapter();
        }


        public void AdvanceChapterPage()
        {
            playerName = PlayerPrefs.GetString("Playername");

            if (_chapters[currentChapter].AdvancePage())
            {

            }
            else
            {
                _chapters[currentChapter].unLoadChapter();
                currentChapter++;
                _chapters[currentChapter].loadChapter();
            }
        }

        public void PreviousChapterPage()
        {
            playerName = PlayerPrefs.GetString("Playername");

            if (_chapters[currentChapter].PreviousPage())
            {

            }
            else
            {
                _chapters[currentChapter].unLoadChapter();
                currentChapter--;
                _chapters[currentChapter].loadChapter();
            }
        }

        public void RestartLevel()
        { 
            PreviousChapterPage();
            AdvanceChapterPage();
        }


    }
}
