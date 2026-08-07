using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NavigationTile", menuName = "Tile/Navigation Tile")]
public class NavigationTile : Tile
{
    public bool walkable;
}
