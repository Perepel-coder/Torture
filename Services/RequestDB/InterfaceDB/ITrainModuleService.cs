using Services.Models;
using System.Collections.Generic;

namespace Services.RequestDB.InterfaceDB
{
    public interface ITrainModuleService
    {
        IEnumerable<ATask> GetTasks();
        IEnumerable<ATask> GetTasks(string Discriminator);
        BaseMath_S GetBasicMath(int Id, string? type=null);
        IEnumerable<MethodProgramm_S> GetMethodPs();
        MethodProgramm_S GetMethodP(int id);
        SelectKey_S GetSelectKey(int id);

        IEnumerable<Topic_S> GetTopics();
        Topic_S GetTopic(string topic);
        IEnumerable<string> GetDiscriminators();
        IEnumerable<string> GetTypeTask(string Discriminator);
        IEnumerable<Question_S> GetQuestions(int Topic);
        IEnumerable<Question_S> GetQuestions();
        IEnumerable<Answer_S> GetAnswers();
        IEnumerable<Answer_S> GetAnswers(int QuestionID, bool clearAnswer);
        Test_S GetTest(int scriptId, bool clearAnswer);
        IEnumerable<Test_S> GetTests(bool clearAnswer);
        IEnumerable<AScript> GetScripts();
        Script_S GetScript(int id, bool clearAnswer);

        bool UpdateQuestion(Question_S question);
        bool SaveQuestion(Question_S question);
        bool SaveTopic(string topic);
    }
}