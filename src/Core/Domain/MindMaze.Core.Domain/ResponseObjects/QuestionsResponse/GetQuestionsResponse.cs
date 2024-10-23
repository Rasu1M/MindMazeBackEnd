using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain.ResponseObjects.QuestionsResponse
{
    public class GetQuestionsResponse
    {
        public string Question { get; set; }

        public List<string> Answers { get; set; } = new List<string>(4);

        public string CorrectAnswer { get; set; }

        public static GetQuestionsResponse CreateClass(Questions question)
        {
          GetQuestionsResponse newquestion =  new GetQuestionsResponse()
            {
                Question = question.Question,
                Answers = new List<string>(4)
                {
                    question.CorrectAnswer,
                    question.FakeAnswer1,
                    question.FakeAnswer2,
                    question.FakeAnswer3
                },
                CorrectAnswer = question.CorrectAnswer
            };

            newquestion.Answers = GetQuestionsResponse.RandomAnswers(newquestion.Answers);

            return newquestion;

        }


        private static List<string> RandomAnswers(List<string> answers)
        {
            return answers.OrderBy(x => Guid.NewGuid()).ToList();
        }

    }
}
