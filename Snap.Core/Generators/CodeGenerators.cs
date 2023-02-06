namespace Snap.Core.Generators
{
    public static class CodeGenerators
    {
        public static string GetActiveCode()
        {
            Random random = new Random();
            return random.Next(100000, 900000).ToString();
        }

        public static Guid GetId()
        {
            return Guid.NewGuid();
        }

        public static string GetFileName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string GetOrderCode()
        {
            Random random = new Random();
            return random.Next(1000000, 9999999).ToString();
        }
    }
}
