namespace MGFramework
{
    public class MGFadeTo : MGFadeOut
    {
        public static MGFadeTo ActionWithDuration(float duartion, float opacity)
        {
            return new MGFadeTo
                       {
                           TarOpacity = opacity,
                           _duration = duartion
                       };
        }
    }
}