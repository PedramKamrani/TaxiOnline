namespace Snap.Core.Generators
{
    public static class ColorGenerators
    {
        public static string SelectColorCode()
        {
            string[] colors = {
                "#FF1493", "#6A5ACD", "#FA8072", "#DC143C", "#FF8C00", "#3CB371", "#20B2AA", "#6495ED", "#BC8F8F", "#A52A2A",
                "#FDD493", "#435ACD", "#A50072", "#0C143C", "#F08C00", "#1CB3F1", "#44B2AA", "#A495ED", "#898F8F", "#A00A2A"  }; ;
            Random random = new Random();
            var index = random.Next(colors.Length);
            return colors[index];
        }
    }
}
