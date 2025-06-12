using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LinqExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            //You can get the trainee details from the GetTraineeDetails() method in TraineeData class
            Console.WriteLine("Enter Menu Number");
            int option = Convert.ToInt32(Console.ReadLine());

            TraineeDetails obj = new TraineeDetails();
            TraineeData ob1 = new TraineeData();
            List<TraineeDetails> b = ob1.GetTraineeDetails();
            List<TraineeScore> t=new List<TraineeScore>();


            switch (option)
            {
                case 1:
                    {
                        //Show the list of Trainee Id
                        var id = b.Select(x => x.TraineeId).ToList();

                        id.ForEach(id => System.Console.WriteLine(id));
                        break;
                    }
                case 2:
                    {
                        //Show the first 3 Trainee Id using Take
                        var id = b.Take(3).Select(x => x.TraineeId).ToList();
                        id.ForEach(id => System.Console.WriteLine(id));
                        break;
                    }
                case 3:
                    {
                        //show the last 2 Trainee Id using Skip
                        var id=b.Skip((b.Count-2)).Select(x=> x.TraineeId).ToList();
                        id.ForEach(id => System.Console.WriteLine(id));
                        break;
                    }
                case 4:
                    {
                        //show the count of Trainee
                        var id=b.Count();
                        System.Console.WriteLine($"Count of trainee : {id}");
                        break;
                    }
                case 5:
                    {
                        //show the Trainee Name who are all passed out 2019 or later
                        var name=b.Where(x=> x.YearPassedOut>=2019).Select(x=> x.TraineeName).ToList();
                        name.ForEach(name=> System.Console.WriteLine(name));
                        break;
                    }
                case 6:
                    {
                        //show the Trainee Id and Trainee Name by alphabetic order of the trainee's name.
                        var result=b.OrderBy(x=> x.TraineeName).ToList();
                        result.ForEach(result=> System.Console.WriteLine($"Trainee ID:{result.TraineeId} Trainee Name:{result.TraineeName}"));
                        break;
                    }
                case 7:
                    {
                        //show the Trainee Id, Trainee Name, Topic Name, Exercise Name and Mark who are   all scores the more than or equal to 4 marks
                        var result=b.SelectMany(s=> s.ScoreDetails.Where(x=> x.Mark>=4).Select(y=> new
                                    {TraineeID=s.TraineeId,TraineeName=s.TraineeName,TopicName=y.TopicName,ExerciseName=y.ExerciseName,Mark=y.Mark})).ToList();
                        
                        result.ForEach(result=> System.Console.WriteLine(result));
                        break;
                    }
                case 8:
                    {
                        //show the unique passed out year using distinct
                        var result=b.Select(x=> x.YearPassedOut).Distinct().ToList();
                        result.ForEach(result=> System.Console.WriteLine(result));
                        break;
                    }
                case 9:
                    {
                        //show the total marks of single user (get the Trainee Id from User), if Trainee Id does not exist, show the invalid message
                        System.Console.WriteLine("Enter your Trainee ID :");
                        string traineeID=Console.ReadLine().ToUpper();

                        

                        var result=b.Where(w=> w.TraineeId==traineeID).Select(x=> x.ScoreDetails.Sum(y=> y.Mark)).ToList();
                        
                        if(result!=null)
                        {
                            result.ForEach(result=> System.Console.WriteLine($"Total marks:{result}"));
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid ID");
                        }
                        break;
                    }
                case 10:
                    {
                        //show the first Trainee Id and Trainee Name
                        var result=b.Select(x=> new{TraineeID=x.TraineeId, TraineeName=x.TraineeName}).FirstOrDefault();
                        System.Console.WriteLine(result);
                        break;
                    }
                case 11:
                    {
                        //show the last Trainee Id and Trainee Name
                        var result=b.Select(x=> new{TraineeId=x.TraineeId,  TraineeName=x.TraineeName}).LastOrDefault();
                        System.Console.WriteLine(result);
                        break;
                    }
                case 12:
                    {
                        //print the total score of each trainee
                        var result=b.Select(x=> x.ScoreDetails.Sum(y=> y.Mark)).ToList();
                        result.ForEach(result=> System.Console.WriteLine(result));
                        break;
                    }
                case 13:
                    {
                        //show the maximum total score
                        var result=b.Select(x=> x.ScoreDetails.Sum(y=> y.Mark)).Max();
                        System.Console.WriteLine(result);
                        break;
                    }
                case 14:
                    {
                        //show the minimum total score
                        var result=b.Select(x=> x.ScoreDetails.Sum(y=> y.Mark)).Min();
                        System.Console.WriteLine(result);
                        break;
                    }
                case 15:
                    {
                        //show the average of total score
                        var result=b.Select(x=> x.ScoreDetails.Sum(y=> y.Mark)).Average();
                        System.Console.WriteLine(result);
                        break;
                    }
                case 16:
                    {
                        //show true or false if anyone has the more than 40 score using any ()
                        var result=b.Any(x=> x.ScoreDetails.Sum(y=> y.Mark)>40);
                        System.Console.WriteLine(result);
                        
                        break;
                    }
                case 17:
                    {
                        //show true of false if all of them has the more than 20 using all ()
                        var result=b.All(x=> x.ScoreDetails.Sum(y=> y.Mark)>20);
                        System.Console.WriteLine(result);
                        break;
                    }
                case 18:
                    {
                        //show the Trainee Id, Trainee Name, Topic Name, Exercise Name and Mark by showing the Trainee Name 
                        // as descending order and then show the Mark as descending order.
                        var result=b.SelectMany(x=> x.ScoreDetails.Select(y=> new
                                    {TraineeId=x.TraineeId, TraineeName=x.TraineeName, TopicName=y.TopicName, ExerciseName=y.ExerciseName , Mark=y.Mark})).OrderByDescending(n=> n.TraineeName).ThenByDescending(m=> m.Mark).ToList();

                        result.ForEach(result=> System.Console.WriteLine(result));
                        break;
                    }



            }

        }
    }
}
