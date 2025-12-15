using System;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutSpawner : MonoBehaviour
{
    public Action OnCompletingPuzzle;

    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> tiles;
    private int size;

    private void Start()
    {
        tiles = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);
        OnCompletingPuzzle += GameManager.Instance.PuzzleCompleted;
    }

    public void OnTileClicked(Transform _clickedTile)
    {
        int currentIndex = tiles.IndexOf(_clickedTile);
        int row = currentIndex / size;
        int col = currentIndex % size;

        ToggleAtIndex(currentIndex);

        ToggleIfValid(row - 1, col);
        ToggleIfValid(row + 1, col);
        ToggleIfValid(row, col - 1);
        ToggleIfValid(row, col + 1);
    }

    private void ToggleIfValid(int _row, int _col)
    {
        int index = (_row * size) + _col;

        if (_row < 0 || _row >= size || _col < 0 || _col >= size)
        {
            return;
        }

        ToggleAtIndex(index);
        CheckCompletion();
    }

    private void ToggleAtIndex(int _currentTile)
    {
        tiles[_currentTile].GetComponent<LightsOutTile>().Toggle();
    }

    private void CheckCompletion()
    {
        bool isPuzzleComplete = true;
        foreach (Transform _tile in tiles)
        {
            LightsOutTile tiles = _tile.GetComponent<LightsOutTile>();
            if (tiles.IsLightOn())
            {
                isPuzzleComplete = false;
                break;
            }
        }

        if (isPuzzleComplete)
        {
            OnCompletingPuzzle?.Invoke();
        }
    }

    private void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                tiles.Add(piece);
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, +1 - (2 * width * row) - width, 0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                float gap = gapThickness / 2;
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                Vector2[] uv = new Vector2[4];
                uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                mesh.uv = uv;
            }
        }
    }
}
