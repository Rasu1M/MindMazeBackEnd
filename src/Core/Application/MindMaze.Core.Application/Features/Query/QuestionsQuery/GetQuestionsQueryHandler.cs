using MediatR;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.QuestionsResponse;
using MindMaze.Core.Domain.Resultobjects;


namespace MindMaze.Core.Application.Features.Query.QuestionsQuery
{
    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, CustomResult<List<GetQuestionsResponse>>>
    {
        private readonly IReadGenericRepository<Questions> _read;

        private readonly IGenericRepository<Questions> _generic;

        IUnitOfWork unit;

        public GetQuestionsQueryHandler(IReadGenericRepository<Questions> read, IGenericRepository<Questions> generic, IUnitOfWork unit)
        {
            _read = read;
            _generic = generic;
            this.unit = unit;
        }

        public async Task<CustomResult<List<GetQuestionsResponse>>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
          


            var questions = _read.AsQueryable().OrderBy(x => Guid.NewGuid()).Take(request.Count).ToList();


            if (questions == null)
            {
                return null;
            }

            return questions.ConvertAll<GetQuestionsResponse>(question => GetQuestionsResponse.CreateClass(question));

        }
    }
}
