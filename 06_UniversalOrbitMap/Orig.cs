namespace _06_UniversalOrbitMap
{
    class Orig
    {
        public string orig;
        public string root;
        public string moon;
        public string name;
        public int distance;

        public Orig (string data)
        {
            orig = data;
            root = data.Split(')')[0];
            moon = data.Split(')')[1];
            name = moon;
            distance = 0;
        }

        public override string ToString()
        {
            return $"{name}";
        }
    }
}
