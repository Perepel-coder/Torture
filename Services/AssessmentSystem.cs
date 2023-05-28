using Services.Models;
using Services.RequestDB.InterfaceDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AssessmentSystem
    {
        private readonly ITrainModuleService TMS;
        public AssessmentSystem(ITrainModuleService trainModuleService)
        {
            TMS = trainModuleService;
        }
        public bool Assessment(Question_S question)
        {
            var answers = TMS.GetAnswers(question.Id, false).ToList();
            for(int i = 0; i < answers.Count; i++)
            {
                if(answers[i].Flag != question.Answers[i].Flag)
                {
                    return false;
                }
            }
            return true;
        }
        public int Assessment(bool TaskCompleted, Counter counter)
        {
            if (TaskCompleted)
            {
                return counter.Value < counter.end ? 1 : 0;
            }
            return 0;
        }
        public void Assessment(ScriptUser_S script)
        {
            double percentGQ =
                ((double)script.Test.Questions.Sum(q => q.Score)) / 
                ((double)script.Test.Questions.Count) * 100;
            script.Test.QuestionsScore =
                (percentGQ < 30) ? 2 :
                (percentGQ < 60) ? 3 :
                (percentGQ < 90) ? 4 : 5;

            double percentGT = 
                ((double)script.Test.Tasks.Sum(q => q.Score)) / 
                ((double)script.Test.Tasks.Count) * 100;
            script.Test.TasksScore =
                (percentGT <= 10) ? 2 :
                (percentGT <= 50) ? 3 :
                (percentGT <= 70) ? 4 : 5;

            script.Score = (script.Test.QuestionsScore + script.Test.TasksScore) / 2;
        }
    }
}
