namespace CG_Calculator
{
    class StudentResult
    {
        public string Subject { get;}
        public int Roll { get; }
        public double Gp { get; }
        public StudentResult(int roll, string subject, double gp)
        {
            this.Subject = subject;
            this.Roll = roll;
            this.Gp = gp;
        }
    }
}
