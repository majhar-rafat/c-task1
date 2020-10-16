using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace CG_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            string pathOfStudentResult = config["pathOfStudentResult"];
            string pathOfStudentInfo = config["pathOfStudentInfo"];

            List<Student> studentInfo = Populate.PopulateStudentInfo(pathOfStudentInfo);
            List<StudentResult> studentResult = Populate.PopulateStudentResult(pathOfStudentResult);

            var studentGroupedBy = studentResult.GroupBy(info => info.Roll);
            Calculate.CG(studentGroupedBy, studentInfo);
        }
    }
}
