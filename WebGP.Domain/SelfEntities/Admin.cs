namespace WebGP.Domain.SelfEntities;
public class Admin : IAuditableEntity
{
    public int Id { get; set; }
    public string RoleName { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Note { get; set; } = null!;
    public DateTime RegistrationTime { get; set; }

    public int? CreatedById { get; set; }
    public virtual Admin? CreatedBy { get; set; } = null!;
}