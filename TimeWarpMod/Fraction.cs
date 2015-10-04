using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeWarpMod
{
    [Serializable]
    public struct Fraction
    {
        public uint num;
        public uint den;

        
        public override string ToString()
        {
            if (num == 0)
                return i18n.current["speed_paused"];

            if (num == 1 && den == 1)
                return i18n.current["speed_normal"];

            if (num > den)
                return num + "x";
            
            return num + "/" + den;
        }
    }
}
