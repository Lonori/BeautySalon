namespace BeautySalon.Components
{
    internal static class MathExt
    {
        public static int Clamp(int value, int min, int max)
        {
            if (min >= value)
            {
                return min;
            }
            if (max <= value)
            {
                return max;
            }
            return value;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (min >= value)
            {
                return min;
            }
            if (max <= value)
            {
                return max;
            }
            return value;
        }

        public static double Clamp(double value, double min, double max)
        {
            if (min >= value)
            {
                return min;
            }
            if (max <= value)
            {
                return max;
            }
            return value;
        }

        public static int Floor(float d)
        {
            return (int)d;
        }

        public static int Floor(double d)
        {
            return (int)d;
        }

        public static int Round(float d)
        {
            return Round((double)d);
        }

        public static int Round(double d)
        {
            if (d < 0)
            {
                return (int)(d - 0.5);
            }
            return (int)(d + 0.5);
        }
    }
}
