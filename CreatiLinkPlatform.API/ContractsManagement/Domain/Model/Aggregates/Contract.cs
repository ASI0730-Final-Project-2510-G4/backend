using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Commands;

namespace CreatiLinkPlatform.ContractsManagement.Domain.Model.Aggregates;

/// <summary>
/// ContractsManagement. aggregate root
/// </summary>
/// <remarks>
/// This class represents the contract aggregate root.
/// It contains the properties and methods to manage the contract information.
/// </remarks>
public partial class Contract
{
    public int Id { get; }

    public int UserId { get; internal set; }
    public Users ClientUser { get; internal set; }

    public int ProfileId { get; internal set; }
    public Profile DesignerProfile { get; internal set; }

    public decimal Price { get; set; }
    public string Requirements { get; set; }
    public string DesignType { get; set; }

    public Contract()
    {
    }

    public Contract(CreateContractCommand command)
    {
        UserId = command.ClientUserId;
        ProfileId = command.DesignerProfileId;
        Price = command.Price;
        Requirements = command.Requirements;
        DesignType = command.DesignType;
    }
}