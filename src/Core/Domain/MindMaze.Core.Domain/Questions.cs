using MindMaze.Core.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain
{
    public class Questions : BaseClass
    {
        public string Question { get; set; }

        public string CorrectAnswer { get; set; }

        public string FakeAnswer1 { get; set; }

        public string FakeAnswer2 { get; set;}

        public string FakeAnswer3 { get; set; }

        public ICollection<GamesQuestions> GamesQuestions { get; set; }
    }
}
