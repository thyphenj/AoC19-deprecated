namespace _10_MonitoringStation
{
    class Program
    {
        public static Grid grid;

        public static void Main()
        {
            Data data = new Data(3);

            grid = new Grid(data);

            grid.Scanner();

            grid.Results(1);

            grid.Laser();

            grid.Results(2);
        }
    }
}
