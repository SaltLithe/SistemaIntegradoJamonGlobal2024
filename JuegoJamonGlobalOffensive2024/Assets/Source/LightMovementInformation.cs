using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LightMovementInformation : SceneElementInformation
{
    public int Direction { get; private set; }

    public LightMovementInformation(int direction) 
    {
        Direction = direction;
    }
}