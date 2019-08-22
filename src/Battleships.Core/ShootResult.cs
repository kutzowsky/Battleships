namespace Battleships.Core
{
    public class ShootResult
    {
        public FieldState State { get; set; }

        public string HitShipName { get; set; }

        public bool HitShipDestroyed { get; set; }
    }
}
