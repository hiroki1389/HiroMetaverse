using UnityEngine;

// AddressDataクラス，PositionDivisionCellクラス，TimeDivisionCellクラスの3つをまとめてメソッドを提供するクラス
// これを使って実装する
public class Address
{
    public readonly PositionDivisionCell header;
    
    public Address()
    {
        header = new PositionDivisionCell();
    }

    public void InsertObjectData(GameObject gameObject)
    {
        AddressData addressData = new AddressData(gameObject, System.DateTime.Now);
        Vector3 position = gameObject.transform.position;
        float scale_x = gameObject.transform.localScale.x;

        PositionDivisionCell posCell = header;
        while (posCell.other != null)
        {
            posCell = posCell.other;
            float THRESHOLD = 0.3f; // ここの同じ物体とみなす閾値は要検討
            float distance = Vector3.Distance(position, posCell.getPosition());
            if (distance <= THRESHOLD)
            {
                TimeDivisionCell timeCell = posCell.posHeader;
                while (timeCell.next != null)
                {
                    if (scale_x > timeCell.next.getScaleX())
                    {
                        TimeDivisionCell temp3  = timeCell.next;
                        timeCell.next = new TimeDivisionCell(scale_x, addressData);
                        timeCell.next.next = temp3;
                        SetActiveMin(posCell.posHeader);
                        return;
                    }
                    timeCell = timeCell.next;
                }
                timeCell.next = new TimeDivisionCell(scale_x, addressData);
                SetActiveMin(posCell.posHeader);
                return;
            }

        }
        posCell.other = new PositionDivisionCell(position);
        posCell.other.posHeader.next = new TimeDivisionCell(scale_x, addressData);

        SetActiveMin(posCell.other.posHeader);
    }

    public void SetActiveMin(TimeDivisionCell posHeader)
    {
        posHeader.next.data.getGameObject().SetActive(true);
        
        TimeDivisionCell temp = posHeader.next;
        while (temp.next != null)
        {
            temp = temp.next;
            temp.data.getGameObject().SetActive(false);
        }
    }
}