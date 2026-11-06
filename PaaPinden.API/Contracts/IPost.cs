using PaaPinden.API.Data;

namespace PaaPinden.API.Contracts
{
    public interface IPost
    {
        string Id { get; set; }
        Byte[] Media { get; set; }
        string MediaType { get; set; }
        string Description { get; set; }
        int Likes { get; set; }
        string Link {  get; set; }
        string UserId { get; set; }
        ApplicationUser? User { get; set; }
    }
}
