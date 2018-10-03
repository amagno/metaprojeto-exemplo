using MediatR;

namespace MetaProjetoExemplo.Application.Commands.ProjectManagement
{
  public class FinalizeProjectCommand : IRequest<bool>
  {
    public int Id { get; set; }
    public FinalizeProjectCommand(int id)
    {
      Id = id;
    }

  }
}