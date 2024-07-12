using UnityEngine;

// 現実の物体を座標によって，同じ物体かどうかを分ける二次元連結リスト（厳密には二次元目は別のクラスを使用）
// 異なる物体はotherですべてつながっている
// 同じと判断された物体はposheaderの時間別の連結リストにつながる
public class PositionDivisionCell
{
    public PositionDivisionCell other;
    public readonly TimeDivisionCell posHeader;
    private Vector3 position;

    // コンストラクタ
    public PositionDivisionCell(Vector3 position)
    {
        this.other = null;
        this.posHeader = new TimeDivisionCell();
        this.position = position;
    }
    public PositionDivisionCell()
    {
        this.other = null;
        this.posHeader = null;
        this.position = new Vector3();
    }

    public Vector3 getPosition()
    {
        return this.position;
    }
}
