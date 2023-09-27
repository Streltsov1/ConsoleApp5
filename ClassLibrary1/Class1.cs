namespace ClassLibrary1
{
    [Serializable]
    public class Car
    {
        public string VINcode { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public Car()
        {
            VINcode= "";
            Year= 0;
            Model = "";
            Color = "";
        }
    }
}