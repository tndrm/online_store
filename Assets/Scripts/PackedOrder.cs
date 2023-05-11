using UnityEngine;

public class PackedOrder : MonoBehaviour
{
    private int totalCost;
    public int getCost => totalCost;
    public void SetCost(int cost)
    {
        totalCost = cost;
    }
}
