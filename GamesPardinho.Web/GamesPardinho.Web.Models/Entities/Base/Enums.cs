using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Entities.Base
{
    [Flags]
    public enum Elo
    {
        None = 0,
        Iron = 16, Bronze = 32, Silver = 64, Gold = 128, Platinum = 256, Diamond = 512, Master = 1024, GrandMaster = 2048, Challenger = 4096,
        I = 8, II = 4, III = 2, IV = 1
    }

    [Flags]
    public enum Role { None = 0, Mid = 1, Top = 2, Jungle = 4, Adc = 8, Support = 16, Fill = 32 }

}
