namespace _10_MonitoringStation
{
    class Program
    {
        public static void Main()
        {
            Data data = new Data(1);

            Grid grid = new Grid(data);

            grid.Scanner();

            grid.Results(1);

        }
    }
}
