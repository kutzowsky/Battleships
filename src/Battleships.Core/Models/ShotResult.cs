using Battleships.Core.Enums;

namespace Battleships.Core.Models
{
    public class ShotResult
    { 

        public bool IsHit { get; set; }

        public string HitShipName { get; set; }

        public bool HitShipDestroyed { get; set; }
    }
}
