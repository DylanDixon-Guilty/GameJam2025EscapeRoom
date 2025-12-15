using UnityEngine;

public class LightsOutTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer tileRenderer;

    [Header("Tile Properties")]
    [SerializeField] private Material lightOff; // Will look Dark blue
    [SerializeField] private Material lightOn; // Will look light blue

    private bool isOn = true;
    private LightsOutSpawner lightsOutSpawner;

    private void Start()
    {
        lightsOutSpawner = GetComponentInParent<LightsOutSpawner>();

        UpdateDisplayState();
    }

    /// <summary>
    /// Sets the color(on/off) based on the state of the tile
    /// </summary>
    private void UpdateDisplayState()
    {
        tileRenderer.material = isOn ? lightOn : lightOff;
    }

    /// <summary>
    /// Toggle the state of a tile on or off
    /// </summary>
    public void Toggle()
    {
        isOn = !isOn;
        UpdateDisplayState();
    }

    /// <summary>
    /// Set the state of the tile to a specific value of off or on
    /// </summary>
    public void SetState(bool _isOn)
    {
        isOn = _isOn;
        UpdateDisplayState();
    }

    /// <summary>
    /// To see if the light is on and check if the puzzle is complete
    /// </summary>
    public bool IsLightOn()
    {
        return isOn;
    }

    private void OnMouseDown()
    {
        if (PlayerInteractivity.IsInteracting)
        {
            lightsOutSpawner.OnTileClicked(this.transform);
        }
    }
}
