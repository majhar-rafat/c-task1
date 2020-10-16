namespace CG_Calculator
{
    class Student
    {
        public string Name { get; }
        public int Roll { get; }
        public double Cg { get; set; }
        public Student(string name, int roll)
        {
            this.Name = name;
            this.Roll = roll;
        }
    }
}
