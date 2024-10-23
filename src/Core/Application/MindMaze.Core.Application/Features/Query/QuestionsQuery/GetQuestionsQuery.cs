using MediatR;
using MindMaze.Core.Domain.ResponseObjects.QuestionsResponse;
using MindMaze.Core.Domain.Resultobjects;

namespace MindMaze.Core.Application.Features.Query.QuestionsQuery
{
    public record GetQuestionsQuery : IRequest<CustomResult<List<GetQuestionsResponse>>>
    {
        public int Count { get; set; }
    }
}
