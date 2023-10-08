namespace NPC.Vehicle
{
    public class WheelSteerable : Wheel, ISteerable
    {
        private float turnAngle;
        public float maxAngle = 30f;
        public float offset = 0f;

        public void Steer(float steerInput)
        {
            turnAngle = steerInput * maxAngle + offset;
            wcol.steerAngle = turnAngle;
        }
    }
}