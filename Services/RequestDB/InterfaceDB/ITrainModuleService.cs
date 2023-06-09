﻿using Models;
using Services.Models;
using System.Collections.Generic;

namespace Services.RequestDB.InterfaceDB
{
    public interface ITrainModuleService
    {
        IEnumerable<ATask> GetTasks();
        IEnumerable<ATask> GetTasks(string Discriminator);
        BaseMath_S GetBasicMath(int Id, string? type=null);
        IEnumerable<MethodProgramm_S> GetMethodPrograms();
        MethodProgramm_S GetMethodProgram(int id);
        SelectKey_S GetSelectKey(int id);

        IEnumerable<Topic_S> GetTopics();
        Topic_S GetTopic(string topic);
        IEnumerable<string> GetDiscriminators();
        IEnumerable<string> GetTypeTask(string Discriminator);
        IEnumerable<Question_S> GetQuestions(int Topic);
        IEnumerable<Question_S> GetQuestions();
        IEnumerable<Answer_S> GetAnswers();
        IEnumerable<Answer_S> GetAnswers(int QuestionID, bool clearAnswer);
        Test_S GetTest(int? Id, bool clearAnswer);
        IEnumerable<Test_S> GetTests(bool clearAnswer);
        IEnumerable<Script_S> GetScripts();
        Script_S GetScript(int id, bool clearAnswer);

        Question_S? UpdateQuestion(Question_S question);
        Question_S? SaveQuestion(Question_S question);
        bool DeleteQuestion(Question_S question);
        Topic_S SaveTopic(string topic);
        bool UpdateScript(Script_S script);
        bool SaveScript(Script_S script);
        bool DeleteScripts(IEnumerable<Script_S> scripts);
    }
}