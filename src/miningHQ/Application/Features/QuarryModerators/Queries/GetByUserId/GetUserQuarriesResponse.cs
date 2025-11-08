namespace Application.Features.QuarryModerators.Queries.GetByUserId;

public class GetUserQuarriesResponse
{
    public List<UserQuarryDto> Quarries { get; set; }
}

public class UserQuarryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
