using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsPuzzleOver;
    public bool IsKeyPickedUp;
    public GameObject KeyToDoor;

    [SerializeField] private Transform keySpawnLocation;

    [Header("Game Setup for Puzzles")]
    [SerializeField] private Transform puzzleLocation;
    [SerializeField] private GameObject puzzle01;
    [SerializeField] private GameObject puzzle02;

    [Header("Rigging Puzzle")]
    [SerializeField] private bool isPuzzleRigged;
    [SerializeField] private int riggedPuzzle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int randomPuzzle = UnityEngine.Random.Range( 1, 2 );

        if (isPuzzleRigged)
        {
            randomPuzzle = riggedPuzzle;
        }

        if (randomPuzzle == 1)
        {
            puzzle01 = Instantiate(puzzle01, puzzleLocation, puzzleLocation);
        }
        else
        {
            puzzle02 = Instantiate(puzzle02, puzzleLocation);
        }
    }

    public void PuzzleCompleted()
    {
        KeyToDoor = Instantiate(KeyToDoor, keySpawnLocation, keySpawnLocation);
        IsPuzzleOver = true;
        PlayerInteractivity.IsInteracting = false;
        TimerManager.Instance.isTimerStart = false;
        Debug.Log("You completed the puzzle!!");
    }
}
