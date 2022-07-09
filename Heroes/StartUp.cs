using Heroes.Core;
using Heroes.Core.Contracts;

namespace CarRacing
{
    public class StartUp
    {
        public static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
