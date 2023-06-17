using Services.Models;
using Services.RequestDB.InterfaceDB;
using System;
using System.Linq;

namespace Services
{
    public class AssessmentSystem
    {
        private readonly ITrainModuleService TMS;
        public AssessmentSystem(ITrainModuleService trainModuleService)
        {
            TMS = trainModuleService;
        }
        public int Assessment(Question_S question)
        {
            double count = 0;
            var answers = TMS.GetAnswers(question.Id, false).ToList();
            for(int i = 0; i < answers.Count; i++)
            {
                if(answers[i].Flag == question.Answers[i].Flag)
                {
                    count++;
                }
            }
            return (int)((count / answers.Count) * 100);
        }
        public static int Assessment(bool TaskCompleted, Counter counter)
        {
            if (TaskCompleted)
            {
                return counter.Value < counter.end ? 1 : 0;
            }
            return 0;
        }
        public static void Assessment(ScriptUser_S script)
        {
            double percentGQ =
                script.Test.Questions.Sum(q => q.Score)*1.0f / 
                (script.Test.Questions.Count * 100) * 100;

            script.Test.QuestionsScore =
                (percentGQ < 30) ? 2 :
                (percentGQ < 70) ? 3 :
                (percentGQ < 90) ? 4 : 5;

            double percentGT = 
                script.Test.Tasks.Sum(q => q.Score)*1.0f / 
                script.Test.Tasks.Count * 100;

            script.Test.TasksScore =
                (percentGT <= 10) ? 2 :
                (percentGT <= 50) ? 3 :
                (percentGT <= 70) ? 4 : 5;

            script.Score = (script.Test.QuestionsScore + script.Test.TasksScore) / 2;
        }
    }
}