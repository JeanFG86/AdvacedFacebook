namespace AdvacedFacebook.src.Data.Contracts.Repos
{
    public interface ISaveFacebookRepository
    {
        Task SaveWithFacebook(ISaveFacebookRepositoryParams param);
    }

    public record ISaveFacebookRepositoryParams(string? Id, string Email, string Name, string FacebookId);
    //{
    //    public string? Id { get; set; }
    //    public string Email { get; set; }
    //    public string Name { get; set; }
    //    public string FacebookId { get; set; }
    //}
}
