using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class OperationClaim : Entity
{
	public int Id { get; set; }
	public string Name { get; set; }
}
