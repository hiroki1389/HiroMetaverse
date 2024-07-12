using UnityEngine;

// 同じと判断された物体を時間別で保持する連結リスト
public class TimeDivisionCell
{
    public TimeDivisionCell next;
    private float scale_x;
    public AddressData data;

    // コンストラクタ
    public TimeDivisionCell(float scale_x, AddressData objectData)
    {
        this.next = null;
        this.scale_x = scale_x;
        this.data = objectData;
    }
    public TimeDivisionCell()
    {
        this.next = null;
        this.scale_x = 0.0f;
        this.data = null;
    }
    public float getScaleX()
    {
        return this.scale_x;
    }
}
