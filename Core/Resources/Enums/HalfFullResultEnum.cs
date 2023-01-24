using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resources.Enums
{
    public enum HalfFullResultEnum : int
    {
        Home_Home = 11,
        Home_Draw = 19,
        Home_Away = 12,
        Draw_Draw = 99,
        Draw_Home = 91,
        Draw_Away = 92,
        Away_Home = 21,
        Away_Draw = 29,
        Away_Away = 22
    }
}
